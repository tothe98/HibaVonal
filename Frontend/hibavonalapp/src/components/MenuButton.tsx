import { MenuButtonProps } from "@/interfaces/MenuButtonProps"

export default function MenuButton(props: Readonly<{ children: React.ReactNode } & MenuButtonProps>) {
    const { children, id, label, onClick } = props

    return (
        <button
            id={id ? `menu-button-${id}` : undefined}
            className={`
                sm:block grow sm:px-8 sm:pb-6 sm:pt-10 p-4 px-6 justify-items-center flex items-center
                rounded-md bg-white hover:bg-slate-100 text-black select-none shadow shadow-gray-300 duration-200
                focus:outline-none focus:shadow-outline
            `}
            onClick={onClick}
        >
            {children}
            {label && <p className="sm:mt-2 sm:ml-0 ml-9 text-md text-gray-500">{label}</p>}
        </button>
    )
}
