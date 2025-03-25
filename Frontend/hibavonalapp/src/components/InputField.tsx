import { InputFieldProps } from "@/interfaces/InputFieldProps"

export default function InputField({ name, placeholder, label, type, onchange }: InputFieldProps) {

    return (
        <div>
            {label && <label className="block text-sm/6 font-medium text-gray-500">{label}</label>}
            <input
                className={`
                    block min-w-0 grow py-2 px-3 rounded-md border-[2.5px]
                    border-gray-500 hover:border-cyan-400 focus:border-cyan-400
                    text-base text-gray-900 placeholder:text-gray-500 focus:outline-none
                `}
                type={type}
                name={name}
                placeholder={placeholder}
                id={name + "-input-field-id"}
                onChange={onchange}
            />
        </div>
    )
}
