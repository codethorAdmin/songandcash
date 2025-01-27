import React from "react";
import ReactDOM from "react-dom/client";

import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

import { AuthProvider } from "context/AuthContext";
import { SettingsProvider } from "context/SettingsContext";
import { AuthCallback } from "components/AuthCallback";
import LogoutPage from "views/LogoutPage";
import RecoverableSales from "views/RecoverableSales";
import UserProfile from "views/UserProfile";

import "bootstrap/dist/css/bootstrap.min.css";
import "./assets/css/animate.min.css";
import "./assets/scss/light-bootstrap-dashboard-react.scss?v=2.0.0";
import "./assets/css/demo.css";
import "@fortawesome/fontawesome-free/css/all.min.css";

import AdminLayout from "layouts/Admin.js";
import { ServiceProvider } from "serviceContainer";

const root = ReactDOM.createRoot(document.getElementById("root"));
let isLoggedIn = false;

root.render(
  <React.StrictMode>
    <BrowserRouter>
      <SettingsProvider>
        <ServiceProvider>
          <AuthProvider>
            <Routes>
              <Route path="/auth/callback" element={<AuthCallback />} />
              <Route path="/logout" element={<LogoutPage />} />
              <Route path="/admin" element={<AdminLayout />}>
                <Route path="user" element={<UserProfile />} />
                <Route path="recoverablesales" element={<RecoverableSales />} />
              </Route>
              <Route path="/" element={<Navigate to="/admin" replace />} />
              <Route path="*" element={<Navigate to="/admin" replace />} />
            </Routes>
          </AuthProvider>
        </ServiceProvider>
      </SettingsProvider>
    </BrowserRouter>
  </React.StrictMode>
);
