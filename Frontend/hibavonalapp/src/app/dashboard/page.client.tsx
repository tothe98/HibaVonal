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
      label: "Order Equipment",
      onClick: logout,
      icon: (
        <svg width="64px" height="64px" viewBox="0 0 20.00 20.00" version="1.1" xmlns="http://www.w3.org/2000/svg" fill="#000000">
          <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
          <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
          <g id="SVGRepo_iconCarrier">
            {" "}
            <g id="layer1">
              {" "}
              <path d="M 16.019531 0.38085938 L 15.779297 0.39453125 L 15.541016 0.44726562 L 15.316406 0.53710938 L 15.107422 0.66210938 L 14.921875 0.8203125 L 0.42578125 15.316406 L 0.2734375 15.494141 L 0.15039062 15.695312 L 0.060546875 15.912109 L 0.00390625 16.140625 L -0.013671875 16.376953 L 0.005859375 16.611328 L 0.060546875 16.839844 L 0.15039062 17.058594 L 0.2734375 17.257812 L 0.42578125 17.439453 L 2.4609375 19.474609 L 2.5742188 19.556641 L 2.703125 19.607422 L 2.8417969 19.619141 L 2.9804688 19.591797 L 3.1035156 19.529297 L 3.2050781 19.431641 L 3.2773438 19.3125 L 3.3125 19.179688 L 3.3300781 19.027344 L 3.3613281 18.900391 L 3.4257812 18.787109 L 3.5175781 18.691406 L 3.6308594 18.625 L 3.7539062 18.589844 L 3.8847656 18.587891 L 4.3808594 18.648438 L 4.5117188 18.646484 L 4.6386719 18.609375 L 4.75 18.542969 L 4.8417969 18.449219 L 4.9042969 18.335938 L 4.9355469 18.208984 L 4.9960938 17.712891 L 5.0273438 17.585938 L 5.0898438 17.470703 L 5.1816406 17.376953 L 5.2929688 17.310547 L 5.4179688 17.275391 L 5.5507812 17.275391 L 6.046875 17.332031 L 6.1777344 17.330078 L 6.3027344 17.294922 L 6.4140625 17.228516 L 6.5058594 17.134766 L 6.5683594 17.019531 L 6.6015625 16.894531 L 6.6601562 16.398438 L 6.6914062 16.271484 L 6.7558594 16.158203 L 6.8476562 16.064453 L 6.9570312 15.996094 L 7.0839844 15.962891 L 7.2148438 15.960938 L 7.7109375 16.017578 L 7.8417969 16.015625 L 7.9667969 15.982422 L 8.078125 15.914062 L 8.1699219 15.820312 L 8.234375 15.707031 L 8.265625 15.580078 L 8.3242188 15.083984 L 8.3554688 14.955078 L 8.4199219 14.84375 L 8.5117188 14.75 L 8.6230469 14.683594 L 8.75 14.646484 L 8.8789062 14.644531 L 9.375 14.703125 L 9.5078125 14.703125 L 9.6308594 14.667969 L 9.7441406 14.601562 L 9.8339844 14.507812 L 9.8984375 14.392578 L 9.9316406 14.265625 L 9.9902344 13.769531 L 10.023438 13.642578 L 10.083984 13.529297 L 10.175781 13.435547 L 10.289062 13.369141 L 10.414062 13.332031 L 10.544922 13.332031 L 11.041016 13.388672 L 11.171875 13.388672 L 11.296875 13.351562 L 11.410156 13.285156 L 11.5 13.191406 L 11.5625 13.078125 L 11.597656 12.951172 L 11.654297 12.455078 L 11.685547 12.328125 L 11.75 12.212891 L 11.841797 12.121094 L 11.953125 12.052734 L 12.080078 12.019531 L 12.208984 12.017578 L 12.707031 12.074219 L 12.835938 12.074219 L 12.960938 12.037109 L 13.074219 11.970703 L 13.164062 11.876953 L 13.228516 11.763672 L 13.261719 11.636719 L 13.320312 11.140625 L 13.351562 11.013672 L 13.416016 10.900391 L 13.505859 10.806641 L 13.619141 10.740234 L 13.744141 10.705078 L 13.873047 10.701172 L 14.371094 10.759766 L 14.501953 10.759766 L 14.626953 10.724609 L 14.740234 10.658203 L 14.830078 10.564453 L 14.894531 10.449219 L 14.925781 10.322266 L 15.035156 9.4003906 L 15.296875 9.4863281 L 15.564453 9.5546875 L 15.839844 9.5859375 L 16.117188 9.5820312 L 16.388672 9.5371094 L 16.652344 9.453125 L 16.904297 9.3359375 L 17.134766 9.1855469 L 17.34375 9.0039062 L 19.574219 6.7753906 L 19.730469 6.5917969 L 19.853516 6.3847656 L 19.945312 6.1621094 L 19.998047 5.9277344 L 20.013672 5.6875 L 19.988281 5.4453125 L 19.927734 5.2148438 L 19.828125 4.9941406 L 19.697266 4.7910156 L 19.535156 4.6152344 L 19.345703 4.4667969 L 18.363281 3.8085938 L 18.152344 3.6503906 L 17.966797 3.4609375 L 17.804688 3.2519531 L 17.673828 3.0234375 L 17.576172 2.7773438 L 17.507812 2.5214844 L 17.474609 2.2597656 L 17.478516 1.9941406 L 17.476562 1.7519531 L 17.435547 1.5136719 L 17.357422 1.2832031 L 17.244141 1.0683594 L 17.095703 0.875 L 16.917969 0.70703125 L 16.716797 0.57226562 L 16.496094 0.47070312 L 16.263672 0.40625 L 16.019531 0.38085938 z M 15.994141 1.3808594 L 16.126953 1.4042969 L 16.251953 1.4589844 L 16.353516 1.546875 L 16.429688 1.6582031 L 16.474609 1.7832031 L 16.480469 1.9179688 L 16.476562 2.2695312 L 16.511719 2.6230469 L 16.585938 2.9667969 L 16.703125 3.3007812 L 16.859375 3.6152344 L 17.046875 3.9121094 L 17.273438 4.1855469 L 17.527344 4.4296875 L 17.808594 4.6425781 L 18.792969 5.296875 L 18.886719 5.3808594 L 18.958984 5.4824219 L 19.001953 5.6015625 L 19.013672 5.7285156 L 18.994141 5.8535156 L 18.945312 5.96875 L 18.867188 6.0664062 L 16.638672 8.296875 L 16.496094 8.4140625 L 16.335938 8.5039062 L 16.160156 8.5625 L 15.978516 8.5878906 L 15.794922 8.5800781 L 15.615234 8.5390625 L 11.527344 7.1757812 L 11.382812 7.1152344 L 11.253906 7.0351562 L 11.136719 6.9335938 L 10.679688 6.4785156 L 15.628906 1.5273438 L 15.736328 1.4453125 L 15.861328 1.3964844 L 15.994141 1.3808594 z M 14.886719 4.0976562 L 14.691406 4.1171875 L 14.503906 4.1738281 L 14.330078 4.265625 L 14.179688 4.390625 L 13.361328 5.2070312 L 13.234375 5.3652344 L 13.140625 5.5449219 L 13.085938 5.7421875 L 13.070312 5.9433594 L 13.095703 6.1445312 L 13.162109 6.3359375 L 13.267578 6.5117188 L 13.404297 6.6621094 L 13.568359 6.7792969 L 13.751953 6.8632812 L 15.34375 7.3945312 L 15.523438 7.4375 L 15.707031 7.4453125 L 15.890625 7.4199219 L 16.064453 7.359375 L 16.226562 7.2695312 L 16.367188 7.1542969 L 16.867188 6.6542969 L 16.990234 6.5019531 L 17.083984 6.3300781 L 17.138672 6.140625 L 17.160156 5.9472656 L 17.138672 5.7519531 L 17.083984 5.5625 L 16.990234 5.390625 L 16.865234 5.2382812 L 16.712891 5.1132812 L 15.441406 4.2675781 L 15.269531 4.1738281 L 15.080078 4.1171875 L 14.886719 4.0976562 z M 14.886719 5.0976562 L 16.160156 5.9472656 L 15.660156 6.4453125 L 14.070312 5.9140625 L 14.886719 5.0976562 z M 9.9726562 7.1855469 L 10.429688 7.6425781 L 10.601562 7.7949219 L 10.791016 7.9277344 L 10.994141 8.0390625 L 11.208984 8.125 L 14.064453 9.0761719 L 13.992188 9.7089844 L 13.755859 9.7011719 L 13.523438 9.7285156 L 13.294922 9.7929688 L 13.082031 9.890625 L 12.886719 10.021484 L 12.712891 10.181641 L 12.568359 10.365234 L 12.453125 10.572266 L 12.371094 10.792969 L 12.326172 11.025391 L 12.091797 11.015625 L 11.857422 11.042969 L 11.630859 11.107422 L 11.416016 11.205078 L 11.220703 11.333984 L 11.048828 11.496094 L 10.902344 11.679688 L 10.789062 11.886719 L 10.707031 12.107422 L 10.660156 12.337891 L 10.425781 12.328125 L 10.191406 12.357422 L 9.9648438 12.419922 L 9.7519531 12.519531 L 9.5566406 12.650391 L 9.3847656 12.808594 L 9.2382812 12.996094 L 9.1230469 13.199219 L 9.0429688 13.421875 L 8.9960938 13.652344 L 8.7617188 13.642578 L 8.5273438 13.671875 L 8.3007812 13.734375 L 8.0878906 13.833984 L 7.8925781 13.964844 L 7.71875 14.125 L 7.5722656 14.310547 L 7.4570312 14.515625 L 7.3769531 14.736328 L 7.3320312 14.966797 L 7.0957031 14.958984 L 6.8632812 14.986328 L 6.6367188 15.050781 L 6.421875 15.148438 L 6.2265625 15.279297 L 6.0546875 15.439453 L 5.9082031 15.625 L 5.7929688 15.830078 L 5.7128906 16.050781 L 5.6660156 16.28125 L 5.4296875 16.271484 L 5.1972656 16.298828 L 4.96875 16.365234 L 4.7578125 16.462891 L 4.5625 16.59375 L 4.3886719 16.753906 L 4.2441406 16.939453 L 4.1289062 17.144531 L 4.046875 17.365234 L 4 17.595703 L 3.7734375 17.587891 L 3.5488281 17.613281 L 3.3261719 17.671875 L 3.1191406 17.761719 L 2.9277344 17.884766 L 2.7558594 18.033203 L 2.609375 18.207031 L 1.1328125 16.732422 L 1.0527344 16.626953 L 1.0019531 16.505859 L 0.98632812 16.376953 L 1.0039062 16.248047 L 1.0546875 16.126953 L 1.1328125 16.023438 L 9.9726562 7.1855469 z "></path>{" "}
            </g>{" "}
          </g>
        </svg>
      ),
    },
  ];

  const managerButtons: ButtonConfig[] = [];

  const adminButtons: ButtonConfig[] = [
    {
      id: "7",
      label: "Register User",
      onClick: () => router.push("/dashboard/register-user"),
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
    /*{
      id: "8",
      label: "Manage Issues",
      onClick: () => router.push("/manage-issues"),
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
    {
      id: "9",
      label: "Manage Users",
      onClick: () => router.push("/manage-users"),
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
      id: "10",
      label: "Manage Rooms",
      onClick: () => router.push("/manage-rooms"),
      icon: (
        <svg width="64px" height="64px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg" transform="matrix(-1, 0, 0, 1, 0, 0)">
          <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
          <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
          <g id="SVGRepo_iconCarrier">
            <path d="M15 4H18C19.1046 4 20 4.89543 20 6V18C20 19.1046 19.1046 20 18 20H15M8 8L4 12M4 12L8 16M4 12L16 12" stroke="#000000" strokeWidth="1.56" strokeLinecap="round" strokeLinejoin="round"></path>
          </g>
        </svg>
      ),
    },*/
    {
      id: "11",
      label: "ErrorType Create",
      onClick: () => router.push("/dashboard/errortype-create"),
      icon: (
        <svg width="64px" height="64px" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg" fill="#000000" stroke="#000000" strokeWidth="0.00016">
          <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
          <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
          <g id="SVGRepo_iconCarrier">
            {" "}
            <g fill="#2e3436">
              {" "}
              <path d="m 13.550781 4 c -0.488281 0.003906 -0.988281 0.195312 -1.429687 0.636719 l -7.625 7.625 c -0.316406 0.316406 -0.496094 0.75 -0.496094 1.199219 v 2.539062 h 2.539062 c 0.449219 0 0.882813 -0.179688 1.199219 -0.496094 l 7.625 -7.625 c 1.515625 -1.511718 0.066407 -3.714844 -1.601562 -3.871094 c -0.070313 -0.007812 -0.140625 -0.007812 -0.210938 -0.007812 z m -1.503906 3.054688 l 0.898437 0.898437 l -5.980468 5.980469 l -0.898438 -0.898438 z m 0 0"></path>{" "}
              <path d="m 5 1 s -0.707031 -0.015625 -1.449219 0.355469 c -0.738281 0.371093 -1.550781 1.3125 -1.550781 2.644531 v 2 h -2 v 1 h 0.0078125 c -0.00390625 0.265625 0.1015625 0.519531 0.2851565 0.707031 l 2 2 c 0.390625 0.390625 1.023437 0.390625 1.414062 0 l 2 -2 c 0.183594 -0.1875 0.289063 -0.441406 0.289063 -0.707031 h 0.003906 v -1 h -2 v -2 c 0.105469 -0.800781 0.5 -1 1 -1 h 5 c 0.550781 0 1 -0.449219 1 -1 s -0.449219 -1 -1 -1 z m 0 0"></path>{" "}
            </g>{" "}
          </g>
        </svg>
      ),
    },
    {
      id: "12",
      label: "ErrorTypes",
      onClick: () => router.push("/dashboard/errortypes"),
      icon: (
        <svg fill="#000000" width="64px" height="64px" viewBox="0 0 1024 1024" xmlns="http://www.w3.org/2000/svg" stroke="#000000" strokeWidth="5">
          <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
          <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
          <g id="SVGRepo_iconCarrier">
            <path d="M520.741 163.801a10.234 10.234 0 00-3.406-3.406c-4.827-2.946-11.129-1.421-14.075 3.406L80.258 856.874a10.236 10.236 0 00-1.499 5.335c0 5.655 4.585 10.24 10.24 10.24h846.004c1.882 0 3.728-.519 5.335-1.499 4.827-2.946 6.352-9.248 3.406-14.075L520.742 163.802zm43.703-26.674L987.446 830.2c17.678 28.964 8.528 66.774-20.436 84.452a61.445 61.445 0 01-32.008 8.996H88.998c-33.932 0-61.44-27.508-61.44-61.44a61.445 61.445 0 018.996-32.008l423.002-693.073c17.678-28.964 55.488-38.113 84.452-20.436a61.438 61.438 0 0120.436 20.436zM512 778.24c22.622 0 40.96-18.338 40.96-40.96s-18.338-40.96-40.96-40.96-40.96 18.338-40.96 40.96 18.338 40.96 40.96 40.96zm0-440.32c-22.622 0-40.96 18.338-40.96 40.96v225.28c0 22.622 18.338 40.96 40.96 40.96s40.96-18.338 40.96-40.96V378.88c0-22.622-18.338-40.96-40.96-40.96z"></path>
          </g>
        </svg>
      ),
    },
    {
      id: "13",
      label: "Manage Equipment",
      onClick: () => router.push("/dashboard/equipments"),
      icon: (
        <svg fill="#000000" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink" viewBox="0 0 490 490" xmlSpace="preserve" width="64px" height="64px" stroke="#000000" strokeWidth="4.9">
          <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
          <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
          <g id="SVGRepo_iconCarrier">
            {" "}
            <g>
              {" "}
              <g>
                {" "}
                <g>
                  {" "}
                  <path d="M480,220H300V10c0-5.522-4.477-10-10-10H10C4.477,0,0,4.478,0,10v470c0,5.522,4.477,10,10,10h265c5.523,0,10-4.478,10-10 v-30h120v30c0,5.522,4.477,10,10,10h35c5.523,0,10-4.478,10-10v-30h5c5.523,0,10-4.478,10-10V275h5c5.523,0,10-4.478,10-10v-35 C490,224.478,485.523,220,480,220z M140,470H20v-85h120V470z M140,365H20V20h120V365z M230,470h-70V20h120v200h-75 c-5.523,0-10,4.478-10,10v35c0,5.522,4.477,10,10,10h10v165c0,5.522,4.477,10,10,10h5V470z M265,470h-15v-20h15V470z M440,470 h-15v-20h15V470z M455,430H235v-70h220V430z M455,340H235v-65h220V340z M470,255H215v-15h255V255z"></path>{" "}
                  <rect x="330" y="295" width="30" height="20"></rect> <rect x="330" y="380" width="30" height="20"></rect> <rect x="180" y="160" width="20" height="40"></rect> <rect x="100" y="160" width="20" height="40"></rect> <rect x="65" y="415" width="30" height="20"></rect>{" "}
                </g>{" "}
              </g>{" "}
            </g>{" "}
          </g>
        </svg>
      ),
    },
    {
      id: "14",
      label: "Create Equipment",
      onClick: () => router.push("/dashboard/equipment-create"),
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
