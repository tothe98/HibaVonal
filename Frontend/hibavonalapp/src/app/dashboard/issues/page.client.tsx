"use client"

import Button from '@/components/Button'

interface Role {
    roleId: number
    name: string
}

interface User {
    name: string
    email: string
    roles: Role[]
}

interface IssuesClientProps {
    user: User
}

export default function IssuesClientPage({ user }: IssuesClientProps) {

    return (
        <>
            <main
                className={`
                    w-full sm:max-w-md max-w-xs p-4 sm:p-6 mb-4
                    bg-white rounded-xl shadow-2xl shadow-gray-600
                    flex flex-col items-center
                `}
            >
                <h1 className="text-2xl font-semibold mb-4">Issues</h1>

                <table id="sorting-table">
                    <thead>
                        <tr>
                            <th>
                                <span className="flex items-center">
                                    reported
                                    <svg className="w-4 h-4 ms-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                        <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m8 15 4 4 4-4m0-6-4-4-4 4" />
                                    </svg>
                                </span>
                            </th>
                            <th>
                                <span className="flex items-center">
                                    description
                                    <svg className="w-4 h-4 ms-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                        <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m8 15 4 4 4-4m0-6-4-4-4 4" />
                                    </svg>
                                </span>
                            </th>
                            <th>
                                <span className="flex items-center">
                                    status
                                    <svg className="w-4 h-4 ms-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                        <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m8 15 4 4 4-4m0-6-4-4-4 4" />
                                    </svg>
                                </span>
                            </th>
                            <th>
                                <span className="flex items-center">
                                    level
                                    <svg className="w-4 h-4 ms-1" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                        <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="m8 15 4 4 4-4m0-6-4-4-4 4" />
                                    </svg>
                                </span>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td className="font-medium text-gray-900 whitespace-nowrap dark:text-white">Switzerland</td>
                            <td>$741 billion</td>
                            <td>9 million</td>
                            <td>$82333</td>
                        </tr>
                        <tr>
                            <td className="font-medium text-gray-900 whitespace-nowrap dark:text-white">Switzerland</td>
                            <td>$741 billion</td>
                            <td>9 million</td>
                            <td>$82333</td>
                        </tr>
                        <tr>
                            <td className="font-medium text-gray-900 whitespace-nowrap dark:text-white">Switzerland</td>
                            <td>$741 billion</td>
                            <td>9 million</td>
                            <td>$82333</td>
                        </tr>
                    </tbody>
                </table>

                <Button
                    type={undefined}
                    className="px-4 py-2 rounded"
                >
                    Report Issue
                </Button>
            </main>
        </>
    )
}
