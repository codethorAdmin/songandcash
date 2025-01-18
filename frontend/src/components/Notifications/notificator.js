import Notify from "simple-notify";
import "simple-notify/dist/simple-notify.css";

export const notify = (settings) => {
  const {
    status,
    title,
    text,
    effect,
    speed,
    customClass,
    customIcon,
    showIcon,
    showCloseButton,
    autoclose,
    autotimeout,
    gap,
    distance,
    type,
    position,
  } = settings;
  return new Notify(settings);
};

export const defaultSuccess = (title, message) => {
  return notify({
    status: "success",
    title: title,
    text: message,
    effect: "fade",
    speed: 300,
    customClass: null,
    customIcon: null,
    showIcon: true,
    showCloseButton: false,
    autoclose: true,
    autotimeout: 3000,
    gap: 20,
    distance: 20,
    type: "success",
    position: "top right",
  });
};

export const defaultError = (title, message) => {
  return notify({
    status: "error",
    title: title,
    text: message,
    effect: "fade",
    speed: 300,
    customClass: null,
    customIcon: null,
    showIcon: true,
    showCloseButton: false,
    autoclose: true,
    autotimeout: 3000,
    gap: 20,
    distance: 20,
    type: "error",
    position: "top right",
  });
};
