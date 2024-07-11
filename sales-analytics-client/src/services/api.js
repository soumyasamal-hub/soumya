import axios from 'axios';

const API_URL = 'https://localhost:3000/api';

const axiosInstance = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const login = async (user) => {
  return await axiosInstance.post('/auth/login', user);
};

export const getSalesRecords = async () => {
  return await axiosInstance.get('/salesrecords');
};

export const createSalesRecord = async (record) => {
  return await axiosInstance.post('/salesrecords', record);
};

export const updateSalesRecord = async (id, record) => {
  return await axiosInstance.put(`/salesrecords/${id}`, record);
};

export const deleteSalesRecord = async (id) => {
  return await axiosInstance.delete(`/salesrecords/${id}`);
};

export const getTotalSales = async () => {
  return await axiosInstance.get('/analytics/TotalSales');
};

export const getTopProducts = async () => {
  return await axiosInstance.get('/analytics/TopProducts');
};

export const getSalesTrends = async () => {
  return await axiosInstance.get('/analytics/SalesTrends');
};
