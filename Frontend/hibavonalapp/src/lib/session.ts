import "server-only"
import { jwtVerify } from "jose"
import { cookies } from "next/headers"

interface SessionPayloadProps {
    id: string
    name: string
    email: string
    expiresAt: Date
}

const secretKey = process.env.SESSION_SECRET
const encodedKey = new TextEncoder().encode(secretKey)

export async function createSession(token: string, expiresAt: Date) {
    const cookieStore = await cookies()
    const expiresDate = new Date(expiresAt)

    cookieStore.set({
        name: "session",
        value: token,
        httpOnly: true,
        secure: true,
        sameSite: 'strict',
        expires: expiresDate,
    })
}

export async function deleteSession() {
    const cookieStore = await cookies()
    cookieStore.delete("session")
}

export async function decrypt(session: string | undefined = '') {
    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/Auth/ValidateToken`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${session}`
            }
        })

        if (response.status === 204) {
            const { payload } = await jwtVerify(session, encodedKey, {
                algorithms: ['HS256'],
            })
            return payload
        }

        if (response.status === 401) {
            console.log("Unauthorized")
        }

    } catch (error) {
        console.log("Failed to verify session: ", error)
        return null
    }
}

interface APIResponse<T> {
    statusCode: number
    message: string | null
    data: T | null
}

interface User {
    id: string
    name: string
    email: string
    phoneNumber: string
    personalRoomId: number
    roles: number[]
}

export async function fetchCurrentUser(token: string): Promise<User | null> {
    try {
        const response = await fetch(`${process.env.NEXT_PUBLIC_BACKEND_URL}/api/User/GetCurrent`, {
            method: "GET",
            headers: { Authorization: `Bearer ${token}` },
            cache: 'no-store',
        })

        if (!response.ok) {
            throw new Error(`Failed to fetch user: ${response.status}`)
        }

        const json: APIResponse<User> = await response.json()
        if (json.statusCode !== 200 || !json.data) {
            throw new Error(`API error: ${json.message || 'Unknown error'}`)
        }

        return json.data

    } catch (error) {
        console.error('Error fetching user data: ', error)
        return null
    }
}
