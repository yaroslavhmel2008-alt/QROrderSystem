import { useContext } from "react";
import { CartContext } from "../context/CartContext.tsx";
import axios from "axios";
import {Link, useNavigate} from "react-router-dom";
import type {CartItem} from "../types";
import {Header} from "../components/Layout/Header.tsx";

export function CartPage() {
    const { cart, clearCart, updateQuantity } = useContext(CartContext);
    const navigate = useNavigate();
    const total = cart.reduce((sum: any, item: any) => sum + (item.price * item.quantity), 0);
    const handleOrderSubmit = async () => {
        let locId = localStorage.getItem('currentLocationId');
        
        if (!locId) {
            alert("Помилка: не знайдено альтанку. Будь ласка, відскануйте QR-код ще раз.");
            return;
        }

        const orderData = {
            LocationId: locId,
            Items: cart.map((item: any) => ({
                ProductId: item.id,
                Quantity: item.quantity
            }))
        }; 
        
        try {
            const response = await axios.post("http://192.168.0.65:5219/api/Order", orderData);
            const newOrderId = response.data.id;
            localStorage.setItem('myOrderId', newOrderId);
            alert("Замовлення успішно відправлено!");
            clearCart();
            navigate('/')
        } catch (error: any) {
            alert("Помилка: " + error.message + " (Статус: " + (error.response?.status || "немає") + ")");
            const errorMsg = error.response?.data?.message || error.message;
            alert("Помилка при оформленні: " + errorMsg);
        }
    };

    return (
        <div className="bg-[#1a2521] min-h-screen p-6 text-white font-serif">
            <Header title="Кошик" />
            <h2 className="text-3xl font-bold mb-8 text-center text-[#d4af37]">Ваше замовлення</h2>
            <Link to="/" className="block text-center text-[#d4af37] hover:text-white transition-colors mb-6">
                ← Повернутися до меню
            </Link>

            {cart.length === 0 ? (
                <p className="text-center text-gray-400 py-10">Ваш кошик поки порожній</p>
            ) : (
                <div className="space-y-4 mb-8">
                    {cart.map((item: CartItem) => (
                        <div key={item.id} className="bg-[#2a3833] p-5 rounded-2xl flex justify-between items-center border border-[#3b4d47]">
                            <span className="font-bold text-lg">{item.name}</span>

                            <div className="flex items-center gap-4 bg-[#1a2521] px-4 py-2 rounded-full border border-[#3b4d47]">
                                <button onClick={() => updateQuantity(item.id, -1)} className="text-[#d4af37] font-bold text-xl">-</button>
                                <span className="font-bold w-8 text-center">{item.quantity}</span>
                                <button onClick={() => updateQuantity(item.id, 1)} className="text-[#d4af37] font-bold text-xl">+</button>
                            </div>

                            <span className="font-bold text-[#d4af37]">{item.price * item.quantity} грн</span>
                        </div>
                    ))}

                    <div className="flex justify-between items-center px-4 pt-6 text-2xl font-bold border-t border-[#3b4d47] mt-6">
                        <span>Разом:</span>
                        <span className="text-[#d4af37]">{total} грн</span>
                    </div>
                </div>
            )}

            {cart.length > 0 && (
                <button
                    onClick={handleOrderSubmit}
                    className="w-full bg-[#d4af37] hover:bg-[#b89630] text-[#1a2521] py-4 rounded-2xl text-lg font-bold shadow-lg transition-all active:scale-95"
                >
                    Підтвердити замовлення
                </button>
            )}
        </div>
    );
}