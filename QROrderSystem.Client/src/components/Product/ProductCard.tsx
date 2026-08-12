import { useContext } from 'react';
import { CartContext } from "../../context/CartContext";
import type { Product } from "../../types";

const API_URL = import.meta.env.VITE_API_URL;

interface Props {
    product: Product;
}

export const ProductCard = ({ product }: Props) => {
    const { cart, addToCart, updateQuantity } = useContext(CartContext);

    const cartItem = cart.find((item: any) => item.id === product.id);
    const quantity = cartItem ? cartItem.quantity : 0;

    return (
        <div className="bg-[#2a3833] p-4 sm:p-5 rounded-2xl flex justify-between items-center border border-[#3b4d47] gap-4">
            {product.imageUrl && (
                <img
                    src={`${API_URL}${product.imageUrl}`}
                    alt={product.name}
                    className="w-20 h-20 sm:w-24 sm:h-24 object-cover rounded-xl border border-[#3b4d47] flex-shrink-0"
                />
            )}

            <div className="flex-1 pr-2">
                <h3 className="text-xl font-bold text-white mb-1">{product.name}</h3>
                <p className="text-sm text-gray-400 mb-2">{product.description}</p>
                <p className="text-[#d4af37] font-bold text-lg">{product.price} грн</p>
            </div>

            {quantity > 0 ? (
                <div className="flex items-center gap-3 bg-[#1a2521] px-4 py-2 rounded-full border border-[#d4af37]">
                    <button onClick={() => updateQuantity(product.id, -1)} className="text-[#d4af37] font-bold text-lg">-</button>
                    <span className="font-bold text-white">{quantity}</span>
                    <button onClick={() => updateQuantity(product.id, 1)} className="text-[#d4af37] font-bold text-lg">+</button>
                </div>
            ) : (
                <button
                    onClick={() => addToCart(product)}
                    className="w-12 h-12 bg-[#d4af37] rounded-full flex items-center justify-center text-2xl text-[#1a2521] active:scale-95 transition-transform flex-shrink-0"
                >
                    +
                </button>
            )}
        </div>
    );
};