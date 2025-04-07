import { InputFieldProps } from "@/interfaces/InputFieldProps"

export default function InputField({ name, placeholder, label, type, onChange }: InputFieldProps) {
    const inputId = name ? `input-field-${name}` : undefined

    return (
        <div>
            {label && <label htmlFor={inputId} className="block ml-1 mb-2 text-md text-gray-700">{label}</label>}
            <input
                className={`
                    w-full min-w-64 py-4 px-3 rounded-md border-2 leading-tight
                    border-gray-500 hover:border-cyan-500 focus:border-cyan-500 text-base text-gray-900 placeholder:text-gray-500 duration-200
                    focus:outline-none appearance-none
                `}
                type={type}
                name={name}
                placeholder={placeholder}
                id={inputId}
                onChange={onChange}
            />
        </div>
    )
}
