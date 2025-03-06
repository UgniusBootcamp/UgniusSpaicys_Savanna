import axios from 'axios';

const apiBaseUrl = import.meta.env.VITE_API_BASE_URL;

const apiClient = axios.create({
  baseURL: apiBaseUrl,
  withCredentials: true,
  headers: {
    'Content-Type': 'application/json',
  },
});

const handleResponse = (response) => {
  const { success, status, message, data, errors } = response.data;

  if (success) {
    return { success, status, message, data };
  } else {
    return Promise.reject({ success, status, message, errors });
  }
};

const api = {
  async get(url, params = {}) {
    try {
      const response = await apiClient.get(url, { params });
      return handleResponse(response);
    } catch (error) {
      return handleError(error);
    }
  },

  async post(url, body = {}) {
    try {
      const response = await apiClient.post(url, body);
      return handleResponse(response);
    } catch (error) {
      return handleError(error);
    }
  },

  async put(url, body = {}) {
    try {
      const response = await apiClient.put(url, body);
      return handleResponse(response);
    } catch (error) {
      return handleError(error);
    }
  },

  async delete(url) {
    try {
      const response = await apiClient.delete(url);
      return handleResponse(response);
    } catch (error) {
      return handleError(error);
    }
  },

  async patch(url, body = {}) {
    try {
      const response = await apiClient.patch(url, body);
      return handleResponse(response);
    } catch (error) {
      return handleError(error);
    }
  },
};

const handleError = (error) => {
  if (error.response) {
    return Promise.reject(error.response.data);
  } else if (error.request) {
    return Promise.reject({ message: 'No response from server' });
  } else {
    return Promise.reject({ message: error.message });
  }
};

apiClient.interceptors.response.use((config) => {
  const token = localStorage.getItem('accessToken');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

export default api;
