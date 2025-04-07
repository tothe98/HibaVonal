import type { Config } from "tailwindcss"

export default {
    content: [
        "./src/pages/**/*.{js,ts,jsx,tsx,mdx}",
        "./src/components/**/*.{js,ts,jsx,tsx,mdx}",
        "./src/app/**/*.{js,ts,jsx,tsx,mdx}",
    ],
    safelist: [
        "text-err-critical",
        "text-err-high",
        "text-err-medium",
        "text-err-low",
    ],
    theme: {
        extend: {
            colors: {
                background: "var(--background)",
                foreground: "var(--foreground)",

                'err-critical': "#6C0000",
                'err-high': "#DC4465",
                'err-medium': "#FE860E",
                'err-low': "#FECC60",

            },
        },
    },
    plugins: [],
} satisfies Config
