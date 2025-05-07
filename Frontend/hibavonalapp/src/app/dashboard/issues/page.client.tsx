"use client";

import { useState, useEffect, useMemo } from "react";
import { useRouter } from "next/navigation";
import Button from "@/components/Button";
import { fetchIssues } from "./actions";

interface Role {
  roleId: number;
  name: string;
}

interface User {
  name: string;
  email: string;
  phoneNumber: string;
  roles: Role[];
}

interface Issue {
  id: number;
  reportTime: string;
  description: string;
  comment: string | null;
  status: string;
  level: string;
  reporter: {
    email: string;
    name: string;
  };
}

interface Props {
  user: User;
}

export default function IssuesClientPage({ user }: Props) {
  const router = useRouter();
  const [issues, setIssues] = useState<Issue[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [sortKey, setSortKey] = useState<keyof Issue | null>(null);
  const [sortOrder, setSortOrder] = useState<"asc" | "desc">("asc");

  useEffect(() => {
    const loadIssues = async () => {
      const result = await fetchIssues();
      if (!result.success) {
        setError(result.error);
        return;
      }
      setIssues(result.data);
    };
    loadIssues();
  }, []);

  const handleSort = (key: keyof Issue) => {
    if (sortKey === key) {
      setSortOrder((prev) => (prev === "asc" ? "desc" : "asc"));
    } else {
      setSortKey(key);
      setSortOrder("asc");
    }
  };

  const sortedIssues = useMemo(() => {
    if (!sortKey) return issues;

    return [...issues].sort((a, b) => {
      const aVal = a[sortKey];
      const bVal = b[sortKey];

      if (typeof aVal === "string" && typeof bVal === "string") {
        return sortOrder === "asc" ? aVal.localeCompare(bVal) : bVal.localeCompare(aVal);
      }

      return sortOrder === "asc" ? (aVal as number) - (bVal as number) : (bVal as number) - (aVal as number);
    });
  }, [issues, sortKey, sortOrder]);

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString("en-US", {
      year: "numeric",
      month: "short",
      day: "numeric",
      hour: "2-digit",
      minute: "2-digit",
    });
  };

  const levelStyles: Record<string, string> = {
    low: "bg-err-low",
    medium: "bg-err-medium",
    high: "bg-err-high",
    critical: "bg-err-critical",
  };
  const getLevelStyle = (level: string) => levelStyles[level.toLowerCase()];

  const renderHeader = (label: string, key: keyof Issue) => (
    <th className="p-2 cursor-pointer select-none" onClick={() => handleSort(key)}>
      <span className="flex items-center">
        {label}
        <svg className={`w-4 h-4 ml-1 transition-transform ${sortKey === key && sortOrder === "desc" ? "rotate-180" : ""}`} xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="currentColor">
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="m8 15 4 4 4-4m0-6-4-4-4 4" />
        </svg>
      </span>
    </th>
  );

  return (
    <main
      className={`
        w-full sm:my-4 max-w-4xl p-4 sm:p-6 mx-auto
        bg-white rounded-none sm:rounded-xl shadow-2xl shadow-gray-600
        flex flex-col items-center
      `}
    >
      <div className="flex justify-between items-center w-full mb-4">
        <Button type="button" onClick={() => router.push("/dashboard")} className="px-4 py-2 bg-gray-300 text-gray-700 hover:bg-gray-400">
          Back
        </Button>
        <h1 className="text-2xl font-semibold">Issues</h1>
        <Button type="button" onClick={() => router.push("/dashboard/report-issue")} className="px-4 py-2">
          Report Issue
        </Button>
      </div>

      <div className="h-8">{error && <p className="text-red-500 mb-4">{error}</p>}</div>

      <div className="w-full overflow-x-auto">
        <table className="w-full text-left">
          <thead>
            <tr className="text-gray-700 border-b">
              {renderHeader("Reported", "reportTime")}
              {renderHeader("Description", "description")}
              {renderHeader("Status", "status")}
              {renderHeader("Comment", "comment")}
              {renderHeader("Level", "level")}
            </tr>
          </thead>
          <tbody>
            {sortedIssues.length === 0 ? (
              <tr>
                <td colSpan={5} className="p-4 text-center text-gray-500">
                  No issues found.
                </td>
              </tr>
            ) : (
              sortedIssues.map((issue) => (
                <tr key={issue.id} className="border-t hover:bg-gray-50">
                  <td className="p-2">{formatDate(issue.reportTime)}</td>
                  <td className="p-2">
                    <span className="block max-w-[200px] whitespace-nowrap overflow-hidden text-ellipsis" title={issue.description}>
                      {issue.description}
                    </span>
                  </td>
                  <td className="p-2">{issue.status}</td>
                  <td className="p-2">
                    <span className="block max-w-[200px] whitespace-nowrap overflow-hidden text-ellipsis" title={issue.comment || undefined}>
                      {issue.comment || "-"}
                    </span>
                  </td>
                  <td className="p-2">
                    <span className={`inline-block text-white text-center px-2 w-20 py-1 rounded ${getLevelStyle(issue.level)}`}>{issue.level}</span>
                  </td>
                  <td className="p-2 text-right">
                    <Button
                      onClick={() => {
                        router.push(`/dashboard/update-issue/${issue.id}`);
                      }}
                      className="bg-blue-400 py-[7px] px-[15px] hover:bg-blue-800"
                      id={issue.id + ""}
                      type={"button"}
                    >
                      Update
                    </Button>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </main>
  );
}
