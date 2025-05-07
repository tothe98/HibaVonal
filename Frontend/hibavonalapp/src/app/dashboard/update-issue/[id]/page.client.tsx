"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import InputField from "@/components/InputField";
import Button from "@/components/Button";
import { fetchIssue, updateIssue } from "./actions";

interface Role {
  roleId: number;
  name: string;
}

interface Room {
  id: number;
  floor: number;
  roomType: string;
  number: number;
  residents: any[];
  equipments: any[];
  dormitory: any | null;
  dormitoryId: number;
}

type Issue = {
  id: number;
  reportTime: string;
  description: string;
  comment: string | null;
  status: string;
  level: string;
  room: Room;
  maintenanceWorker: User | null;
  reporter: User;
};
interface User {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  roles: Role[];
}

interface UpdateIssueFormData {
  description: string;
  level: number;
  roomId: number;
}

interface Props {
  user: User;
  updateId: number;
}
//Ha beírod az url-t és úgy nyitod meg, lehet meghal mert nem kap updateId-t idk
export default function UpdateIssueClientPage({ user, updateId }: Props) {
  const router = useRouter();
  const [formData, setFormData] = useState<UpdateIssueFormData>({
    description: "",
    level: 0,
    roomId: 0,
  });
  const [error, setError] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "level" ? parseInt(value) || 0 : value,
    }));
  };

  const levelMap: Record<string, number> = {
    Low: 0,
    Medium: 1,
    High: 2,
    Critical: 3,
  };

  useEffect(() => {
    const loadIssue = async () => {
      const result = await fetchIssue(updateId);
      //console.log(result.data.level);
      if (!result.success || !result.data || result.data.length === 0) {
        setError(result.error || "Issue not found.");
        console.log("Error loading the issue data");
        return;
      }
      //bullshit múködik...
      const data: Issue = result.data;

      setFormData({
        description: data.description || "",
        level: levelMap[data.level] ?? 0,
        roomId: data.room.id,
      });
    };

    loadIssue();
  }, [updateId]);

  const handleLevelChange = (level: number) => {
    setFormData((prev) => ({
      ...prev,
      level,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setIsSubmitting(true);

    if (!formData.description) {
      setError("Description is required.");
      setIsSubmitting(false);
      return null;
    }

    if (formData.level < 0 || formData.level > 3) {
      setError("Please select a valid level.");
      setIsSubmitting(false);
      return null;
    }

    const result = await updateIssue(
      {
        description: formData.description,
        level: formData.level,
      },
      updateId
    );

    setIsSubmitting(false);

    if (!result.success) {
      setError(result.error);
      return null;
    }

    router.push("/dashboard/issues");
  };

  const levelStyles = ["bg-err-low hover:bg-err-low-h", "bg-err-medium hover:bg-err-medium-h", "bg-err-high hover:bg-err-high-h", "bg-err-critical hover:bg-err-critical-h"];

  return (
    <main
      className={`
                w-full my-4 sm:max-w-md max-w-xs p-4 sm:p-6
                bg-white rounded-xl shadow-2xl shadow-gray-600
                flex flex-col items-center
            `}
    >
      <h1 className="text-2xl font-semibold mb-4">Update Issue</h1>
      <div className="h-8">{error && <p className="text-red-500 mb-4">{error}</p>}</div>
      <form onSubmit={handleSubmit} className="w-full">
        <div className="mb-4">
          <label htmlFor="description" className="block ml-1 mb-2 text-md text-gray-700">
            Description
          </label>
          <textarea
            name="description"
            id="description"
            value={formData.description}
            placeholder="Enter issue description"
            className={`
                            w-full min-w-64 py-4 px-3 rounded-md border-2 leading-tight
                            border-gray-500 hover:border-cyan-500 focus:border-cyan-500 text-base text-gray-900 placeholder:text-gray-500
                            focus:outline-none appearance-none
                        `}
            onChange={handleChange}
            rows={4}
          />
        </div>
        <div className="mb-4">
          <label htmlFor="level" className="block ml-1 mb-2 text-md text-gray-700">
            Level
          </label>
          <div className="hidden sm:flex justify-center gap-2">
            {["Low", "Medium", "High", "Critical"].map((label, i) => (
              <Button
                key={label}
                type="button"
                onClick={() => handleLevelChange(i)}
                className={`
                                    flex-1 px-4 py-2 rounded-md text-sm text-white
                                    ${formData.level === i ? levelStyles[i] : "bg-gray-300 text-gray-700 hover:bg-gray-400"}
                                `}
              >
                {label}
              </Button>
            ))}
          </div>

          {/* dropdown if sm screen*/}
          <div className="sm:hidden">
            <select id="level" name="level" value={formData.level} onChange={(e) => handleLevelChange(parseInt(e.target.value))} className="w-full py-2 px-3 h-14 rounded-md border-2 border-gray-500 hover:border-cyan-500 focus:border-cyan-500 focus:outline-none">
              <option value={0}>Low</option>
              <option value={1}>Medium</option>
              <option value={2}>High</option>
              <option value={3}>Critical</option>
            </select>
          </div>
        </div>

        <div className="mb-8">
          <InputField name="roomId" value={formData.roomId + ""} label="Room ID" type="string" placeholder="Enter room ID" disabled onChange={handleChange} min={1} />
        </div>
        <div className="flex justify-end gap-2">
          <Button type="button" onClick={() => router.push("/dashboard")} className="px-4 py-2 bg-gray-300 text-gray-700 hover:bg-gray-400" disabled={isSubmitting}>
            Cancel
          </Button>
          <Button type="submit" className="px-6 py-2" disabled={isSubmitting}>
            {isSubmitting ? "Submitting..." : "Submit"}
          </Button>
        </div>
      </form>
    </main>
  );
}
