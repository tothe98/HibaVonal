import { useFormStatus } from "react-dom"

export default function LoginButton() {
    const { pending } = useFormStatus()

    return (
        <button
            disabled={pending}
            type="submit"
            className={`
                block min-w-0 grow py-2 px-5 rounded-md border-[2.5px] bg-cyan-400
                border-cyan-400 hover:border-violet-500 focus:border-violet-500 hover:bg-violet-500
                text-white placeholder:text-gray-500 focus:outline-none
            `}
            id="login-button-id"
        >
            Login
        </button>
    )
}
