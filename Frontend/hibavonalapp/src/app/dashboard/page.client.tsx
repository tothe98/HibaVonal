"use client"

import MenuButton from "@/components/MenuButton"
import { logout } from "../actions"
import { useRouter } from "next/navigation"

interface ButtonConfig {
    id: string
    label: string
    onClick: () => void
    icon: React.ReactNode
}

interface Role {
    roleId: number
    name: string
}

interface User {
    name: string
    email: string
    roles: Role[]
}

interface DashboardClientPageProps {
    user: User
}

export default function DashboardClientPage({ user }: DashboardClientPageProps) {
    const router = useRouter()
    const roles = user.roles || []

    const ROLE_USER = 4
    const ROLE_WORKER = 3
    const ROLE_MANAGER = 2
    const ROLE_ADMIN = 1

    const baseButtons: ButtonConfig[] = [
        {
            id: '1', label: 'Edit Profile', onClick: () => router.push('/dashboard/edit-profile'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M11 15C10.1183 15 9.28093 14.8098 8.52682 14.4682C8.00429 14.2315 7.74302 14.1131 7.59797 14.0722C7.4472 14.0297 7.35983 14.0143 7.20361 14.0026C7.05331 13.9914 6.94079 14 6.71575 14.0172C6.6237 14.0242 6.5425 14.0341 6.46558 14.048C5.23442 14.2709 4.27087 15.2344 4.04798 16.4656C4 16.7306 4 17.0485 4 17.6841V19.4C4 19.9601 4 20.2401 4.10899 20.454C4.20487 20.6422 4.35785 20.7951 4.54601 20.891C4.75992 21 5.03995 21 5.6 21H8.4M15 7C15 9.20914 13.2091 11 11 11C8.79086 11 7 9.20914 7 7C7 4.79086 8.79086 3 11 3C13.2091 3 15 4.79086 15 7ZM12.5898 21L14.6148 20.595C14.7914 20.5597 14.8797 20.542 14.962 20.5097C15.0351 20.4811 15.1045 20.4439 15.1689 20.399C15.2414 20.3484 15.3051 20.2848 15.4324 20.1574L19.5898 16C20.1421 15.4477 20.1421 14.5523 19.5898 14C19.0376 13.4477 18.1421 13.4477 17.5898 14L13.4324 18.1574C13.3051 18.2848 13.2414 18.3484 13.1908 18.421C13.1459 18.4853 13.1088 18.5548 13.0801 18.6279C13.0478 18.7102 13.0302 18.7985 12.9948 18.975L12.5898 21Z" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path></g></svg>
            )
        },
        {
            id: '2', label: 'Logout', onClick: logout, icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" transform="matrix(-1, 0, 0, 1, 0, 0)"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M15 4H18C19.1046 4 20 4.89543 20 6V18C20 19.1046 19.1046 20 18 20H15M8 8L4 12M4 12L8 16M4 12L16 12" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path></g></svg>
            )
        }
    ]

    const userButtons: ButtonConfig[] = [
        {
            id: '3', label: 'My Issue List', onClick: () => router.push('/dashboard/issues'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M9 5H7C5.89543 5 5 5.89543 5 7V19C5 20.1046 5.89543 21 7 21H17C18.1046 21 19 20.1046 19 19V7C19 5.89543 18.1046 5 17 5H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 12H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 16H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M9 5C9 3.89543 9.89543 3 11 3H13C14.1046 3 15 3.89543 15 5V7H9V5Z" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <circle cx="9" cy="12" r="1" fill="#000000"></circle> <circle cx="9" cy="16" r="1" fill="#000000"></circle> </g></svg>
            )
        },
        {
            id: '4', label: 'Report Issue', onClick: () => router.push('/dashboard/report-issue'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeWidth="0"></g><g id="SVGRepo_iconCarrier"><path d="M4 12H20M12 4V20" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path></g></svg>
            )
        }
    ]

    const workerButtons: ButtonConfig[] = [
        {
            id: '5', label: 'Issues List', onClick: logout, icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M9 5H7C5.89543 5 5 5.89543 5 7V19C5 20.1046 5.89543 21 7 21H17C18.1046 21 19 20.1046 19 19V7C19 5.89543 18.1046 5 17 5H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 12H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 16H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M9 5C9 3.89543 9.89543 3 11 3H13C14.1046 3 15 3.89543 15 5V7H9V5Z" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <circle cx="9" cy="12" r="1" fill="#000000"></circle> <circle cx="9" cy="16" r="1" fill="#000000"></circle> </g></svg>
            )
        },
        {
            id: '6', label: 'Order Equipment', onClick: logout, icon: (
                <svg width="64px" height="64px" viewBox="0 0 20.00 20.00" version="1.1" xmlns="http://www.w3.org/2000/svg" fill="#000000"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <g id="layer1"> <path d="M 16.019531 0.38085938 L 15.779297 0.39453125 L 15.541016 0.44726562 L 15.316406 0.53710938 L 15.107422 0.66210938 L 14.921875 0.8203125 L 0.42578125 15.316406 L 0.2734375 15.494141 L 0.15039062 15.695312 L 0.060546875 15.912109 L 0.00390625 16.140625 L -0.013671875 16.376953 L 0.005859375 16.611328 L 0.060546875 16.839844 L 0.15039062 17.058594 L 0.2734375 17.257812 L 0.42578125 17.439453 L 2.4609375 19.474609 L 2.5742188 19.556641 L 2.703125 19.607422 L 2.8417969 19.619141 L 2.9804688 19.591797 L 3.1035156 19.529297 L 3.2050781 19.431641 L 3.2773438 19.3125 L 3.3125 19.179688 L 3.3300781 19.027344 L 3.3613281 18.900391 L 3.4257812 18.787109 L 3.5175781 18.691406 L 3.6308594 18.625 L 3.7539062 18.589844 L 3.8847656 18.587891 L 4.3808594 18.648438 L 4.5117188 18.646484 L 4.6386719 18.609375 L 4.75 18.542969 L 4.8417969 18.449219 L 4.9042969 18.335938 L 4.9355469 18.208984 L 4.9960938 17.712891 L 5.0273438 17.585938 L 5.0898438 17.470703 L 5.1816406 17.376953 L 5.2929688 17.310547 L 5.4179688 17.275391 L 5.5507812 17.275391 L 6.046875 17.332031 L 6.1777344 17.330078 L 6.3027344 17.294922 L 6.4140625 17.228516 L 6.5058594 17.134766 L 6.5683594 17.019531 L 6.6015625 16.894531 L 6.6601562 16.398438 L 6.6914062 16.271484 L 6.7558594 16.158203 L 6.8476562 16.064453 L 6.9570312 15.996094 L 7.0839844 15.962891 L 7.2148438 15.960938 L 7.7109375 16.017578 L 7.8417969 16.015625 L 7.9667969 15.982422 L 8.078125 15.914062 L 8.1699219 15.820312 L 8.234375 15.707031 L 8.265625 15.580078 L 8.3242188 15.083984 L 8.3554688 14.955078 L 8.4199219 14.84375 L 8.5117188 14.75 L 8.6230469 14.683594 L 8.75 14.646484 L 8.8789062 14.644531 L 9.375 14.703125 L 9.5078125 14.703125 L 9.6308594 14.667969 L 9.7441406 14.601562 L 9.8339844 14.507812 L 9.8984375 14.392578 L 9.9316406 14.265625 L 9.9902344 13.769531 L 10.023438 13.642578 L 10.083984 13.529297 L 10.175781 13.435547 L 10.289062 13.369141 L 10.414062 13.332031 L 10.544922 13.332031 L 11.041016 13.388672 L 11.171875 13.388672 L 11.296875 13.351562 L 11.410156 13.285156 L 11.5 13.191406 L 11.5625 13.078125 L 11.597656 12.951172 L 11.654297 12.455078 L 11.685547 12.328125 L 11.75 12.212891 L 11.841797 12.121094 L 11.953125 12.052734 L 12.080078 12.019531 L 12.208984 12.017578 L 12.707031 12.074219 L 12.835938 12.074219 L 12.960938 12.037109 L 13.074219 11.970703 L 13.164062 11.876953 L 13.228516 11.763672 L 13.261719 11.636719 L 13.320312 11.140625 L 13.351562 11.013672 L 13.416016 10.900391 L 13.505859 10.806641 L 13.619141 10.740234 L 13.744141 10.705078 L 13.873047 10.701172 L 14.371094 10.759766 L 14.501953 10.759766 L 14.626953 10.724609 L 14.740234 10.658203 L 14.830078 10.564453 L 14.894531 10.449219 L 14.925781 10.322266 L 15.035156 9.4003906 L 15.296875 9.4863281 L 15.564453 9.5546875 L 15.839844 9.5859375 L 16.117188 9.5820312 L 16.388672 9.5371094 L 16.652344 9.453125 L 16.904297 9.3359375 L 17.134766 9.1855469 L 17.34375 9.0039062 L 19.574219 6.7753906 L 19.730469 6.5917969 L 19.853516 6.3847656 L 19.945312 6.1621094 L 19.998047 5.9277344 L 20.013672 5.6875 L 19.988281 5.4453125 L 19.927734 5.2148438 L 19.828125 4.9941406 L 19.697266 4.7910156 L 19.535156 4.6152344 L 19.345703 4.4667969 L 18.363281 3.8085938 L 18.152344 3.6503906 L 17.966797 3.4609375 L 17.804688 3.2519531 L 17.673828 3.0234375 L 17.576172 2.7773438 L 17.507812 2.5214844 L 17.474609 2.2597656 L 17.478516 1.9941406 L 17.476562 1.7519531 L 17.435547 1.5136719 L 17.357422 1.2832031 L 17.244141 1.0683594 L 17.095703 0.875 L 16.917969 0.70703125 L 16.716797 0.57226562 L 16.496094 0.47070312 L 16.263672 0.40625 L 16.019531 0.38085938 z M 15.994141 1.3808594 L 16.126953 1.4042969 L 16.251953 1.4589844 L 16.353516 1.546875 L 16.429688 1.6582031 L 16.474609 1.7832031 L 16.480469 1.9179688 L 16.476562 2.2695312 L 16.511719 2.6230469 L 16.585938 2.9667969 L 16.703125 3.3007812 L 16.859375 3.6152344 L 17.046875 3.9121094 L 17.273438 4.1855469 L 17.527344 4.4296875 L 17.808594 4.6425781 L 18.792969 5.296875 L 18.886719 5.3808594 L 18.958984 5.4824219 L 19.001953 5.6015625 L 19.013672 5.7285156 L 18.994141 5.8535156 L 18.945312 5.96875 L 18.867188 6.0664062 L 16.638672 8.296875 L 16.496094 8.4140625 L 16.335938 8.5039062 L 16.160156 8.5625 L 15.978516 8.5878906 L 15.794922 8.5800781 L 15.615234 8.5390625 L 11.527344 7.1757812 L 11.382812 7.1152344 L 11.253906 7.0351562 L 11.136719 6.9335938 L 10.679688 6.4785156 L 15.628906 1.5273438 L 15.736328 1.4453125 L 15.861328 1.3964844 L 15.994141 1.3808594 z M 14.886719 4.0976562 L 14.691406 4.1171875 L 14.503906 4.1738281 L 14.330078 4.265625 L 14.179688 4.390625 L 13.361328 5.2070312 L 13.234375 5.3652344 L 13.140625 5.5449219 L 13.085938 5.7421875 L 13.070312 5.9433594 L 13.095703 6.1445312 L 13.162109 6.3359375 L 13.267578 6.5117188 L 13.404297 6.6621094 L 13.568359 6.7792969 L 13.751953 6.8632812 L 15.34375 7.3945312 L 15.523438 7.4375 L 15.707031 7.4453125 L 15.890625 7.4199219 L 16.064453 7.359375 L 16.226562 7.2695312 L 16.367188 7.1542969 L 16.867188 6.6542969 L 16.990234 6.5019531 L 17.083984 6.3300781 L 17.138672 6.140625 L 17.160156 5.9472656 L 17.138672 5.7519531 L 17.083984 5.5625 L 16.990234 5.390625 L 16.865234 5.2382812 L 16.712891 5.1132812 L 15.441406 4.2675781 L 15.269531 4.1738281 L 15.080078 4.1171875 L 14.886719 4.0976562 z M 14.886719 5.0976562 L 16.160156 5.9472656 L 15.660156 6.4453125 L 14.070312 5.9140625 L 14.886719 5.0976562 z M 9.9726562 7.1855469 L 10.429688 7.6425781 L 10.601562 7.7949219 L 10.791016 7.9277344 L 10.994141 8.0390625 L 11.208984 8.125 L 14.064453 9.0761719 L 13.992188 9.7089844 L 13.755859 9.7011719 L 13.523438 9.7285156 L 13.294922 9.7929688 L 13.082031 9.890625 L 12.886719 10.021484 L 12.712891 10.181641 L 12.568359 10.365234 L 12.453125 10.572266 L 12.371094 10.792969 L 12.326172 11.025391 L 12.091797 11.015625 L 11.857422 11.042969 L 11.630859 11.107422 L 11.416016 11.205078 L 11.220703 11.333984 L 11.048828 11.496094 L 10.902344 11.679688 L 10.789062 11.886719 L 10.707031 12.107422 L 10.660156 12.337891 L 10.425781 12.328125 L 10.191406 12.357422 L 9.9648438 12.419922 L 9.7519531 12.519531 L 9.5566406 12.650391 L 9.3847656 12.808594 L 9.2382812 12.996094 L 9.1230469 13.199219 L 9.0429688 13.421875 L 8.9960938 13.652344 L 8.7617188 13.642578 L 8.5273438 13.671875 L 8.3007812 13.734375 L 8.0878906 13.833984 L 7.8925781 13.964844 L 7.71875 14.125 L 7.5722656 14.310547 L 7.4570312 14.515625 L 7.3769531 14.736328 L 7.3320312 14.966797 L 7.0957031 14.958984 L 6.8632812 14.986328 L 6.6367188 15.050781 L 6.421875 15.148438 L 6.2265625 15.279297 L 6.0546875 15.439453 L 5.9082031 15.625 L 5.7929688 15.830078 L 5.7128906 16.050781 L 5.6660156 16.28125 L 5.4296875 16.271484 L 5.1972656 16.298828 L 4.96875 16.365234 L 4.7578125 16.462891 L 4.5625 16.59375 L 4.3886719 16.753906 L 4.2441406 16.939453 L 4.1289062 17.144531 L 4.046875 17.365234 L 4 17.595703 L 3.7734375 17.587891 L 3.5488281 17.613281 L 3.3261719 17.671875 L 3.1191406 17.761719 L 2.9277344 17.884766 L 2.7558594 18.033203 L 2.609375 18.207031 L 1.1328125 16.732422 L 1.0527344 16.626953 L 1.0019531 16.505859 L 0.98632812 16.376953 L 1.0039062 16.248047 L 1.0546875 16.126953 L 1.1328125 16.023438 L 9.9726562 7.1855469 z "></path> </g> </g></svg>
            )
        }
    ]

    const managerButtons: ButtonConfig[] = []

    const adminButtons: ButtonConfig[] = [
        {
            id: '7', label: 'Register User', onClick: () => router.push('/register-user'), icon: (
                < svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" ><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g><g id="SVGRepo_iconCarrier"> <path d="M9 5H7C5.89543 5 5 5.89543 5 7V19C5 20.1046 5.89543 21 7 21H17C18.1046 21 19 20.1046 19 19V7C19 5.89543 18.1046 5 17 5H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 12H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M12 16H15" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <path d="M9 5C9 3.89543 9.89543 3 11 3H13C14.1046 3 15 3.89543 15 5V7H9V5Z" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path> <circle cx="9" cy="12" r="1" fill="#000000"></circle> <circle cx="9" cy="16" r="1" fill="#000000"></circle> </g></svg>
            )
        },
        {
            id: '8', label: 'Manage Issues', onClick: () => router.push('/manage-issues'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" strokeWidth="0"></g><g id="SVGRepo_tracerCarrier" strokeWidth="0"></g><g id="SVGRepo_iconCarrier"><path d="M4 12H20M12 4V20" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path></g></svg>
            )
        },
        {
            id: '9', label: 'Manage Users', onClick: () => router.push('/manage-users'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"><path d="M15.8 21C15.8 21.3866 16.1134 21.7 16.5 21.7C16.8866 21.7 17.2 21.3866 17.2 21H15.8ZM4.8 21C4.8 21.3866 5.1134 21.7 5.5 21.7C5.8866 21.7 6.2 21.3866 6.2 21H4.8ZM6.2 18C6.2 17.6134 5.8866 17.3 5.5 17.3C5.1134 17.3 4.8 17.6134 4.8 18H6.2ZM12.3 21C12.3 21.3866 12.6134 21.7 13 21.7C13.3866 21.7 13.7 21.3866 13.7 21H12.3ZM13.7 18C13.7 17.6134 13.3866 17.3 13 17.3C12.6134 17.3 12.3 17.6134 12.3 18H13.7ZM11.7429 11.3125L11.3499 10.7333L11.3499 10.7333L11.7429 11.3125ZM16.2429 11.3125L15.8499 10.7333L15.8499 10.7333L16.2429 11.3125ZM3.2 21V19.5H1.8V21H3.2ZM8 14.7H11V13.3H8V14.7ZM15.8 19.5V21H17.2V19.5H15.8ZM11 14.7C13.651 14.7 15.8 16.849 15.8 19.5H17.2C17.2 16.0758 14.4242 13.3 11 13.3V14.7ZM3.2 19.5C3.2 16.849 5.34903 14.7 8 14.7V13.3C4.57583 13.3 1.8 16.0758 1.8 19.5H3.2ZM11 14.7H15.5V13.3H11V14.7ZM20.3 19.5V21H21.7V19.5H20.3ZM15.5 14.7C18.151 14.7 20.3 16.849 20.3 19.5H21.7C21.7 16.0758 18.9242 13.3 15.5 13.3V14.7ZM6.2 21V18H4.8V21H6.2ZM13.7 21V18H12.3V21H13.7ZM9.5 11.3C7.67746 11.3 6.2 9.82255 6.2 8.00001H4.8C4.8 10.5958 6.90426 12.7 9.5 12.7V11.3ZM6.2 8.00001C6.2 6.17746 7.67746 4.7 9.5 4.7V3.3C6.90426 3.3 4.8 5.40427 4.8 8.00001H6.2ZM9.5 4.7C11.3225 4.7 12.8 6.17746 12.8 8.00001H14.2C14.2 5.40427 12.0957 3.3 9.5 3.3V4.7ZM12.8 8.00001C12.8 9.13616 12.2264 10.1386 11.3499 10.7333L12.1358 11.8918C13.3801 11.0477 14.2 9.61973 14.2 8.00001H12.8ZM11.3499 10.7333C10.8225 11.091 10.1867 11.3 9.5 11.3V12.7C10.4757 12.7 11.3839 12.4019 12.1358 11.8918L11.3499 10.7333ZM14 4.7C15.8225 4.7 17.3 6.17746 17.3 8.00001H18.7C18.7 5.40427 16.5957 3.3 14 3.3V4.7ZM17.3 8.00001C17.3 9.13616 16.7264 10.1386 15.8499 10.7333L16.6358 11.8918C17.8801 11.0477 18.7 9.61973 18.7 8.00001H17.3ZM15.8499 10.7333C15.3225 11.091 14.6867 11.3 14 11.3V12.7C14.9757 12.7 15.8839 12.4019 16.6358 11.8918L15.8499 10.7333ZM11.9378 5.42349C12.5029 4.97049 13.2189 4.7 14 4.7V3.3C12.8892 3.3 11.8667 3.68622 11.0622 4.33114L11.9378 5.42349ZM14 11.3C13.3133 11.3 12.6775 11.091 12.1501 10.7333L11.3642 11.8918C12.1161 12.4019 13.0243 12.7 14 12.7V11.3Z" fill="#000000"></path></g></svg>            )
        },
        {
            id: '10', label: 'Manage Rooms', onClick: () => router.push('/manage-rooms'), icon: (
                <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" stroke="#000000"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier">{" "}<path d="M16.5 22.5H18.75C19.1478 22.5 19.5294 22.342 19.8107 22.0607C20.092 21.7794 20.25 21.3978 20.25 21V13.5" stroke="#000000" stroke-width="1.44" stroke-linecap="round" stroke-linejoin="round"></path>{" "}<path d="M7.5 22.5H5.25C4.85218 22.5 4.47064 22.342 4.18934 22.0607C3.90804 21.7794 3.75 21.3978 3.75 21V13.5" stroke="#000000" stroke-width="1.44" stroke-linecap="round" stroke-linejoin="round"></path>{" "}<path d="M0.75 12.129L10.939 1.939C11.0783 1.79961 11.2437 1.68904 11.4258 1.61359C11.6078 1.53815 11.8029 1.49932 12 1.49932C12.1971 1.49932 12.3922 1.53815 12.5742 1.61359C12.7563 1.68904 12.9217 1.79961 13.061 1.939L23.122 12"stroke="#000000"stroke-width="1.44"stroke-linecap="round"stroke-linejoin="round"></path>{" "}<path d="M12 12C13.2426 12 14.25 10.9926 14.25 9.75C14.25 8.50736 13.2426 7.5 12 7.5C10.7574 7.5 9.75 8.50736 9.75 9.75C9.75 10.9926 10.7574 12 12 12Z" stroke="#000000" stroke-width="1.44" stroke-linecap="round" stroke-linejoin="round"></path>{" "}<path d="M12 13.5C11.0054 13.5 10.0516 13.8951 9.34835 14.5983C8.64509 15.3016 8.25 16.2554 8.25 17.25V18H9.75L10.5 22.5H13.5L14.25 18H15.75V17.25C15.75 16.2554 15.3549 15.3016 14.6517 14.5983C13.9484 13.8951 12.9946 13.5 12 13.5Z"stroke="#000000"stroke-width="1.44"stroke-linecap="round"stroke-linejoin="round"></path>{" "}</g></svg>            )
        }
    ]

    let buttons: ButtonConfig[] = [
    ]
    if (roles.some(role => role.roleId === ROLE_ADMIN)) {
        buttons = [...adminButtons]
    }
    if (roles.some(role => role.roleId === ROLE_MANAGER)) {
        buttons = [...managerButtons]
    }
    if (roles.some(role => role.roleId === ROLE_WORKER)) {
        buttons = [...workerButtons]
    }
    if (roles.some(role => role.roleId === ROLE_USER || role.roleId === ROLE_ADMIN)) {
        buttons = [...buttons, ...userButtons]
    }
    buttons = [...buttons, ...baseButtons]

    return (
        <>
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
        </>
    )
}
