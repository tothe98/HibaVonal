import { cookies } from "next/headers"
import { NextRequest, NextResponse } from "next/server"
import { decrypt } from "./lib/session"

const protectedRoutes = ["/dashboard"]
const adminRoutes = ["/dashboard/register-user", "/dashboard/errortypes", "/dashboard/errortype-create", "/dashboard/equipments", "/dashboard/create-equipment"]
const managerRoutes = ["/dashboard/reports"]
const workerRoutes = ["/dashboard/assigned-issues", "/dashboard/work-orders"]
const userRoutes = ["/dashboard/report-issue", "/dashboard/issues", "/dashboard/update-issue"]
const publicRoutes = ["/"]

export default async function middleware(req: NextRequest) {
    const path = req.nextUrl.pathname

    const isProtectedRoute = protectedRoutes.some((route) => path.startsWith(route))
    const isAdminRoute = adminRoutes.some((route) => path.startsWith(route))
    const isManagerRoute = managerRoutes.some((route) => path.startsWith(route))
    const isWorkerRoute = workerRoutes.some((route) => path.startsWith(route))
    const isUserRoute = userRoutes.some((route) => path.startsWith(route))
    const isPublicRoute = publicRoutes.includes(path)

    const cookieStore = await cookies()
    const cookie = cookieStore.get("session")?.value

    if (!cookie) {
        if (isProtectedRoute) {
            return NextResponse.redirect(new URL("/", req.nextUrl))
        }
        return NextResponse.next()
    }

    const session = await decrypt(cookie)

    if (!session?.email && isProtectedRoute) {
        cookieStore.delete("session")
        return NextResponse.redirect(new URL("/", req.nextUrl))
    }

    if (isAdminRoute || isWorkerRoute || isManagerRoute || isUserRoute) {

        // Admin routes
        if (isAdminRoute && session?.role === '1') {
            return NextResponse.next()
        }

        // Manager routes
        if (isManagerRoute && session?.role === '2') {
            return NextResponse.next()
        }

        // Worker routes
        if (isWorkerRoute && session?.role === '3') {
            return NextResponse.next()
        }

        // User routes
        if (isUserRoute && session?.role === '4') {
            return NextResponse.next()
        }

        return NextResponse.redirect(new URL("/dashboard", req.nextUrl))
    }

    if (isPublicRoute && session?.email) {
        return NextResponse.redirect(new URL("/dashboard", req.nextUrl))
    }

    return NextResponse.next()
}
