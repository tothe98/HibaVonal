"use client"

import { logout } from "../actions";

export default function Home() {
    return (
        <div className={`
                bg-gradient-to-tr from-cyan-600 via-cyan-400 to-white
                flex flex-col justify-center items-center min-h-screen
                sm:p-20 text-black font-[family-name:var(--font-geist-sans)]
            `}
        >
            <main
                className={`
                    bg-white rounded-xl py-10 px-20 shadow-2xl shadow-gray-600
                    flex flex-col gap-2 items-center row-start-2 sm:items-start
                `}
            >
                <button onClick={() => logout()}>Logout for now</button>
            </main>
        </div>
    );
}
