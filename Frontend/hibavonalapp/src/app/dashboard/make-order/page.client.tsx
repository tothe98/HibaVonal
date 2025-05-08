"use client"

import { useRouter } from 'next/navigation'

interface Role {
    roleId: number
    name: string
}

interface User {
    name: string
    email: string
    phoneNumber: string
    roles: Role[]
}

interface Props {
    user: User
}

export default function MakeOrderClientPage({ user }: Props) {
    const router = useRouter()

    return (
        <main
            className={`
        w-full sm:my-4 max-w-4xl p-4 sm:p-6 mx-auto
        bg-white rounded-none sm:rounded-xl shadow-2xl shadow-gray-600
        flex flex-col items-center
      `}
        >
            Make order
        </main>
    )
}
