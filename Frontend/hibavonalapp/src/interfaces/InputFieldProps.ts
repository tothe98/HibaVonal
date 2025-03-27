import { ChangeEventHandler } from "react";

export interface InputFieldProps {
    name: string
    placeholder?: string
    label?: string
    type: string
    onChange?: ChangeEventHandler<HTMLInputElement>
}
