"use client"

import { useActionState } from "react"
import { login } from "./actions"

import InputField from "@/components/InputField"
import LoginButton from "@/components/LoginButton"

export default function loginPage() {
    const [state, loginAction] = useActionState(login, undefined)

    return (
        <div className={`
                bg-gradient-to-tr from-cyan-600 via-cyan-400 to-white
                flex flex-col justify-center items-center min-h-screen
                sm:p-20 text-black font-[family-name:var(--font-geist-sans)]
            `}
        >
            <form
                action={loginAction}
                className={`
                    bg-white rounded-xl py-10 px-20 shadow-2xl shadow-gray-600
                    flex flex-col gap-2 items-center row-start-2 sm:items-start
                `}
            >
                <h1 className="text-4xl text-center font-bold mb-4">
                    Dorm Manager
                </h1>
                <br />
                <br />
                <InputField
                    name="email"
                    placeholder="Email"
                    type="email"
                />
                <br />
                <InputField
                    name="password"
                    placeholder="Password"
                    type="password"
                />
                {state?.errors.email && <p style={{ color: "red" }}>{state.errors.email}</p>}
                {state?.errors.password && <p style={{ color: "red" }}>{state.errors.password}</p>}
                <p className="text-gray-500 underline text-sm text-center mb-2">
                    forgot password
                </p>
                <br />
                <br />
                <LoginButton />
            </form>
        </div>
    )
}
