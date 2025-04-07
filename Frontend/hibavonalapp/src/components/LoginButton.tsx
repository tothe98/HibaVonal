import { useFormStatus } from "react-dom"

export default function LoginButton() {
    const { pending } = useFormStatus()

    return (
        <button
            disabled={pending}
            type="submit"
            className={`
                py-3 px-6
                rounded-md bg-cyan-400 font-bold hover:bg-cyan-500 text-white select-none duration-200
                focus:outline-none focus:shadow-outline
            `}
            id="login-button-id"
        >
            Login
        </button>
    )
}
