"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import InputField from "@/components/InputField";
import Button from "@/components/Button";
import { fetchRoles, registerUser, registerUserWithRoom } from "./actions";

interface Role {
  roleId: number;
  name: string;
}

interface Roles {
  id: number;
  name: string;
}

interface User {
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  roles: Role[];
}

interface RegisterFormData {
  name: string;
  email: string;
  password: string;
  passwordConfirm: string;
  phoneNumber: string;
  roleId: number;
  roomId: number;
}

interface ResponseData {
  success: boolean;
  error: any;
}

interface Props {
  user: User;
  isAuthenticated: boolean;
}

export default function RegisterUserClientPage({ user }: Props) {
  const router = useRouter();
  const [formData, setFormData] = useState<RegisterFormData>({
    name: "",
    email: "",
    password: "",
    passwordConfirm: "",
    phoneNumber: "",
    roleId: 1,
    roomId: 0,
  });
  const [error, setError] = useState<string | null>(null);
  const [result, setResult] = useState<ResponseData>();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [roles, setRoles] = useState<Roles[]>([]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "roomId" ? parseInt(value) || 0 : value,
    }));
  };

  useEffect(() => {
    const loadRoles = async () => {
      const result = await fetchRoles();
      if (!result.success) {
        setError(result.error);
        return;
      }
      console.log(result);
      setRoles(result.data);
    };
    loadRoles();
  }, []);

  const handleRoleId = (roleId: number) => {
    console.log(roleId);

    setFormData((prev) => ({
      ...prev,
      roleId,
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setIsSubmitting(true);

    if (formData.roleId < 1 || formData.roleId > 4) {
      setError("Please select a valid role.");
      setIsSubmitting(false);
      return null;
    }

    if (!formData.name) {
      setError("Name is required.");
      setIsSubmitting(false);
      return null;
    }
    if (!formData.email) {
      setError("Email is required.");
      setIsSubmitting(false);
      return null;
    }

    if (!formData.password) {
      setError("Password is required.");
      setIsSubmitting(false);
      return null;
    }
    if (!formData.passwordConfirm) {
      setError("Password-Confirm is required.");
      setIsSubmitting(false);
      return null;
    }
    if (formData.password != formData.passwordConfirm) {
      setError("The passwords do not match.");
      setIsSubmitting(false);
      return null;
    }

    if (formData.roomId == 0) {
      setResult(
        await registerUser({
          name: formData.name,
          email: formData.email,
          password: formData.password,
          passwordConfirm: formData.passwordConfirm,
          phoneNumber: formData.phoneNumber,
          roleId: formData.roleId,
        })
      );
    } else {
      setResult(
        await registerUserWithRoom({
          name: formData.name,
          email: formData.email,
          password: formData.password,
          passwordConfirm: formData.passwordConfirm,
          phoneNumber: formData.phoneNumber,
          roleId: formData.roleId,
          roomId: formData.roomId,
        })
      );
    }

    setIsSubmitting(false);

    if (!result?.success) {
      setError(result?.error);
      return null;
    }

    router.push("/dashboard");
  };

  return (
    <main
      className={`
                w-full my-4 sm:max-w-md max-w-xs p-4 sm:p-6
                bg-white rounded-xl shadow-2xl shadow-gray-600
                flex flex-col items-center
            `}
    >
      <h1 className="text-2xl font-semibold mb-4">Register User</h1>
      <div className="h-8">{error && <p className="text-red-500 mb-4">{error}</p>}</div>
      <form onSubmit={handleSubmit} className="w-full">
        <div className="mb-8">
          <InputField name="name" label="Name" type="string" placeholder="Name" onChange={handleChange} />
        </div>
        <div className="mb-8">
          <InputField name="email" label="Email" type="string" placeholder="Email" onChange={handleChange} />
        </div>
        <div className="mb-8">
          <InputField name="phoneNumber" label="PhoneNumber" type="string" placeholder="PhoneNumber" onChange={handleChange} />
        </div>
        <div className="mb-8">
          <InputField name="password" label="Password" type="password" placeholder="Password" onChange={handleChange} />
        </div>
        <div className="mb-8">
          <InputField name="passwordConfirm" label="Password-Confirm" type="password" placeholder="Password-Confirm" onChange={handleChange} />
        </div>
        <div className="">
          <select id="roleId" name="roleId" value={formData.roleId} onChange={(e) => handleRoleId(parseInt(e.target.value))} className="w-full py-2 px-3 h-14 rounded-md border-2 border-gray-500 hover:border-cyan-500 focus:border-cyan-500 focus:outline-none">
            <option value="" disabled>
              Select a role
            </option>
            {roles.map((role) => (
              <option key={role.id} value={role.id}>
                {role.name}
              </option>
            ))}
          </select>
        </div>
        <div className="mb-8">
          <InputField name="roomId" label="RoomId" type="number" placeholder="RoomId" onChange={handleChange} />
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
