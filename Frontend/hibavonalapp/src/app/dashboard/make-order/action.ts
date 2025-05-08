"use server"

import { cookies } from 'next/headers'

interface Equipment {
    id: number
    name: string
}

interface OrderItem {
    equipmentId: number
    quantity: number
    price: number
}



export async function fetchEquipmemt(): Promise<{ success: boolean; error: string | null; data: Equipment[] }> {
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
            const errorText = await response.text()
            return { success: false, error: `Failed to fetch orders: ${response.status} ${errorText}`, data: [] }
        }

        let data: Equipment[]
        try {
            data = await response.json()
        } catch (jsonError) {
            return { success: false, error: 'Invalid JSON response from server.', data: [] }
        }

        if (!Array.isArray(data)) {
            return { success: false, error: 'Response is not an array.', data: [] }
        }

        return { success: true, error: null, data }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.', data: [] }
    }
}

export async function createOrder(orderItems: OrderItem[]): Promise<{ success: boolean; error: string | null }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.' }
    }

    if (!orderItems || orderItems.length === 0) {
        return { success: false, error: 'No order items provided.' }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Order/Create`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify({ status: 2, items: orderItems }),
        })

        if (!response.ok) {
            const errorText = await response.text()
            return { success: false, error: `Failed to create order: ${response.status} ${errorText}` }
        }

        return { success: true, error: null }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.' }
    }
}