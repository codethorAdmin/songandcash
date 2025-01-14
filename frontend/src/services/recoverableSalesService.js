import axios from "axios";
const API_URL = process.env.REACT_APP_API_URL;

class RecoverableSalesService {
  constructor(config) {
    this.axios = axios.create({
      baseURL: config.apiUrl,
      withCredentials: true,
    });

    this.axios.interceptors.request.use((config) => {
      const token = localStorage.getItem("token");
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    });
  }
}

export default RecoverableSalesService;
