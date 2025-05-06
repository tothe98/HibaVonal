"use client"

import MenuButton from "@/components/MenuButton"
import { logout } from "../actions"
import { useRouter } from "next/navigation"

interface ButtonConfig {
    id: string
    label: string
    onClick: () => void
    icon: React.ReactNode
}

interface Role {
    roleId: number
    name: string
}

interface User {
    name: string
    email: string
    roles: Role[]
}

interface DashboardClientPageProps {
    user: User
}

export default function DashboardClientPage({ user }: DashboardClientPageProps) {
    const router = useRouter()
    const roles = user.roles || []

    const ROLE_USER = 4
    const ROLE_WORKER = 3
    const ROLE_MANAGER = 2
    const ROLE_ADMIN = 1

    const userButtons: ButtonConfig[] = [
        {
            id: '1', label: 'My Issue List', onClick: () => router.push('/dashboard/issues'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M9 5H7C5.89543 5 5 5.89543 5 7V19C5 20.1046 5.89543 21 7 21H17C18.1046 21 19 20.1046 19 19V7C19 5.89543 18.1046 5 17 5H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 12H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 16H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M9 5C9 3.89543 9.89543 3 11 3H13C14.1046 3 15 3.89543 15 5V7H9V5Z" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <circle cx="9" cy="12" r="1" fill="#000000"></circle> <circle cx="9" cy="16" r="1" fill="#000000"></circle> </g></svg>
            )
        },
        {
            id: '2', label: 'Report Issue', onClick: () => router.push('/dashboard/report-issue'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeWidth="0"></g><g id="SVGRepo_iconCarrier"><path d="M4 12H20M12 4V20" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path></g></svg>
            )
        },
        {
            id: '3', label: 'Edit Profile', onClick: () => router.push('/dashboard/edit-profile'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M11 15C10.1183 15 9.28093 14.8098 8.52682 14.4682C8.00429 14.2315 7.74302 14.1131 7.59797 14.0722C7.4472 14.0297 7.35983 14.0143 7.20361 14.0026C7.05331 13.9914 6.94079 14 6.71575 14.0172C6.6237 14.0242 6.5425 14.0341 6.46558 14.048C5.23442 14.2709 4.27087 15.2344 4.04798 16.4656C4 16.7306 4 17.0485 4 17.6841V19.4C4 19.9601 4 20.2401 4.10899 20.454C4.20487 20.6422 4.35785 20.7951 4.54601 20.891C4.75992 21 5.03995 21 5.6 21H8.4M15 7C15 9.20914 13.2091 11 11 11C8.79086 11 7 9.20914 7 7C7 4.79086 8.79086 3 11 3C13.2091 3 15 4.79086 15 7ZM12.5898 21L14.6148 20.595C14.7914 20.5597 14.8797 20.542 14.962 20.5097C15.0351 20.4811 15.1045 20.4439 15.1689 20.399C15.2414 20.3484 15.3051 20.2848 15.4324 20.1574L19.5898 16C20.1421 15.4477 20.1421 14.5523 19.5898 14C19.0376 13.4477 18.1421 13.4477 17.5898 14L13.4324 18.1574C13.3051 18.2848 13.2414 18.3484 13.1908 18.421C13.1459 18.4853 13.1088 18.5548 13.0801 18.6279C13.0478 18.7102 13.0302 18.7985 12.9948 18.975L12.5898 21Z" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path></g></svg>
            )
        },
        {
            id: '4', label: 'Logout', onClick: logout, icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" transform="matrix(-1, 0, 0, 1, 0, 0)"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M15 4H18C19.1046 4 20 4.89543 20 6V18C20 19.1046 19.1046 20 18 20H15M8 8L4 12M4 12L8 16M4 12L16 12" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path></g></svg>
            )
        }
    ]

    const adminButtons: ButtonConfig[] = [
        {
            id: '5', label: 'Register User', onClick: () => router.push('/register-user'), icon: (
                < svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" ><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M9 5H7C5.89543 5 5 5.89543 5 7V19C5 20.1046 5.89543 21 7 21H17C18.1046 21 19 20.1046 19 19V7C19 5.89543 18.1046 5 17 5H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 12H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 16H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M9 5C9 3.89543 9.89543 3 11 3H13C14.1046 3 15 3.89543 15 5V7H9V5Z" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <circle cx="9" cy="12" r="1" fill="#000000"></circle> <circle cx="9" cy="16" r="1" fill="#000000"></circle> </g></svg>
            )
        },
        {
            id: '6', label: 'Manage Issues', onClick: () => router.push('/manage-issues'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeWidth="0"></g><g id="SVGRepo_iconCarrier"><path d="M4 12H20M12 4V20" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path></g></svg>
            )
        },
        {
            id: '7', label: 'Manage Users', onClick: () => router.push('/manage-users'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M11 15C10.1183 15 9.28093 14.8098 8.52682 14.4682C8.00429 14.2315 7.74302 14.1131 7.59797 14.0722C7.4472 14.0297 7.35983 14.0143 7.20361 14.0026C7.05331 13.9914 6.94079 14 6.71575 14.0172C6.6237 14.0242 6.5425 14.0341 6.46558 14.048C5.23442 14.2709 4.27087 15.2344 4.04798 16.4656C4 16.7306 4 17.0485 4 17.6841V19.4C4 19.9601 4 20.2401 4.10899 20.454C4.20487 20.6422 4.35785 20.7951 4.54601 20.891C4.75992 21 5.03995 21 5.6 21H8.4M15 7C15 9.20914 13.2091 11 11 11C8.79086 11 7 9.20914 7 7C7 4.79086 8.79086 3 11 3C13.2091 3 15 4.79086 15 7ZM12.5898 21L14.6148 20.595C14.7914 20.5597 14.8797 20.542 14.962 20.5097C15.0351 20.4811 15.1045 20.4439 15.1689 20.399C15.2414 20.3484 15.3051 20.2848 15.4324 20.1574L19.5898 16C20.1421 15.4477 20.1421 14.5523 19.5898 14C19.0376 13.4477 18.1421 13.4477 17.5898 14L13.4324 18.1574C13.3051 18.2848 13.2414 18.3484 13.1908 18.421C13.1459 18.4853 13.1088 18.5548 13.0801 18.6279C13.0478 18.7102 13.0302 18.7985 12.9948 18.975L12.5898 21Z" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path></g></svg>
            )
        },
        {
            id: '8', label: 'Manage Rooms', onClick: () => router.push('/manage-rooms'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" transform="matrix(-1, 0, 0, 1, 0, 0)"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M15 4H18C19.1046 4 20 4.89543 20 6V18C20 19.1046 19.1046 20 18 20H15M8 8L4 12M4 12L8 16M4 12L16 12" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path></g></svg>
            )
        }
    ]

    let buttons: ButtonConfig[] = []
    if (roles.some(role => role.roleId === ROLE_ADMIN)) {
        buttons = [...adminButtons]
    }
    if (roles.some(role => role.roleId === ROLE_USER || role.roleId === ROLE_ADMIN)) {
        buttons = [...buttons, ...userButtons]
    }

    return (
        <>
            <main
                className={`
                    w-full my-4 sm:max-w-md max-w-xs p-4 sm:p-6
                    bg-white rounded-xl shadow-2xl shadow-gray-600
                    grid grid-cols-1 gap-2 items-center
                    sm:gap-4 sm:grid-cols-2
                `}
            >
                {buttons.length < 1 ? (
                    <p className="col-span-1 sm:col-span-2 text-center py-4 text-gray-500">Your role is kind of ass :(</p>
                ) : (
                    buttons.map((btn) => (
                        <MenuButton key={btn.id} id={btn.id} label={btn.label} onClick={btn.onClick}>
                            {btn.icon}
                        </MenuButton>
                    ))
                )}
            </main>
        </>
    )
}
