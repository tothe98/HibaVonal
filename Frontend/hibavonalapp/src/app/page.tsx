import { Key } from "react";

export default function Home() {
    let status: string[] = ["critical", "high", "medium", "low"];

    return (
        <div className="bg-gradient-to-tr from-[#009BB0] from-10% grid grid-rows-[20px_1fr_20px] items-center justify-items-center min-h-screen p-8 pb-20 gap-16 sm:p-20 font-[family-name:var(--font-geist-sans)]">
            <main className="bg-white rounded-xl p-20 shadow-2xl flex-col gap-8 row-start-2 items-center sm:items-start">
                {status.map((e: string, i: Key) => {
                    return (
                        <h1 key={i} className={`font-bold text-xl text-err-${e}`}>
                            {e}
                        </h1>
                    )
                })}
            </main>
        </div>
    );
}
