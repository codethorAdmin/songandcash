import React, { createContext, useContext, useState, useEffect } from "react";
import { useSettings } from "./SettingsContext";

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
  const { settings } = useSettings();
  const apiUrl = settings.apiUrl;

  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  const validateAndSetUser = async (token) => {
    try {
      const response = await fetch(`${apiUrl}/api/userinfo`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      if (response.ok) {
        const userData = await response.json();
        setUser(userData);
        localStorage.setItem("jwt_token", token);
        localStorage.setItem("user", JSON.stringify(userData));
      } else {
        logout();
      }
    } catch (error) {
      console.error("Auth validation error:", error);
      logout();
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const token = localStorage.getItem("jwt_token");
    if (token) {
      validateAndSetUser(token);
    } else {
      setLoading(false);
    }
  }, []);

  const login = () => {
    window.location.href = `${apiUrl}/auth/login`;
  };

  const logout = () => {
    localStorage.removeItem("jwt_token");
    setUser(null);
  };

  return (
    <AuthContext.Provider
      value={{ user, loading, login, logout, validateAndSetUser }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
