"use client"

import { useState, ChangeEventHandler } from 'react'
import { useRouter } from 'next/navigation'
import InputField from '@/components/InputField'
import Button from '@/components/Button'

interface ReportIssueFormData {
    description: string
    roomNumber: string
    level: number
}

interface APIResponse<T> {
    statusCode: number
    message: string | null
    data: T | null
}

interface Role {
    roleId: number
    name: string
}

interface User {
    id: number
    name: string
    email: string
    phoneNumber: string
    isDeleted: boolean
    roles: Role[]
}

interface Props {
    user: User
}

interface InputFieldProps {
    name: string
    placeholder?: string
    label?: string
    type: string
    onChange?: ChangeEventHandler<HTMLInputElement>
}

export default function ReportIssueClientPage({ user }: Props) {
    const router = useRouter()
    const [formData, setFormData] = useState<ReportIssueFormData>({
        description: '',
        roomNumber: '',
        level: 0
    })
    const [error, setError] = useState<string | null>(null)
    const [isSubmitting, setIsSubmitting] = useState(false)

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target
        setFormData((prev) => ({ ...prev, [name]: value }))
    }

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        setError(null)
        setIsSubmitting(true)

        if (!formData.description || !formData.roomNumber || formData.level == 0) {
            setError('All fields are required.')
            setIsSubmitting(false)
            return null
        }

    }

    return (
        <main
            className={`
                w-full sm:max-w-md max-w-xs p-4 sm:p-6 mb-4
                bg-white rounded-xl shadow-2xl shadow-gray-600
                flex flex-col items-center
          `}
        >
            <h1 className="text-2xl font-semibold mb-4">Report an Issue</h1>
            {error && <p className="text-red-500 mb-4">{error}</p>}
            <form onSubmit={handleSubmit} className="w-full">
                <div className="mb-4">
                    <label htmlFor="description" className="block ml-1 mb-2 text-md text-gray-700">
                        Description
                    </label>
                    <textarea
                        id="description"
                        name="description"
                        value={formData.description}
                        onChange={handleChange}
                        className={`
                            w-full min-w-64 min-h-10 py-4 px-3 rounded-md border-2 leading-tight
                            border-gray-500 hover:border-cyan-500 focus:border-cyan-500 text-base text-gray-900 placeholder:text-gray-500
                            focus:outline-none appearance-none
                        `}
                        placeholder="Describe the issue"
                        rows={4}
                        required
                    />
                </div>
                <div className="mb-4">
                    <InputField
                        name="roomNumber"
                        label="Room Number"
                        type="text"
                        placeholder="Enter room number"
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="flex justify-end gap-2">
                    <Button
                        type="button"
                        onClick={() => router.back()}
                        className="px-4 py-2 bg-gray-300 text-gray-700 hover:bg-gray-400"
                        disabled={isSubmitting}
                    >
                        Cancel
                    </Button>
                    <Button
                        type="submit"
                        className="px-4 py-2"
                        disabled={isSubmitting}
                    >
                        {isSubmitting ? 'Submitting...' : 'Submit'}
                    </Button>
                </div>
            </form>
        </main>
    )
}
