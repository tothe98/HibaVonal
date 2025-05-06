"use client"

import { useActionState } from "react"
import { login } from "./actions"

import InputField from "@/components/InputField"
import LoginButton from "@/components/LoginButton"

export default function LoginClientPage() {
    const [state, loginAction] = useActionState(login, undefined)

    return (
        <form
            action={loginAction}
            className={`
                    w-full sm:max-w-sm max-w-xs px-8 pt-10 pb-12 mb-4
                    bg-white rounded-xl shadow-2xl shadow-gray-600
                `}
        >
            <h1 className="select-none sm:text-[2.5rem] text-4xl text-center font-bold mb-12 pb-2">
                Dorm Manager
            </h1>
            <div className="flex flex-col items-center mb-4">
                <InputField
                    name="email"
                    placeholder="Email"
                    type="email"
                />
            </div>

            <div className="flex flex-col items-center mb-2">
                <InputField
                    name="password"
                    placeholder="Password"
                    type="password"
                />
            </div>
            <div className="flex flex-col items-center h-10">
                {state?.errors.email && <p className="text-rose-600">{state.errors.email}</p>}
            </div>

            <div className="mx-8 flex sm:flex-row flex-col-reverse items-center justify-between">
                <LoginButton />
                <a className="select-none sm:mb-0 mb-4 inline-block align-baseline font-bold text-sm text-gray-500 hover:text-cyan-500 duration-200" href="#">
                    Forgot Password?
                </a>
            </div>
        </form>
    )
}
