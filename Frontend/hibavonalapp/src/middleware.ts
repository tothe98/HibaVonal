import { cookies } from "next/headers"
import { NextRequest, NextResponse } from "next/server"
import { decrypt } from "./lib/session"

const protectedRoutes = ["/dashboard"]
const publicRoutes = ["/"]

export default async function middleware(req: NextRequest) {
    const path = req.nextUrl.pathname

    const isProtectedRoute = protectedRoutes.some((route) => path.startsWith(route))
    const isPublicRoute = publicRoutes.includes(path)

    const cookieStore = await cookies()
    const cookie = cookieStore.get("session")?.value

    const session = await decrypt(cookie)

    // Redirects to login page when trying to access a protected route
    if (isProtectedRoute && !session?.email) {
        cookieStore.delete("session")
        return NextResponse.redirect(new URL("/", req.nextUrl))
    }

    // Basically reverse so a user cannot access login page while already logged in
    if (isPublicRoute && session?.email) {
        return NextResponse.redirect(new URL("/dashboard", req.nextUrl))
    }

    return NextResponse.next()
}
