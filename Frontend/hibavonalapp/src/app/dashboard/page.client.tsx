"use client";

import MenuButton from "@/components/MenuButton";
import { logout } from "../actions";
import { useRouter } from "next/navigation";

interface ButtonConfig {
    id: string;
    label: string;
    onClick: () => void;
    icon: React.ReactNode;
}

interface Role {
    roleId: number;
    name: string;
}

interface User {
    name: string;
    email: string;
    roles: Role[];
}

interface DashboardClientPageProps {
    user: User;
}

export default function DashboardClientPage({ user }: DashboardClientPageProps) {
    const router = useRouter();
    const roles = user.roles || [];

    const ROLE_USER = 4;
    const ROLE_WORKER = 3;
    const ROLE_MANAGER = 2;
    const ROLE_ADMIN = 1;

    const baseButtons: ButtonConfig[] = [
        {
            id: "1",
            label: "Edit Profile",
            onClick: () => router.push("/dashboard/edit-profile"),
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                    <g id="SVGRepo_iconCarrier">
                        <path
                            d="M11 15C10.1183 15 9.28093 14.8098 8.52682 14.4682C8.00429 14.2315 7.74302 14.1131 7.59797 14.0722C7.4472 14.0297 7.35983 14.0143 7.20361 14.0026C7.05331 13.9914 6.94079 14 6.71575 14.0172C6.6237 14.0242 6.5425 14.0341 6.46558 14.048C5.23442 14.2709 4.27087 15.2344 4.04798 16.4656C4 16.7306 4 17.0485 4 17.6841V19.4C4 19.9601 4 20.2401 4.10899 20.454C4.20487 20.6422 4.35785 20.7951 4.54601 20.891C4.75992 21 5.03995 21 5.6 21H8.4M15 7C15 9.20914 13.2091 11 11 11C8.79086 11 7 9.20914 7 7C7 4.79086 8.79086 3 11 3C13.2091 3 15 4.79086 15 7ZM12.5898 21L14.6148 20.595C14.7914 20.5597 14.8797 20.542 14.962 20.5097C15.0351 20.4811 15.1045 20.4439 15.1689 20.399C15.2414 20.3484 15.3051 20.2848 15.4324 20.1574L19.5898 16C20.1421 15.4477 20.1421 14.5523 19.5898 14C19.0376 13.4477 18.1421 13.4477 17.5898 14L13.4324 18.1574C13.3051 18.2848 13.2414 18.3484 13.1908 18.421C13.1459 18.4853 13.1088 18.5548 13.0801 18.6279C13.0478 18.7102 13.0302 18.7985 12.9948 18.975L12.5898 21Z"
                            stroke="#000000"
                            strokeWidth="1.56"
                            strokeLinecap="round"
                            strokeLinejoin="round"
                        ></path>
                    </g>
                </svg>
            ),
        },
        {
            id: "2",
            label: "Logout",
            onClick: logout,
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" transform="matrix(-1, 0, 0, 1, 0, 0)">
                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                    <g id="SVGRepo_iconCarrier">
                        <path d="M15 4H18C19.1046 4 20 4.89543 20 6V18C20 19.1046 19.1046 20 18 20H15M8 8L4 12M4 12L8 16M4 12L16 12" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path>
                    </g>
                </svg>
            ),
        },
    ];

    const userButtons: ButtonConfig[] = [
        {
            id: "3",
            label: "My Issue List",
            onClick: () => router.push("/dashboard/issues"),
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                    <g id="SVGRepo_iconCarrier">
                        {" "}
                        <path d="M9 5H7C5.89543 5 5 5.89543 5 7V19C5 20.1046 5.89543 21 7 21H17C18.1046 21 19 20.1046 19 19V7C19 5.89543 18.1046 5 17 5H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path>{" "}
                        <path d="M12 12H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 16H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path>{" "}
                        <path d="M9 5C9 3.89543 9.89543 3 11 3H13C14.1046 3 15 3.89543 15 5V7H9V5Z" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <circle cx="9" cy="12" r="1" fill="#000000"></circle> <circle cx="9" cy="16" r="1" fill="#000000"></circle>{" "}
                    </g>
                </svg>
            ),
        },
        {
            id: "4",
            label: "Report Issue",
            onClick: () => router.push("/dashboard/report-issue"),
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_tracerCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_iconCarrier">
                        <path d="M4 12H20M12 4V20" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path>
                    </g>
                </svg>
            ),
        },
    ];

    const workerButtons: ButtonConfig[] = [
        {
            id: "5",
            label: "Assigned Issues",
            onClick: () => router.push("/dashboard/assigned-issues"),
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                    <g id="SVGRepo_iconCarrier">
                        {" "}
                        <path d="M9 5H7C5.89543 5 5 5.89543 5 7V19C5 20.1046 5.89543 21 7 21H17C18.1046 21 19 20.1046 19 19V7C19 5.89543 18.1046 5 17 5H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path>{" "}
                        <path d="M12 12H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 16H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path>{" "}
                        <path d="M9 5C9 3.89543 9.89543 3 11 3H13C14.1046 3 15 3.89543 15 5V7H9V5Z" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <circle cx="9" cy="12" r="1" fill="#000000"></circle> <circle cx="9" cy="16" r="1" fill="#000000"></circle>{" "}
                    </g>
                </svg>
            ),
        },
        {
            id: "6",
            label: "Make Order",
            onClick: () => router.push("/dashboard/make-order"),
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M21 5L19 12H7.37671M20 16H8L6 3H3M16 5.5H13.5M13.5 5.5H11M13.5 5.5V8M13.5 5.5V3M9 20C9 20.5523 8.55228 21 8 21C7.44772 21 7 20.5523 7 20C7 19.4477 7.44772 19 8 19C8.55228 19 9 19.4477 9 20ZM20 20C20 20.5523 19.5523 21 19 21C18.4477 21 18 20.5523 18 20C18 19.4477 18.4477 19 19 19C19.5523 19 20 19.4477 20 20Z" stroke="#000000" strokeWidth="1.44" strokeLinecap="round" strokeLinejoin="round"></path> </g></svg>
            ),
        },
    ];

    const managerButtons: ButtonConfig[] = [
        {
            id: "7",
            label: "Orders",
            onClick: () => router.push("/dashboard/orders"),
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M7.2998 5H22L20 12H8.37675M21 16H9L7 3H4M4 8H2M5 11H2M6 14H2M10 20C10 20.5523 9.55228 21 9 21C8.44772 21 8 20.5523 8 20C8 19.4477 8.44772 19 9 19C9.55228 19 10 19.4477 10 20ZM21 20C21 20.5523 20.5523 21 20 21C19.4477 21 19 20.5523 19 20C19 19.4477 19.4477 19 20 19C20.5523 19 21 19.4477 21 20Z" stroke="#000000" strokeWidth="1.44" strokeLinecap="round" strokeLinejoin="round"></path> </g></svg>
            ),
        },
        {
            id: "8",
            label: "Make Order",
            onClick: () => router.push("/dashboard/make-order"),
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M21 5L19 12H7.37671M20 16H8L6 3H3M16 5.5H13.5M13.5 5.5H11M13.5 5.5V8M13.5 5.5V3M9 20C9 20.5523 8.55228 21 8 21C7.44772 21 7 20.5523 7 20C7 19.4477 7.44772 19 8 19C8.55228 19 9 19.4477 9 20ZM20 20C20 20.5523 19.5523 21 19 21C18.4477 21 18 20.5523 18 20C18 19.4477 18.4477 19 19 19C19.5523 19 20 19.4477 20 20Z" stroke="#000000" strokeWidth="1.44" strokeLinecap="round" strokeLinejoin="round"></path> </g></svg>
            ),
        },
        {
            id: "9",
            label: "Reports",
            onClick: () => router.push("/dashboard/reports"),
            icon: (
                <svg fill="#000000" width="64px" height="64px" viewBox="0 0 56 56" xmlns="http://www.w3.org/2000/svg" stroke="#000000" strokeWidth="0.00056"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M 20.8867 50.7109 L 35.1132 50.7109 C 38.0898 50.7109 39.5195 49.7500 41.0429 48.0859 L 49.6679 38.5234 C 51.2149 36.8125 51.6133 35.6875 51.6133 33.3671 L 51.6133 22.6562 C 51.6133 20.3125 51.2149 19.2109 49.6679 17.5000 L 41.0429 7.9375 C 39.5195 6.2734 38.0898 5.2891 35.1132 5.2891 L 20.8867 5.2891 C 17.9101 5.2891 16.5039 6.2734 14.9570 7.9375 L 6.3320 17.5000 C 4.7851 19.2109 4.3867 20.3125 4.3867 22.6562 L 4.3867 33.3671 C 4.3867 35.6875 4.7851 36.8125 6.3320 38.5234 L 14.9570 48.0859 C 16.5039 49.7500 17.9101 50.7109 20.8867 50.7109 Z M 22.1523 46.9609 C 19.8086 46.9609 19.1523 46.4453 17.8398 45.0390 L 9.8711 36.2500 C 8.9335 35.2187 8.7226 34.6328 8.7226 32.9218 L 8.7226 23.1015 C 8.7226 21.3906 8.9335 20.8047 9.8711 19.7734 L 17.8398 10.9844 C 19.1523 9.5547 19.8086 9.0625 22.1523 9.0625 L 33.8476 9.0625 C 36.1913 9.0625 36.8476 9.5547 38.1601 10.9844 L 46.1523 19.7734 C 47.0663 20.8047 47.2775 21.3906 47.2775 23.1015 L 47.2775 32.9218 C 47.2775 34.6328 47.0663 35.2187 46.1523 36.2500 L 38.1601 45.0390 C 36.8476 46.4453 36.1913 46.9609 33.8476 46.9609 Z M 28.0117 32.125 C 29.1132 32.125 29.7695 31.4922 29.7929 30.2734 L 30.1445 17.8984 C 30.1679 16.7031 29.2304 15.8125 27.9882 15.8125 C 26.7226 15.8125 25.8320 16.6797 25.8554 17.8750 L 26.1601 30.2734 C 26.1835 31.4687 26.8398 32.125 28.0117 32.125 Z M 28.0117 39.7422 C 29.3476 39.7422 30.5195 38.6640 30.5195 37.3047 C 30.5195 35.9453 29.3711 34.8671 28.0117 34.8671 C 26.6289 34.8671 25.4804 35.9687 25.4804 37.3047 C 25.4804 38.6406 26.6523 39.7422 28.0117 39.7422 Z"></path></g></svg>
            ),
        },
    ];

    const adminButtons: ButtonConfig[] = [
        {
            id: "10",
            label: "Error Types",
            onClick: () => router.push("/dashboard/errortypes"),
            icon: (
                <svg fill="#000000" width="64px" height="64px" viewBox="0 0 1024 1024" xmlns="http://www.w3.org/2000/svg" stroke="#000000" strokeWidth="10.24"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M520.741 163.801a10.234 10.234 0 00-3.406-3.406c-4.827-2.946-11.129-1.421-14.075 3.406L80.258 856.874a10.236 10.236 0 00-1.499 5.335c0 5.655 4.585 10.24 10.24 10.24h846.004c1.882 0 3.728-.519 5.335-1.499 4.827-2.946 6.352-9.248 3.406-14.075L520.742 163.802zm43.703-26.674L987.446 830.2c17.678 28.964 8.528 66.774-20.436 84.452a61.445 61.445 0 01-32.008 8.996H88.998c-33.932 0-61.44-27.508-61.44-61.44a61.445 61.445 0 018.996-32.008l423.002-693.073c17.678-28.964 55.488-38.113 84.452-20.436a61.438 61.438 0 0120.436 20.436zM512 778.24c22.622 0 40.96-18.338 40.96-40.96s-18.338-40.96-40.96-40.96-40.96 18.338-40.96 40.96 18.338 40.96 40.96 40.96zm0-440.32c-22.622 0-40.96 18.338-40.96 40.96v225.28c0 22.622 18.338 40.96 40.96 40.96s40.96-18.338 40.96-40.96V378.88c0-22.622-18.338-40.96-40.96-40.96z"></path></g></svg>
            ),
        },
        {
            id: "11",
            label: "Add Error Type",
            onClick: () => router.push("/dashboard/create-errortype"),
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M20 14V7C20 5.34315 18.6569 4 17 4H12M20 14L13.5 20M20 14H15.5C14.3954 14 13.5 14.8954 13.5 16V20M13.5 20H7C5.34315 20 4 18.6569 4 17V12" stroke="#000000" strokeWidth="1.44" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M7 4V7M7 10V7M7 7H4M7 7H10" stroke="#000000" strokeWidth="1.44" strokeLinecap="round" strokeLinejoin="round"></path> </g></svg>
            ),
        },
        {
            id: "12",
            label: "Equipments",
            onClick: () => router.push("/dashboard/equipments"),
            icon: (
                <svg fill="#000000" height="64px" width="64px" version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink" viewBox="0 0 556.705 556.705" xmlSpace="preserve" stroke="#000000" strokeWidth="2.783525">
                    <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
                    <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
                    <g id="SVGRepo_iconCarrier">
                        {" "}
                        <path d="M549.205,86.508H7.5c-4.143,0-7.5,3.358-7.5,7.5v303.996c0,4.142,3.357,7.5,7.5,7.5h25.266 c-14.057,3.741-24.447,16.574-24.447,31.793c0,18.141,14.759,32.899,32.899,32.899s32.899-14.759,32.899-32.899 c0-15.219-10.389-28.053-24.447-31.793H248.66c-14.057,3.741-24.446,16.574-24.446,31.793c0,18.141,14.758,32.899,32.898,32.899 s32.899-14.759,32.899-32.899c0-15.219-10.389-28.053-24.447-31.793h243.322c-12.74,4.54-21.886,16.718-21.886,30.997 c0,18.141,14.759,32.899,32.899,32.899s32.899-14.759,32.899-32.899c0-14.279-9.146-26.457-21.886-30.997h18.291 c4.143,0,7.5-3.358,7.5-7.5V94.008C556.705,89.867,553.348,86.508,549.205,86.508z M519.9,454.4c-9.87,0-17.899-8.03-17.899-17.899 c0-7.19,4.27-13.386,10.399-16.23v16.23c0,4.142,3.357,7.5,7.5,7.5s7.5-3.358,7.5-7.5v-16.23c6.129,2.844,10.399,9.041,10.399,16.23 C537.8,446.371,529.771,454.4,519.9,454.4z M257.112,455.197c-9.869,0-17.898-8.03-17.898-17.899c0-7.189,4.27-13.386,10.398-16.23 v16.23c0,4.142,3.357,7.5,7.5,7.5s7.5-3.358,7.5-7.5v-16.23c6.129,2.844,10.399,9.041,10.399,16.23 C275.012,447.167,266.982,455.197,257.112,455.197z M41.219,455.197c-9.87,0-17.899-8.03-17.899-17.899 c0-7.19,4.27-13.386,10.399-16.23v16.23c0,4.142,3.357,7.5,7.5,7.5s7.5-3.358,7.5-7.5v-16.23c6.129,2.844,10.399,9.041,10.399,16.23 C59.118,447.167,51.089,455.197,41.219,455.197z M239.214,390.504v-27.935c0-4.142-3.357-7.5-7.5-7.5s-7.5,3.358-7.5,7.5v27.935H15 V101.508h209.214v221.094c0,4.142,3.357,7.5,7.5,7.5s7.5-3.358,7.5-7.5V101.508h302.491v288.996H239.214z M164.649,143.246h-91.67 c-4.143,0-7.5,3.357-7.5,7.5v65.479c0,4.142,3.357,7.5,7.5,7.5h91.67c4.143,0,7.5-3.358,7.5-7.5v-65.479 C172.149,146.603,168.792,143.246,164.649,143.246z M80.479,208.724v-50.479h76.67v50.479H80.479z M200.529,109.106H33.691 c-4.143,0-7.5,3.357-7.5,7.5v192.243c0,4.142,3.357,7.5,7.5,7.5h166.838c4.143,0,7.5-3.358,7.5-7.5V116.606 C208.029,112.464,204.672,109.106,200.529,109.106z M41.191,301.349V124.106h151.838v177.243H41.191z M200.529,327.802H33.691 c-4.143,0-7.5,3.358-7.5,7.5v38.502c0,4.142,3.357,7.5,7.5,7.5h166.838c4.143,0,7.5-3.358,7.5-7.5v-38.502 C208.029,331.16,204.672,327.802,200.529,327.802z M41.191,366.304v-23.502h151.838v23.502H41.191z M523.806,327.802H263.892 c-4.143,0-7.5,3.358-7.5,7.5v38.502c0,4.142,3.357,7.5,7.5,7.5h259.914c4.143,0,7.5-3.358,7.5-7.5v-38.502 C531.306,331.16,527.948,327.802,523.806,327.802z M271.392,366.304v-23.502h244.914v23.502H271.392z M523.806,109.106H263.892 c-4.143,0-7.5,3.357-7.5,7.5v193.297c0,4.142,3.357,7.5,7.5,7.5h259.914c4.143,0,7.5-3.358,7.5-7.5V116.606 C531.306,112.464,527.948,109.106,523.806,109.106z M271.392,302.403V124.106h244.914v178.297H271.392z"></path>{" "}
                    </g>
                </svg>
            ),
        },
        {
            id: "13",
            label: "Add Equipment",
            onClick: () => router.push("/dashboard/create-equipment"),
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M14 21H10L9.44904 18.5206C8.7879 18.2618 8.17573 17.9053 7.63028 17.4689L5.20573 18.232L3.20573 14.7679L5.07828 13.0503C5.02673 12.7077 5 12.357 5 12C5 11.643 5.02673 11.2923 5.07828 10.9496L3.20573 9.23204L5.20574 5.76794L7.6303 6.53106C8.17575 6.09467 8.78791 5.73819 9.44904 5.47935L10 3H14L14.551 5.47935C15.2121 5.73819 15.8242 6.09466 16.3697 6.53104L18.7942 5.76794L20.7942 9.23204L18.9217 10.9496C18.9733 11.2922 19 11.643 19 12C19 12.357 18.9733 12.7078 18.9217 13.0504L20.7942 14.7679L18.7942 18.232L16.3697 17.4689C15.8243 17.9053 15.2121 18.2618 14.551 18.5206L14 21Z" stroke="#000000" strokeLinecap="round" strokeLinejoin="round" strokeWidth="1.44"></path><path d="M12 9V12M12 15V12M12 12H9M12 12H15" stroke="#000000" strokeLinecap="round" strokeLinejoin="round" strokeWidth="1.44"></path></g></svg>
            ),
        },
        {
            id: "14",
            label: "Register User",
            onClick: () => router.push("/dashboard/register-user"),
            icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M8 5.00005C7.01165 5.00082 6.49359 5.01338 6.09202 5.21799C5.71569 5.40973 5.40973 5.71569 5.21799 6.09202C5 6.51984 5 7.07989 5 8.2V17.8C5 18.9201 5 19.4802 5.21799 19.908C5.40973 20.2843 5.71569 20.5903 6.09202 20.782C6.51984 21 7.07989 21 8.2 21H15.8C16.9201 21 17.4802 21 17.908 20.782C18.2843 20.5903 18.5903 20.2843 18.782 19.908C19 19.4802 19 18.9201 19 17.8V8.2C19 7.07989 19 6.51984 18.782 6.09202C18.5903 5.71569 18.2843 5.40973 17.908 5.21799C17.5064 5.01338 16.9884 5.00082 16 5.00005M8 5.00005V7H16V5.00005M8 5.00005V4.70711C8 4.25435 8.17986 3.82014 8.5 3.5C8.82014 3.17986 9.25435 3 9.70711 3H14.2929C14.7456 3 15.1799 3.17986 15.5 3.5C15.8201 3.82014 16 4.25435 16 4.70711V5.00005M15 18C14.7164 16.8589 13.481 16 12 16C10.519 16 9.28364 16.8589 9 18M12 12H12.01M13 12C13 12.5523 12.5523 13 12 13C11.4477 13 11 12.5523 11 12C11 11.4477 11.4477 11 12 11C12.5523 11 13 11.4477 13 12Z" stroke="#000000" strokeWidth="1.44" strokeLinecap="round" strokeLinejoin="round"></path> </g></svg>
            ),
        }
    ];

    let buttons: ButtonConfig[] = [];
    if (roles.some((role) => role.roleId === ROLE_ADMIN)) {
        buttons = [...adminButtons];
    }
    if (roles.some((role) => role.roleId === ROLE_MANAGER)) {
        buttons = [...managerButtons];
    }
    if (roles.some((role) => role.roleId === ROLE_WORKER)) {
        buttons = [...workerButtons];
    }
    if (roles.some((role) => role.roleId === ROLE_USER)) {
        buttons = [...buttons, ...userButtons];
    }
    buttons = [...buttons, ...baseButtons];

    return (
        <main
            className={`
                    w-full my-4 sm:max-w-md max-w-xs p-4 sm:p-6
                    bg-white rounded-xl shadow-2xl shadow-gray-600
                    grid grid-cols-1 gap-2 items-center
                    sm:gap-4 sm:grid-cols-2
                `}
        >
            {buttons.length < 1 ? (
                <p className="col-span-1 sm:col-span-2 text-center py-4 text-gray-500">Your role is kind of ass :(</p>
            ) : (
                buttons.map((btn) => (
                    <MenuButton key={btn.id} id={btn.id} label={btn.label} onClick={btn.onClick}>
                        {btn.icon}
                    </MenuButton>
                ))
            )}
        </main>
    );
}
