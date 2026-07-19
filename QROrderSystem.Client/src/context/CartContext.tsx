import { createContext, useState } from 'react';

export const CartContext = createContext<any>(null);

export const CartProvider = ({ children }: any) => {
    const [cart, setCart] = useState<any[]>([]);

    const addToCart = (product: any) => {
        setCart(prevItems => {
            const existingItem = prevItems.find((item: any) => item.id === product.id);

            if (existingItem) {
                return prevItems.map((item: any) =>
                    item.id === product.id
                        ? { ...item, quantity: (item.quantity || 1) + 1 }
                        : item
                );
            }
            return [...prevItems, { ...product, quantity: 1 }];
        });
    };

    const updateQuantity = (id: string, delta: number) => {
        setCart(prev =>
            prev.map(item =>
                item.id === id ? { ...item, quantity: item.quantity + delta } : item
            ).filter(item => item.quantity > 0)
        );
    };

    const clearCart = () => setCart([]);

    return (
        <CartContext.Provider value={{ cart, addToCart, clearCart, updateQuantity }}>
            {children}
        </CartContext.Provider>
    );
};