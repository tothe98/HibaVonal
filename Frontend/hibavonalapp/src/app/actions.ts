"use server"

import { z } from "zod"
import { createSession, deleteSession } from "@/lib/session"
import { redirect } from "next/navigation"

const loginSchema = z.object({
    email: z.string().email({ message: "Invalid email or password" }).trim(),
    password: z.string().min(8, { message: "Password must be at least 8 characters" }).trim()
})

export async function login(prevState: any, formData: FormData) {
    const result = loginSchema.safeParse(Object.fromEntries(formData))

    if (!result.success) {
        return {
            errors: result.error.flatten().fieldErrors
        }
    }

    const { email, password } = result.data

    const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Auth/LoginRequest`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
        credentials: "include",
    })

    if (!response.ok) {
        if (response.status === 401) {
            return { errors: { email: ["Invalid email or password"] } }
        }
        return { errors: { email: ["Server error, please try again later"] } }
    }

    const data = await response.json()

    await createSession(data?.data.token, data?.data.expiresAt)

    redirect("/dashboard")
}

export async function logout() {
    await deleteSession()

    redirect("/")
}
