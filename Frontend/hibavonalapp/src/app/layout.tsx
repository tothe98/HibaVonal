import type { Metadata } from "next"
import { Geist, Geist_Mono } from "next/font/google"
import "./globals.css"

const geistSans = Geist({
    variable: "--font-geist-sans",
    subsets: ["latin"],
})

const geistMono = Geist_Mono({
    variable: "--font-geist-mono",
    subsets: ["latin"],
})

export const metadata: Metadata = {
    title: "Dorm Manager",
    description: "Dormitory manager web application",
}

export default function RootLayout({ children }: Readonly<{ children: React.ReactNode }>) {
    return (
        <html lang="en">
            <head>
                <meta name="viewport" content="width=device-width, initial-scale=1.0" />
            </head>
            <body
                className={`
                    bg-gradient-to-tr from-cyan-600 via-cyan-400 to-white
                    flex flex-col justify-center items-center min-h-screen
                    text-black ${geistSans.variable} ${geistMono.variable} antialiased
                `}
            >
                {children}
            </body>
        </html>
    )
}
