"use server"

import { cookies } from 'next/headers'

interface Role {
    roleId: number
    roleName: string
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

export async function fetchIssues(): Promise<{ success: boolean; error: string | null; data: Issue[] }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.', data: [] }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/ErrorLogs/ListCurrent`, {
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
