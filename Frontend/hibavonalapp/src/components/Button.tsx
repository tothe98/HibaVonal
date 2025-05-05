interface ButtonProps {
    id?: string
    type: "submit" | "reset" | "button" | undefined
    disabled?: boolean
    onClick?: () => void
    className?: string
}

export default function Button({ id, type, disabled, onClick, className, children }: ButtonProps & Readonly<{ children: React.ReactNode }>) {
    return (
        <button
            id={id}
            type={type}
            disabled={disabled}
            onClick={onClick}
            className={`
                py-3 px-6
                rounded-md bg-cyan-400 font-bold hover:bg-cyan-500 text-white select-none duration-200
                focus:outline-none focus:shadow-outline ${className}
            `}
        >
            {children}
        </button>
    )
}
