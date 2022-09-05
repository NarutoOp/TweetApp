import { createContext, useContext, useMemo } from "react";
import { useNavigate } from "react-router-dom";
import { useCookie } from "./useCookie";
const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useCookie("user", null);
  const navigate = useNavigate();

  const login = async (data) => {
    setUser(data);
    navigate("/");
  };

  const logout = () => {
    setUser(null);
    navigate("/login", { replace: true });
  };

  const value = useMemo(
    () => ({
      user,
      login,
      logout,
    }),
    [user]
  );
  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  return useContext(AuthContext);
};
