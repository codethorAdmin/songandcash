import UserProfile from "views/UserProfile.js";
import TableList from "views/TableList.js";

const dashboardRoutes = [
  {
    path: "/user",
    name: "Mi cuenta",
    icon: "nc-icon nc-circle-09",
    component: UserProfile,
    layout: "/admin",
  },
  {
    path: "/table",
    name: "Ventas recuperables",
    icon: "nc-icon nc-notes",
    component: TableList,
    layout: "/admin",
  },
  {
    path: "/table",
    name: "Cerrar sesión",
    icon: "nc-icon nc-button-power",
    component: TableList,
    layout: "/closeSesion",
  },
];

export default dashboardRoutes;
