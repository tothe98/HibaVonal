import "server-only"
import { jwtVerify } from "jose"
import { cookies } from "next/headers"

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
                "Authorization": `Bearer ${session}`
            }
        })

        if (response.status === 204) {
            const { payload } = await jwtVerify(session, encodedKey, {
                algorithms: ['HS256'],
            })
            // delete log in production along with status 401
            console.log("Authorized")
            return payload
        }

        if (response.status === 401) {
            console.log("Unauthorized")
        }

    } catch (error) {
        console.log("Failed to verify session")
    }
}


