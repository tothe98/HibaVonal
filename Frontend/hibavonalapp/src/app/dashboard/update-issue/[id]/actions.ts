"use server"

import { cookies } from 'next/headers'

interface UpdateIssueFormData {
    description: string
    level: number
}

interface Room {
    id: number
    floor: number
    roomType: string
    number: number
    residents: any[]
    equipments: any[]
    dormitory: any | null
    dormitoryId: number
}

interface Role {
    roleId: number
    roleName: string
}

interface User {
    email: string
    name: string
    phoneNumber: string
    personalRoomId: number | null
    roles: Role[]
}

interface Issue {
    id: number
    reportTime: string
    description: string
    comment: string | null
    status: string
    level: string
    room: Room
    maintenanceWorker: User | null
    reporter: User
}

interface APIResponse<T> {
    statusCode: number
    message: string | null
    data: T | null
}

export async function fetchIssue(id: number): Promise<{ success: boolean; error: string | null; data: Issue[] }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.', data: [] }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/ErrorLogs/Get/id?id=${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        })

        if (!response.ok) {
            return { success: false, error: 'Failed to fetch issue.', data: [] }
        }

        const data: Issue[] = await response.json()
        return { success: true, error: null, data }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.', data: [] }
    }
}

export async function updateIssue(formData: UpdateIssueFormData, id: number) {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.' }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/ErrorLogs/UserUpdate/${id}`, {
            method: 'PUT',
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
