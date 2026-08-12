export interface Product {
    imageUrl: string;
    categoryId: string;
    id: string;
    name: string;
    price: number;
    description?: string;
}

export interface CartItem {
    id: string;
    name: string;
    price: number;
    quantity: number;
}

export interface Category {
    id: string;
    name: string;
}