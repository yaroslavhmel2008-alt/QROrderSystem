import { useContext } from "react";
import { CartContext } from "../context/CartContext.tsx";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";
import type { CartItem } from "../types";
import { Header } from "../components/Layout/Header.tsx";

const API_URL = import.meta.env.VITE_API_URL;

export function CartPage() {
    const { cart, clearCart, updateQuantity } = useContext(CartContext);
    const navigate = useNavigate();
    const total = cart.reduce((sum: number, item: CartItem) => sum + (item.price * item.quantity), 0);

    const handleOrderSubmit = async () => {
        let locId = localStorage.getItem('currentLocationId');

        if (!locId) {
            alert("Помилка: не знайдено альтанку. Будь ласка, відскануйте QR-код ще раз.");
            return;
        }

        const orderData = {
            LocationId: locId,
            Items: cart.map((item: CartItem) => ({
                ProductId: item.id,
                Quantity: item.quantity
            }))
        };

        try {
            const response = await axios.post(`${API_URL}/api/Order`, orderData);
            const newOrderId = response.data.id;
            localStorage.setItem('myOrderId', newOrderId);
            alert("Замовлення успішно відправлено!");
            clearCart();
            navigate('/');
        } catch (error: any) {
            const errorMsg = error.response?.data?.message || error.message;
            alert("Помилка при оформленні: " + errorMsg + " (Статус: " + (error.response?.status || "немає") + ")");
        }
    };

    return (
        <div className="bg-[#1a2521] min-h-screen p-4 sm:p-6 text-white font-serif">
            <Header title="Кошик" />
            <h2 className="text-2xl sm:text-3xl font-bold mb-6 sm:mb-8 text-center text-[#d4af37]">Ваше замовлення</h2>

            <Link to="/" className="block text-center text-[#d4af37] hover:text-white transition-colors mb-6 text-sm sm:text-base">
                ← Повернутися до меню
            </Link>

            {cart.length === 0 ? (
                <p className="text-center text-gray-400 py-10">Ваш кошик поки порожній</p>
            ) : (
                <div className="space-y-4 mb-8">
                    {cart.map((item: CartItem) => (
                        <div key={item.id} className="bg-[#2a3833] p-4 sm:p-5 rounded-2xl flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 border border-[#3b4d47]">
                            <span className="font-bold text-base sm:text-lg">{item.name}</span>

                            <div className="flex items-center justify-between w-full sm:w-auto gap-4">
                                <div className="flex items-center gap-3 bg-[#1a2521] px-3 py-1.5 rounded-full border border-[#3b4d47]">
                                    <button onClick={() => updateQuantity(item.id, -1)} className="text-[#d4af37] font-bold text-lg px-2">-</button>
                                    <span className="font-bold w-6 text-center">{item.quantity}</span>
                                    <button onClick={() => updateQuantity(item.id, 1)} className="text-[#d4af37] font-bold text-lg px-2">+</button>
                                </div>

                                <span className="font-bold text-[#d4af37] text-base sm:text-lg">{item.price * item.quantity} грн</span>
                            </div>
                        </div>
                    ))}

                    <div className="flex justify-between items-center px-4 pt-6 text-xl sm:text-2xl font-bold border-t border-[#3b4d47] mt-6">
                        <span>Разом:</span>
                        <span className="text-[#d4af37]">{total} грн</span>
                    </div>
                </div>
            )}

            {cart.length > 0 && (
                <button
                    onClick={handleOrderSubmit}
                    className="w-full bg-[#d4af37] hover:bg-[#b89630] text-[#1a2521] py-3.5 sm:py-4 rounded-2xl text-base sm:text-lg font-bold shadow-lg transition-all active:scale-95"
                >
                    Підтвердити замовлення
                </button>
            )}
        </div>
    );
}