import UserProfile from "views/UserProfile.js";
import RecoverableSales from "views/RecoverableSales";

const dashboardRoutes = [
  {
    path: "/user",
    name: "Mi cuenta",
    icon: "nc-icon nc-circle-09",
    component: UserProfile,
    layout: "/admin",
  },
  {
    path: "/recoverablesales",
    name: "Ventas recuperables",
    icon: "nc-icon nc-notes",
    component: RecoverableSales,
    layout: "/admin",
  },
];

export default dashboardRoutes;
