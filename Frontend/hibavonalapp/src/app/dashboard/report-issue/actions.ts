"use server"

import { cookies } from 'next/headers'

interface ReportIssueFormData {
    description: string;
    roomNumber: string;
    level: number
}

interface APIResponse<T> {
    statusCode: number
    message: string | null
    data: T | null
}

export async function listRooms() {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.' }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Room/List`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        })

        const json: APIResponse<ReportIssueFormData> = await response.json()
        if (!response.ok || json.statusCode !== 200) {
            return { success: false, error: json.message || 'Failed to update profile.' }
        }

        console.log(json);
        console.log(response);
        return { json, success: true, error: null }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.' }
    }
}
