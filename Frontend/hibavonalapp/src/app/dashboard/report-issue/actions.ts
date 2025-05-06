"use server"

import { cookies } from 'next/headers'

interface ReportIssueFormData {
    description: string
    level: number
    roomId: number
}

interface APIResponse<T> {
    statusCode: number
    message: string | null
    data: T | null
}

export async function reportIssue(formData: ReportIssueFormData) {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.' }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/ErrorLogs/Create`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(formData),
        })

        const json: APIResponse<null> = await response.json()
        if (!response.ok || json.statusCode !== 200) {
            return { success: false, error: json.message || 'Failed to report issue.' }
        }

        return { success: true, error: null }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.' }
    }
}
