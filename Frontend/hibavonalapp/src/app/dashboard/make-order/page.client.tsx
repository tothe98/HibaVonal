'use client'

import { useEffect, useState } from 'react'
import { useRouter } from 'next/navigation'
import { createOrder, fetchEquipmemt } from './action'
import InputField from '@/components/InputField'
import Button from '@/components/Button'

interface Equipment {
    id: number
    name: string
}

interface OrderItem {
    equipmentId: number
    quantity: number
    price: number
}

interface Role {
    roleId: number;
    name: string;
}

interface User {
    name: string;
    email: string;
    roles: Role[];
}

interface Props {
    user: User
}

export default function MakeOrderClientPage({ user }: Props) {
    const router = useRouter()
    const [equipments, setEquipments] = useState<Equipment[]>([])
    const [orderItems, setOrderItems] = useState<OrderItem[]>([])
    const [error, setError] = useState<string | null>(null)

    const roles = user.roles || [];

    useEffect(() => {
        const loadEquipments = async () => {
            const result = await fetchEquipmemt()
            if (!result.success) {
                console.error(result.error)
                return null
            }
            setEquipments(result.data)
        }

        loadEquipments()
    }, [])

    const handleItemChange = (
        index: number,
        field: keyof OrderItem,
        value: number
    ) => {
        const updatedItems = [...orderItems]
        updatedItems[index][field] = value
        setOrderItems(updatedItems)
    }

    const addOrderItem = () => {
        setOrderItems([
            ...orderItems,
            { equipmentId: equipments[0]?.id ?? 0, quantity: 1, price: 0 }
        ])
    }

    const removeOrderItem = (index: number) => {
        const updatedItems = orderItems.filter((_, i) => i !== index)
        setOrderItems(updatedItems)
    }

    const handleSubmit = async () => {
        const result = await createOrder(orderItems);
        if (!result.success) {
            setError(result.error)
            return null
        }

        if (roles.some((role) => role.roleId === 2)) {
            router.push('/dashboard/orders')
        } else {
            router.push('/dashboard')
        }
    }

    return (
        <main
            className={`
                w-full sm:my-4 max-w-4xl p-4 sm:p-6 mx-auto
                bg-white rounded-none sm:rounded-xl shadow-2xl shadow-gray-600
                flex flex-col items-center
            `}
        >
            <h1 className="text-2xl font-semibold mb-4">Make Order</h1>

            <div className="h-8">
                {error && <p className="text-red-500 mb-4">{error}</p>}
            </div>

            {orderItems.map((item, i) => (
                <div key={i} className="flex flex-col gap-2 sm:flex-row sm:gap-4 mb-4 w-full">
                    <select
                        id={`${i}-s`}
                        value={item.equipmentId}
                        onChange={(e) =>
                            handleItemChange(i, 'equipmentId', parseInt(e.target.value))
                        }
                        className="w-full sm:flex-1 border rounded px-2 py-1"
                    >
                        {equipments.map((equipment) => (
                            <option key={equipment.id} value={equipment.id}>
                                {equipment.name}
                            </option>
                        ))}
                    </select>

                    <InputField
                        type="number"
                        name="Quantity"
                        placeholder="Quantity"
                        min={1}
                        max={100}
                        value={item.quantity + ""}
                        onChange={(e) =>
                            handleItemChange(i, 'quantity', parseInt(e.target.value))
                        }
                        required
                        className="w-full sm:flex-1"
                    />

                    <InputField
                        type="number"
                        name="UnitPrice"
                        placeholder="Unit Price"
                        min={1}
                        value={item.price + ""}
                        onChange={(e) =>
                            handleItemChange(i, 'price', parseInt(e.target.value))
                        }
                        required
                        className="w-full sm:flex-1"
                    />

                    <Button
                        id={`${i}-b`}
                        type="button"
                        onClick={() => removeOrderItem(i)}
                        className="bg-err-high hover:bg-err-high-h w-full sm:w-auto px-4 py-2"
                    >
                        -
                    </Button>
                </div>
            ))}

            <Button type="button" onClick={addOrderItem} className="mb-4 w-full sm:w-auto">
                +
            </Button>
            <div className="flex justify-between items-center w-full">
                <Button
                    type="button"
                    onClick={() => router.push('/dashboard')}
                    className="px-4 py-2 bg-gray-300 text-gray-700 hover:bg-gray-400"
                >
                    Cancel
                </Button>
                <Button type="button" onClick={handleSubmit}>
                    Submit
                </Button>
            </div>
        </main>
    )
}
