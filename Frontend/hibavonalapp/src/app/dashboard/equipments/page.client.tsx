"use client";

import { useState, useEffect, useMemo } from "react";
import { useRouter } from "next/navigation";
import Button from "@/components/Button";
import { deleteEquipment, fetchEquipments } from "./actions";

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

interface Equipment {
    id: number;
    name: string;
    errorType: ErrorType;
}

interface ErrorType {
    id: number;
    name: string;
}

interface EquipmentsClientProps {
    user: User;
}

type SortKey = keyof Equipment | "errorType.name";

export default function EquipmentsClientPage({ user }: EquipmentsClientProps) {
    const router = useRouter();
    const [equipments, setEquipments] = useState<Equipment[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [sortKey, setSortKey] = useState<SortKey | null>(null);
    const [sortOrder, setSortOrder] = useState<"asc" | "desc">("asc");
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [selectedEquipment, setSelectedEquipment] = useState<Equipment | null>(null);

    const openDeleteModal = (equipment: Equipment) => {
        setSelectedEquipment(equipment);
        setShowDeleteModal(true);
    };

    const closeDeleteModal = () => {
        setShowDeleteModal(false);
        setSelectedEquipment(null);
    };

    useEffect(() => {
        const loadEquipments = async () => {
            const result = await fetchEquipments();
            if (!result.success) {
                setError(result.error);
                return;
            }
            setEquipments(result.data);
        };
        loadEquipments();
    }, []);

    const handleDelete = async (id: number) => {
        const result = await deleteEquipment(id);
        if (!result.success) {
            setError(result.error);
            closeDeleteModal();
            return;
        }
        setEquipments((prev) => prev.filter((eq) => eq.id !== id));
        closeDeleteModal();
    };

    const handleSort = (key: SortKey) => {
        if (sortKey === key) {
            setSortOrder((prev) => (prev === "asc" ? "desc" : "asc"));
        } else {
            setSortKey(key);
            setSortOrder("asc");
        }
    };

    const sortedEquipments = useMemo(() => {
        if (!sortKey) return equipments;

        return [...equipments].sort((a, b) => {
            let aVal: string | number = "";
            let bVal: string | number = "";

            if (sortKey === "errorType.name") {
                aVal = a.errorType?.name || "";
                bVal = b.errorType?.name || "";
            } else {
                const aProp = a[sortKey];
                const bProp = b[sortKey];

                if (typeof aProp === "string" || typeof aProp === "number") {
                    aVal = aProp;
                    bVal = bProp as typeof aVal;
                }
            }

            if (typeof aVal === "string" && typeof bVal === "string") {
                return sortOrder === "asc" ? aVal.localeCompare(bVal) : bVal.localeCompare(aVal);
            }

            return sortOrder === "asc" ? (aVal as number) - (bVal as number) : (bVal as number) - (aVal as number);
        });
    }, [equipments, sortKey, sortOrder]);

    const renderHeader = (label: string, key: SortKey) => (
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
            {showDeleteModal && selectedEquipment && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
                    <div className="bg-white p-6 rounded-lg shadow-lg max-w-sm w-full">
                        <h2 className="text-lg font-semibold mb-4">Confirm Deletion</h2>
                        <p>
                            Are you sure you want to delete <strong>{selectedEquipment.name}</strong>?
                        </p>
                        <div className="mt-6 flex justify-end space-x-3">
                            <button onClick={closeDeleteModal} className="px-4 py-2 bg-gray-300 text-gray-700 rounded hover:bg-gray-400">
                                Cancel
                            </button>
                            <button onClick={() => handleDelete(selectedEquipment.id)} className="px-4 py-2 bg-red-600 text-white rounded hover:bg-red-700">
                                Delete
                            </button>
                        </div>
                    </div>
                </div>
            )}
            <div className="flex justify-between items-center w-full mb-4">
                <Button type="button" onClick={() => router.push("/dashboard")} className="px-4 py-2 bg-gray-300 text-gray-700 hover:bg-gray-400">
                    Back
                </Button>
                <h1 className="text-2xl font-semibold">Equipments</h1>
                <Button type="button" onClick={() => router.push("/dashboard/create")} className="px-4 py-2">
                    Create Equipment
                </Button>
            </div>

            <div className="h-8">{error && <p className="text-red-500 mb-4">{error}</p>}</div>

            <div className="w-full overflow-x-auto">
                <table className="w-full text-left">
                    <thead>
                        <tr className="text-gray-700 border-b">
                            {renderHeader("Name", "name")}
                            {renderHeader("Error Type", "errorType.name")}
                        </tr>
                    </thead>
                    <tbody>
                        {sortedEquipments.length === 0 ? (
                            <tr>
                                <td colSpan={5} className="p-4 text-center text-gray-500">
                                    No Equipment found.
                                </td>
                            </tr>
                        ) : (
                            sortedEquipments.map((equipment) => (
                                <tr key={equipment.id} className="border-t hover:bg-gray-50">
                                    <td className="p-2">{equipment.name}</td>
                                    <td className="p-2">
                                        <span className="block max-w-[200px] whitespace-nowrap overflow-hidden text-ellipsis" title={equipment.errorType?.name || undefined}>
                                            {equipment.errorType?.name || "-"}
                                        </span>
                                    </td>
                                    <td className="p-2 text-right">
                                        <Button onClick={() => openDeleteModal(equipment)} className="bg-err-high py-[7px] px-[15px] hover:bg-err-high-h" id={equipment.id + ""} type={"button"}>
                                            Delete
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
