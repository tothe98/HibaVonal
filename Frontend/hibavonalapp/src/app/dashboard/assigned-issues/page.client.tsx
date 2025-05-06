"use client"

import { useRouter } from 'next/navigation'

interface Role {
    roleId: number
    name: string
}

interface User {
    id: number
    name: string
    email: string
    phoneNumber: string
    roles: Role[]
}

interface Props {
    user: User
}

export default function AssignedIssuesClientPage({ user }: Props) {
    const router = useRouter()

    return (
        <main
            className={`
                w-full my-4 sm:max-w-md max-w-xs p-4 sm:p-6
                bg-white rounded-xl shadow-2xl shadow-gray-600
                flex flex-col items-center
            `}
        >
            <h1 className="text-2xl font-semibold mb-4">Assigned Issues</h1>
        </main>
    )
}
