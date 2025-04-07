import { MouseEventHandler } from "react"

export interface ButtonProps {
    className?: string
    name: string
    value?: string
    onClick: MouseEventHandler<HTMLButtonElement>
}
