import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

export function AuthCallback() {
  const navigate = useNavigate();
  const { validateAndSetUser } = useAuth();

  useEffect(() => {
    const params = new URLSearchParams(window.location.search);
    const token = params.get("token");

    if (token) {
      validateAndSetUser(token);
      navigate("/");
    } else {
      navigate("/auth/error");
    }
  }, []);

  return <div>Loading...</div>;
}
