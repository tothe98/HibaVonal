"use server"

import { cookies } from 'next/headers'

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

interface Equipment {
    id: number;
    name: string;
    errorType: errorType;
}

interface errorType {
    id: number;
    name: string;
}

export async function fetchEquipments(): Promise<{ success: boolean; error: string | null; data: Equipment[] }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.', data: [] }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Equipment/List`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        })

        if (!response.ok) {
            return { success: false, error: 'Failed to fetch equipments.', data: [] }
        }

        const data: Equipment[] = await response.json()
        return { success: true, error: null, data }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.', data: [] }
    }
}

export async function deleteEquipment(id: number): Promise<{ success: boolean; error: string | null; data: Equipment[] }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.', data: [] }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Equipment/Delete/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        })

        if (!response.ok) {
            return { success: false, error: 'Failed to delete equipment.', data: [] }
        }

        const data: Equipment[] = await response.json()
        return { success: true, error: null, data }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.', data: [] }
    }
}