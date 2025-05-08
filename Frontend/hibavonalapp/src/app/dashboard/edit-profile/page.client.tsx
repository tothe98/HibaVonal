"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import InputField from "@/components/InputField";
import Button from "@/components/Button";
import { updateProfile, updatePassword } from "./actions";

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

interface ProfileFormData {
  name: string;
  email: string;
  phoneNumber: string;
}

interface PasswordFormData {
  currentPassword: string;
  newPassword: string;
  newPasswordConfirm: string;
}

interface EditProfileFormData {
  name: string;
  email: string;
  phoneNumber: string;
  currentPassword: string;
  newPassword: string;
  newPasswordConfirm: string;
}

interface Props {
    user: User
}

export default function EditProfileClientPage({ user }: Props) {
  const router = useRouter();
  const [formData, setFormData] = useState<EditProfileFormData>({
    name: user.name,
    email: user.email,
    phoneNumber: user.phoneNumber,
    currentPassword: "",
    newPassword: "",
    newPasswordConfirm: "",
  });
  const [profileError, setProfileError] = useState<string | null>(null);
  const [passwordError, setPasswordError] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const hasProfileChanges = () => {
    return formData.name !== user.name || formData.email !== user.email || formData.phoneNumber !== user.phoneNumber;
  };

  const hasPasswordChanges = () => {
    return formData.currentPassword !== "" && formData.newPassword !== "" && formData.newPasswordConfirm !== "";
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setProfileError(null);
    setPasswordError(null);
    setIsSubmitting(true);

    // Profile validation
    let profileValid = true;
    if (hasProfileChanges()) {
      if (!formData.name) {
        setProfileError("Name is required.");
        profileValid = false;
      }

      if (!formData.email) {
        setProfileError("Email is required.");
        profileValid = false;
      }
    }

    // Password validation
    let passwordValid = true;
    if (hasPasswordChanges()) {
      if (!formData.currentPassword) {
        setPasswordError("Current password is required.");
        passwordValid = false;
      }

      if (formData.newPassword.length < 8) {
        setPasswordError("New password must be at least 8 characters.");
        passwordValid = false;
      }

      if (formData.newPassword !== formData.newPasswordConfirm) {
        setPasswordError("New passwords do not match.");
        passwordValid = false;
      }
    }

    if (!profileValid || !passwordValid) {
      setIsSubmitting(false);
      return null;
    }

    let profileSuccess = true;
    let passwordSuccess = true;

    if (hasProfileChanges()) {
      const profileResult = await updateProfile({
        name: formData.name,
        email: formData.email,
        phoneNumber: formData.phoneNumber,
      } as ProfileFormData);
      if (!profileResult.success) {
        setProfileError(profileResult.error);
        profileSuccess = false;
      }
    }

    if (hasPasswordChanges()) {
      const passwordResult = await updatePassword({
        currentPassword: formData.currentPassword,
        newPassword: formData.newPassword,
        newPasswordConfirm: formData.newPasswordConfirm,
      } as PasswordFormData);
      if (!passwordResult.success) {
        setPasswordError(passwordResult.error);
        passwordSuccess = false;
      }
    }

    setIsSubmitting(false);

    if (profileSuccess && passwordSuccess && (hasProfileChanges() || hasPasswordChanges())) {
      location.reload();
    }
  };

  return (
    <main
      className={`
                w-full my-4 sm:max-w-md max-w-xs p-4 sm:p-6
                bg-white rounded-xl shadow-2xl shadow-gray-600
                flex flex-col items-center
            `}
    >
      <h1 className="text-2xl font-semibold mb-4">Edit Profile</h1>
      <div className="h-8">{profileError && <p className="text-red-500 mb-4">{profileError}</p>}</div>
      <form onSubmit={handleSubmit} className="w-full">
        <h2 className="text-xl font-semibold mb-4">Profile data</h2>
        <div className="mb-4">
          <InputField name="name" label="Name" type="text" placeholder={formData.name} onChange={handleChange} />
        </div>
        <div className="mb-4">
          <InputField name="email" label="Email" type="email" placeholder={formData.email} onChange={handleChange} />
        </div>
        <div className="mb-8">
          <InputField name="phoneNumber" label="Phone Number" type="text" placeholder={formData.phoneNumber} onChange={handleChange} />
        </div>
        <h2 className="text-xl font-semibold mb-4">Password</h2>
        <div className="h-8">{passwordError && <p className="text-red-500 mb-4">{passwordError}</p>}</div>
        <div className="mb-4">
          <InputField name="currentPassword" label="Current Password" type="password" placeholder="Enter current password" onChange={handleChange} />
        </div>
        <div className="mb-4">
          <InputField name="newPassword" label="New Password" type="password" placeholder="Enter new password" onChange={handleChange} />
        </div>
        <div className="mb-8">
          <InputField name="confirmNewPassword" label="Confirm New Password" type="password" placeholder="Confirm new password" onChange={handleChange} />
        </div>
        <div className="flex justify-end gap-2">
          <Button type="button" onClick={() => router.push("/dashboard")} className="px-4 py-2 bg-gray-300 text-gray-700 hover:bg-gray-400" disabled={isSubmitting}>
            Cancel
          </Button>
          <Button type="submit" className="px-6 py-2" disabled={isSubmitting}>
            {isSubmitting ? "Saving..." : "Save"}
          </Button>
        </div>
      </form>
    </main>
  );
}
