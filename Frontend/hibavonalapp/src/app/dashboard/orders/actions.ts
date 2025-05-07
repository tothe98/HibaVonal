"use server"

import { cookies } from 'next/headers'

interface Order {
    id: number
    date: string
    totalAmount: number
    status: string
    items: [
        {
            id: number
            quantity: number
            price: number
            equipment: {
                id: number
                name: string
                errorType: {
                    id: number
                    name: string
                }
            }
            orderId: number
        }
    ]
}

export async function fetchOrders(): Promise<{ success: boolean; error: string | null; data: Order[] }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.', data: [] }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Order/List`, {
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

        let data: Order[]
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

export async function updateOrderStatus(
    orderId: number,
    status: number
): Promise<{ success: boolean; error: string | null }> {
    const cookieStore = await cookies()
    const token = cookieStore.get('session')?.value

    if (!token) {
        return { success: false, error: 'No session token found.' }
    }

    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Order/UpdateStatus/${orderId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify({ status }),
        })

        if (!response.ok) {
            const errorText = await response.text()
            return { success: false, error: `Failed to update order status: ${response.status} ${errorText}` }
        }

        return { success: true, error: null }
    } catch (error) {
        return { success: false, error: error instanceof Error ? error.message : 'An error occurred.' }
    }
}
