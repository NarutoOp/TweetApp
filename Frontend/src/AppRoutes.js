import Login from "./components/Login";
import Registration from "./components/Registration";
import Feed from "./components/Feed";
import Users from "./components/Users";
import UserTweets from "./components/UserTweets";

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
  {
    path: "/Users",
    element: <Users />,
  },
  {
    path: "/UserTweets",
    element: <UserTweets />,
  },
];

export default AppRoutes;
