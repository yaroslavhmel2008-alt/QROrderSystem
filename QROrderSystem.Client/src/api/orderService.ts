import { apiClient } from './axiosConfig';
import type {Product} from "../types";

export const getProducts = () => apiClient.get<Product[]>('/Product');