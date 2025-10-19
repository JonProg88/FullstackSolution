import axios from "axios";

// DRY: baseURL viene del .env. Si no existe, cae al 7153.
export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "https://localhost:7153"
});

// Interceptor opcional: loguea errores en consola para debug.
api.interceptors.response.use(
  r => r,
  err => {
    console.error("API error:", err?.response?.status, err?.response?.data);
    return Promise.reject(err);
  }
);
