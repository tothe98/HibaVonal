import { MouseEventHandler } from "react"

export interface MenuButtonProps {
    id: string
    label?: string
    onClick: MouseEventHandler<HTMLButtonElement>
}
