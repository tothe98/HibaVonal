"use client"

import { useState, useEffect, useMemo, useRef } from 'react'
import { useRouter } from 'next/navigation'
import Button from '@/components/Button'
import { fetchOrders, updateOrderStatus } from './actions'

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

interface Props {
    user: User
}

const statusOptions = [
    { label: 'Declined', value: 0 },
    { label: 'Accepted', value: 1 },
    { label: 'InProgress', value: 2 },
    { label: 'InStock', value: 3 },
]

export default function OrdersClientPage({ user }: Props) {
    const router = useRouter()
    const [orders, setOrders] = useState<Order[]>([])
    const [error, setError] = useState<string | null>(null)
    const [sortKey, setSortKey] = useState<keyof Order | null>(null)
    const [sortOrder, setSortOrder] = useState<'asc' | 'desc'>('asc')
    const triggerRefs = useRef<Map<number, HTMLSpanElement>>(new Map())

    useEffect(() => {
        const loadOrders = async () => {
            const result = await fetchOrders()
            if (!result.success) {
                setError(result.error)
                return
            }
            setOrders(result.data)
        }
        loadOrders()
    }, [])

    const handleSort = (key: keyof Order) => {
        if (sortKey === key) {
            setSortOrder(prev => (prev === 'asc' ? 'desc' : 'asc'))
        } else {
            setSortKey(key)
            setSortOrder('asc')
        }
    }

    const handleStatusChange = async (order: Order, newStatusValue: number) => {
        const result = await updateOrderStatus(order.id, newStatusValue)
        if (!result.success) {
            setError(result.error)
            return
        }

        const newLabel = statusOptions.find(opt => opt.value === newStatusValue)?.label || order.status

        setOrders(prev =>
            prev.map(o => (o.id === order.id ? { ...o, status: newLabel } : o))
        )
    }

    const sortedOrders = useMemo(() => {
        if (!sortKey) return orders

        return [...orders].sort((a, b) => {
            const aVal = a[sortKey]
            const bVal = b[sortKey]

            if (typeof aVal === 'string' && typeof bVal === 'string') {
                return sortOrder === 'asc' ? aVal.localeCompare(bVal) : bVal.localeCompare(aVal)
            }

            return sortOrder === 'asc' ? (aVal as number) - (bVal as number) : (bVal as number) - (aVal as number)
        })
    }, [orders, sortKey, sortOrder])

    const formatDate = (dateString: string) => {
        return new Date(dateString).toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'short',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit',
        })
    }

    const renderHeader = (label: string, key: keyof Order) => (
        <th className="p-2 cursor-pointer select-none" onClick={() => handleSort(key)}>
            <span className="flex items-center">
                {label}
                <svg
                    className={`w-4 h-4 ml-1 transition-transform ${sortKey === key && sortOrder === 'desc' ? 'rotate-180' : ''}`}
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                >
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="m8 15 4 4 4-4m0-6-4-4-4 4" />
                </svg>
            </span>
        </th>
    )

    return (
        <main
            className={`
        w-full sm:my-4 max-w-4xl p-4 sm:p-6 mx-auto
        bg-white rounded-none sm:rounded-xl shadow-2xl shadow-gray-600
        flex flex-col items-center
      `}
        >
            <div className="flex justify-between items-center w-full mb-4">
                <Button
                    type="button"
                    onClick={() => router.push('/dashboard')}
                    className="px-4 py-2 bg-gray-300 text-gray-700 hover:bg-gray-400"
                >
                    Back
                </Button>
                <h1 className="text-2xl font-semibold">Orders</h1>
                <div></div>
            </div>

            <div className="h-8">{error && <p className="text-red-500 mb-4">{error}</p>}</div>

            <div className="w-full overflow-x-auto">
                <table className="w-full text-left">
                    <thead>
                        <tr className="text-gray-700 border-b">
                            {renderHeader('Date', 'date')}
                            {renderHeader('Total Amount', 'totalAmount')}
                            <th className="p-2">Status</th>
                            <th className="p-2">Items</th>
                        </tr>
                    </thead>
                    <tbody>
                        {sortedOrders.length === 0 ? (
                            <tr>
                                <td colSpan={4} className="p-4 text-center text-gray-500">
                                    No orders found.
                                </td>
                            </tr>
                        ) : (
                            sortedOrders.map((order) => {
                                const handleMouseEnter = (e: React.MouseEvent<HTMLSpanElement>) => {
                                    const trigger = e.currentTarget
                                    const rect = trigger.getBoundingClientRect()
                                    const popup = trigger.nextElementSibling as HTMLElement
                                    if (popup) {
                                        popup.style.top = `${rect.bottom + window.scrollY + 8}px`
                                        popup.style.left = `${rect.left + window.scrollX}px`
                                    }
                                }

                                return (
                                    <tr key={order.id} className="border-t hover:bg-gray-50">
                                        <td className="p-2">{formatDate(order.date)}</td>
                                        <td className="p-2">${order.totalAmount.toFixed(2)}</td>
                                        <td className="p-2">
                                            <select
                                                value={statusOptions.find(opt => opt.label === order.status)?.value ?? ''}
                                                onChange={(e) => handleStatusChange(order, parseInt(e.target.value))}
                                                className="border rounded px-2 py-1"
                                            >
                                                {statusOptions.map(({ label, value }) => (
                                                    <option key={value} value={value}>{label}</option>
                                                ))}
                                            </select>
                                        </td>
                                        <td className="p-2 relative group">
                                            <span
                                                className="text-blue-600 cursor-pointer underline"
                                                onMouseEnter={handleMouseEnter}
                                                ref={(el) => {
                                                    if (el) triggerRefs.current.set(order.id, el)
                                                    else triggerRefs.current.delete(order.id)
                                                }}
                                            >
                                                View Items ({order.items.length})
                                            </span>
                                            <div
                                                className="fixed hidden group-hover:block bg-white border border-gray-300 rounded-lg shadow-lg p-4 z-50 min-w-[350px] max-h-[300px] overflow-y-auto"
                                            >
                                                <h4 className="font-semibold text-lg mb-3">Order Items</h4>
                                                {order.items.length === 0 ? (
                                                    <p className="text-gray-500">No items in this order.</p>
                                                ) : (
                                                    <div className="space-y-3">
                                                        {order.items.map((item) => (
                                                            <div key={item.id} className="border-b pb-2 last:border-b-0">
                                                                <div className="grid grid-cols-2 gap-2">
                                                                    <p className="font-medium">Equipment:</p>
                                                                    <p>{item.equipment.name}</p>
                                                                    <p className="font-medium">Quantity:</p>
                                                                    <p>{item.quantity}</p>
                                                                    <p className="font-medium">Price:</p>
                                                                    <p>${item.price.toFixed(2)}</p>
                                                                    <p className="font-medium">Error Type:</p>
                                                                    <p>{item.equipment.errorType ? item.equipment.errorType.name : 'None'}</p>
                                                                </div>
                                                            </div>
                                                        ))}
                                                    </div>
                                                )}
                                            </div>
                                        </td>
                                    </tr>
                                )
                            })
                        )}
                    </tbody>
                </table>
            </div>
        </main>
    )
}
