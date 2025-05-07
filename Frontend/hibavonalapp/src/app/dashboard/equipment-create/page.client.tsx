"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import InputField from "@/components/InputField";
import Button from "@/components/Button";
import { createEquipment } from "./actions";

interface Role {
  roleId: number;
  name: string;
}

interface User {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  roles: Role[];
}

interface CreateEquipmentFormData {
  name: string;
}

interface Props {
  user: User;
}

export default function EquipmentCreateClientPage({ user }: Props) {
  const router = useRouter();
  const [formData, setFormData] = useState<CreateEquipmentFormData>({
    name: "",
  });
  const [error, setError] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setIsSubmitting(true);

    if (!formData.name) {
      setError("Name is required.");
      setIsSubmitting(false);
      return null;
    }

    const result = await createEquipment({
      name: formData.name,
    });

    setIsSubmitting(false);

    if (!result.success) {
      setError(result.error);
      return null;
    }

    router.push("/dashboard/equipments");
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
      <h1 className="text-2xl font-semibold mb-4">Create Equipment</h1>
      <div className="h-8">{error && <p className="text-red-500 mb-4">{error}</p>}</div>
      <form onSubmit={handleSubmit} className="w-full">
        <div className="mb-8">
          <InputField name="name" label="Name" type="string" placeholder="Enter equipment Name" onChange={handleChange} />
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
