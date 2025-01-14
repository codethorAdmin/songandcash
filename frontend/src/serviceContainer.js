import React, { createContext, useContext } from "react";
import RecoverableSalesService from "./services/recoverableSalesService";
import UserService from "./services/userService";
import { useSettings } from "context/SettingsContext";

const createServices = (config) => {
  return {
    recoverableSalesService: new RecoverableSalesService(config),
    userService: new UserService(config),
  };
};

export const ServiceContext = createContext(null);

export const ServiceProvider = ({ children, config }) => {
  const { settings } = useSettings();
  const apiUrl = settings.apiUrl;

  const services = createServices({
    ...config,
    apiUrl: apiUrl,
    userId: localStorage.getItem("userId"),
  });

  return (
    <ServiceContext.Provider value={services}>
      {children}
    </ServiceContext.Provider>
  );
};

export const useServices = () => {
  const context = useContext(ServiceContext);
  if (!context) {
    throw new Error("useServices must be used within a ServiceProvider");
  }
  return context;
};
