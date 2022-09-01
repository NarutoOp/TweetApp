import Login from "./components/Login";
import Registration from "./components/Registration";
import Feed from "./components/Feed";

const AppRoutes = [
  {
    index: true,
    element: <Feed />,
  },
  {
    path: "/Login",
    element: <Login />,
  },
  {
    path: "/Registration",
    element: <Registration />,
  },
];

export default AppRoutes;
