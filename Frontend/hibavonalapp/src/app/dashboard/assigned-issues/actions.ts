"use server"

import { cookies } from 'next/headers'

interface Issue {
    id: number
    reportTime: string
    description: string
    comment: string | null
    status: string
    level: string
}


export async function fetchAssignedIssues(): Promise<{ success: boolean; error: string | null; data: Issue[] }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.', data: [] }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/ErrorLogs/ListCurrentAssigned`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        })

        if (!response.ok) {
            return { success: false, error: 'Failed to fetch assigned issues.', data: [] }
        }

        const data: Issue[] = await response.json()
        return { success: true, error: null, data }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.', data: [] }
    }
}

export async function updateIssue(
    issueId: number,
    status: number,
): Promise<{ success: boolean; error: string | null }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.' }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/ErrorLogs/Update/${issueId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify({ status }),
        })

        if (!response.ok) {
            const errorData = await response.json()
            return { success: false, error: errorData.message || 'Failed to update issue.' }
        }

        return { success: true, error: null }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.' }
    }
}
