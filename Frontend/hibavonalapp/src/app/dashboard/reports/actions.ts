"use server"

import { cookies } from 'next/headers'

interface Issue {
    id: number
    reportTime: string
    description: string
    comment: string | null
    status: string
    level: string
    maintenanceWorker: MaintenanceWorker | null
}

interface MaintenanceWorker {
    id: number,
    email: string,
    name: string,
    phoneNumber: string
}

export async function fetchReports(): Promise<{ success: boolean; error: string | null; data: Issue[] }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.', data: [] }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/ErrorLogs/List`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        })

        if (!response.ok) {
            return { success: false, error: 'Failed to fetch issues.', data: [] }
        }

        const data: Issue[] = await response.json()
        return { success: true, error: null, data }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.', data: [] }
    }
}

export async function fetchMaintenanceWorkers(): Promise<{ success: boolean; error: string | null; data: MaintenanceWorker[] }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.', data: [] }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/User/GetMaintenanceWorkers`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        })

        if (!response.ok) {
            return { success: false, error: 'Failed to fetch workers.', data: [] }
        }

        const data: MaintenanceWorker[] = await response.json()
        return { success: true, error: null, data }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.', data: [] }
    }
}

export async function updateIssue(
    issueId: number,
    status: number,
    workerId?: number
): Promise<{ success: boolean; error: string | null }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value
    const body: any = {};
    if (status !== undefined) body.status = status;
    if (workerId !== undefined) body.workerId = workerId;

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
            body: JSON.stringify({ status, maintenanceWorkerId: workerId }),
        })

        const data = await response.json();

        if (!response.ok) {
            const errorData = data
            return { success: false, error: errorData.message || 'Failed to update issue.' }
        }

        return { success: true, error: null }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.' }
    }
}
