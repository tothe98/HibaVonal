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

export default function MakeOrderClientPage() {
    const router = useRouter()
    const [equipments, setEquipments] = useState<Equipment[]>([])
    const [orderItems, setOrderItems] = useState<OrderItem[]>([])
    const [error, setError] = useState<string | null>(null)

    useEffect(() => {
        const loadEquipments = async () => {
            const result = await fetchEquipmemt()
            if (!result.success) {
                console.error(result.error)
                return
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
        router.push('/dashboard')

    }

    return (
        <main className="w-full sm:my-4 max-w-4xl p-4 sm:p-6 mx-auto bg-white rounded-none sm:rounded-xl shadow-2xl shadow-gray-600 flex flex-col items-center">
            <div className="flex justify-between items-center w-full mb-4">
                <Button
                    type="button"
                    onClick={() => router.push('/dashboard')}
                    className="px-4 py-2 bg-gray-300 text-gray-700 hover:bg-gray-400"
                >
                    Back
                </Button>
                <h1 className="text-2xl font-semibold">Make Order</h1>
                <div></div>
            </div>
            <div className="h-8">
                {error && <p className="text-red-500 mb-4">{error}</p>}
            </div>

            {orderItems.map((item, index) => (
                <div key={index} className="flex flex-row gap-4 mb-2">
                    <select
                        value={item.equipmentId}
                        onChange={(e) =>
                            handleItemChange(index, 'equipmentId', parseInt(e.target.value))
                        }
                        className="border rounded px-2 py-1"
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
                            handleItemChange(index, 'quantity', parseInt(e.target.value))
                        }
                        required
                    />

                    <InputField
                        type="number"
                        name="UnitPrice"
                        placeholder="Unit Price"
                        min={1}
                        value={item.price + ""}
                        onChange={(e) =>
                            handleItemChange(index, 'price', parseInt(e.target.value))
                        }
                        required
                    />

                    <Button type="button" onClick={() => removeOrderItem(index)}>
                        -
                    </Button>
                </div>
            ))}

            <Button type="button" onClick={addOrderItem} className="mb-4">
                + Add item
            </Button>

            <Button type="button" onClick={handleSubmit}>
                Make an order
            </Button>
        </main>
    )
}
