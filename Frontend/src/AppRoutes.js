import Login from "./components/Login";
import Registration from "./components/Registration";
import Feed from "./components/Feed";
import Users from "./components/Users";
import UserTweets from "./components/UserTweets";
import { ProtectedRoute } from "./Auth/ProtectedRoute";
import ForgotPassword from "./components/ForgotPassword";

const AppRoutes = [
  {
    index: true,
    element: (
      <ProtectedRoute>
        <Feed />
      </ProtectedRoute>
    ),
  },
  {
    path: "/Login",
    element: <Login />,
  },
  {
    path: "/Registration",
    element: <Registration />,
  },
  {
    path: "/ForgotPassword",
    element: <ForgotPassword />,
  },
  {
    path: "/Users",
    element: (
      <ProtectedRoute>
        <Users />
      </ProtectedRoute>
    ),
  },
  {
    path: "/UserTweets",
    element: (
      <ProtectedRoute>
        <UserTweets />
      </ProtectedRoute>
    ),
  },
];

export default AppRoutes;
