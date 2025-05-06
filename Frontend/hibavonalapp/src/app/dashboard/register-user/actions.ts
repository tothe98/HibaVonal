"use server"

import { cookies } from 'next/headers'
import { z } from "zod"

interface RegisterFormData {
    name: string;
    email: string;
    password: string;
    passwordConfirm: string;
    phoneNumber: string;
    roleId: number;
}

interface RegisterFormDataWithRoom {
    name: string;
    email: string;
    password: string;
    passwordConfirm: string;
    phoneNumber: string;
    roleId: number;
    roomId: number;
}

interface Roles {
    id: number
    roleName: string
}

interface APIResponse<T> {
    statusCode: number
    message: string | null
    data: T | null
}

function isValidEmail(email: string): boolean {
    const emailRegex =
        /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailRegex.test(email);
}

export async function registerUserWithRoom(formData: RegisterFormDataWithRoom) {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.' }
    }

    if (!isValidEmail(formData.email)) {
        return { success: false, error: 'Invalid email format.' }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Auth/Registration`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(formData),
        })

        //const json: APIResponse<null> = await response.json()
        const json = await response.json();
        console.log('Register error response:', json);
        if (!response.ok || json.statusCode !== 200) {
            console.log(response);
            return { success: false, error: json.message || 'Failed to register user.' }
        }

        return { success: true, error: null }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.' }
    }
}

export async function registerUser(formData: RegisterFormData) {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.' }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Auth/Registration`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(formData),
        })

        const json = await response.json();

        console.log('Register error response:', json);
        if (!response.ok || json.statusCode !== 200) {
            console.log(response);
            return { success: false, error: json.message || 'Failed to register user.' }
        }

        return { success: true, error: null }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.' }
    }
}

export async function fetchRoles(): Promise<{ success: boolean; error: string | null; data: Roles[] }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.', data: [] }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Auth/ListRoles`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        })

        if (!response.ok) {
            return { success: false, error: 'Failed to fetch issues.', data: [] }
        }

        const data: Roles[] = await response.json()

        return { success: true, error: null, data }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.', data: [] }
    }
}
