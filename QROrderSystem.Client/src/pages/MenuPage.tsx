import { useContext, useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate, useSearchParams, Link } from 'react-router-dom'; 
import { ShoppingCart } from 'lucide-react';
import type { Product } from "../types";
import { ProductCard } from "../components/Product/ProductCard.tsx";
import { CartContext } from '../context/CartContext';
import * as signalR from "@microsoft/signalr";

export function MenuPage() {
    const { cart } = useContext(CartContext);
    const [products, setProducts] = useState<Product[]>([]);
    const [categories, setCategories] = useState<{ id: string, name: string }[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [selectedCategoryId, setSelectedCategoryId] = useState<string | null>(null);
    const [locationName, setLocationName] = useState<string>("");

    const [searchParams] = useSearchParams();
    const navigate = useNavigate();
    const locationId = searchParams.get('locationId');

    useEffect(() => {
        const fetchLocationName = async () => {
            const locId = localStorage.getItem('currentLocationId');
            if (locId) {
                try {
                    const res = await axios.get('http://192.168.0.65:5219/api/Location');
                    const found = res.data.find((l: any) => l.id === locId);
                    if (found) setLocationName(found.name);
                } catch (err) {
                    console.error("Не вдалося завантажити ім'я альтанки", err);
                }
            }
        };
        fetchLocationName();
    }, []);

    useEffect(() => {
        if (locationId) {
            localStorage.setItem('currentLocationId', locationId);
        }
    }, [locationId, navigate]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const [catRes, prodRes] = await Promise.all([
                    axios.get('http://192.168.0.65:5219/api/Categories'),
                    axios.get('http://192.168.0.65:5219/api/Product')
                ]);
                setCategories(catRes.data);
                setProducts(prodRes.data);
                if (catRes.data?.length > 0) setSelectedCategoryId(catRes.data[0].id);
                setLoading(false);
            } catch (err) {
                setError("Не вдалося завантажити меню.");
                setLoading(false);
            }
        };
        fetchData();
    }, []);

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://192.168.0.65:5219/orderHub")
            .build();

        connection.on("ReceiveOrderStatusUpdate", (updatedOrder) => {
            console.log("Статус, який прийшов:", updatedOrder.status);

            const myOrderId = localStorage.getItem('myOrderId');

            if (updatedOrder.id.toLowerCase() === myOrderId?.toLowerCase()) {
                switch (updatedOrder.status) {
                    case 'Paid':
                        alert("Ваше замовлення успішно оплачено! Смачного!");
                        break;
                    case 'Cancelled':
                        alert("Ваше замовлення було скасовано.");
                        localStorage.removeItem('myOrderId');
                        break;
                    default:
                        console.log("Статус оновлено на:", updatedOrder.status);
                }
            }
        });

        connection.start().catch(err => console.error("Помилка SignalR:", err));

        return () => { connection.stop(); };
    }, []); 
    

    const filteredProducts = selectedCategoryId
        ? products.filter(p => p.categoryId === selectedCategoryId)
        : products;

    if (loading) return <div className="text-center p-10 text-white">Завантаження...</div>;
    if (error) return <div className="text-center p-10 text-red-600">{error}</div>;

    return (
        <div className="bg-[#1a2521] min-h-screen p-6 text-white font-serif">
            <div className="relative bg-[#223729] pt-8 pb-12 px-6 shadow-[0_10px_15px_-3px_rgba(0,0,0,0.3)]">
                {/* Кошик */}
                <Link to="/cart" className="absolute top-8 right-6 z-20 flex items-center p-2 bg-[#1a2521]/40 rounded-full border border-[#d4af37]/20">
                    <ShoppingCart className="w-6 h-6 text-[#d4af37]" />
                    {cart.length > 0 && (
                        <span className="absolute -top-1 -right-1 bg-[#d4af37] text-[#1a2521] text-[10px] font-bold w-5 h-5 flex items-center justify-center rounded-full shadow-lg">
                            {cart.reduce((sum: number, item: any) => sum + item.quantity, 0)}
                        </span>
                    )}
                </Link>

                <div className="absolute top-0 right-0 w-40 h-40 bg-[#d4af37] opacity-[0.07] blur-3xl rounded-full"></div>

                <div className="relative z-10">
                    <p className="text-[10px] uppercase tracking-[0.2em] text-[#d4af37]">Відпочинок у Смусика</p>
                    <h1 className="text-4xl font-bold text-white mb-4">Меню</h1>
                    <div className="inline-flex items-center gap-2 bg-[#1a2521]/40 border border-[#d4af37]/20 px-4 py-2 rounded-full">
                        <span className="text-[#d4af37]">🏠</span>
                        <span className="text-sm">Ваша альтанка: <span className="font-bold">№{locationName}</span></span>
                    </div>
                </div>

                <div className="absolute bottom-0 left-0 w-full overflow-hidden leading-none">
                    <svg className="relative block w-full h-8" viewBox="0 0 1200 120" preserveAspectRatio="none">
                        <path d="M0,0V46.29c47.79,22.2,103.59,32.17,158,28,61.79-4.8,116.36-21.29,179-28,78.23-8.31,162.77-9.52,241,12,65.86,18.39,127.18,48.8,193,48.8,79.52,0,157.57-25.77,236-40.84,65.71-12.67,133.25-18.7,201-18.7h0V0Z" fill="#1a2521"></path>
                    </svg>
                </div>
            </div>

            <div className="px-6 mt-4">
                <div className="flex gap-3 overflow-x-auto pb-4 scrollbar-hide mask-fade-right">
                    {categories.map((cat) => (
                        <button
                            key={cat.id}
                            onClick={() => setSelectedCategoryId(cat.id)}
                            className={`flex-none px-6 py-2 rounded-full font-medium transition-all ${
                                selectedCategoryId === cat.id ? 'bg-[#d4af37] text-[#1a2521]' : 'bg-[#2a3833] text-gray-300 border border-[#3b4d47]'
                            }`}
                        >
                            {cat.name}
                        </button>
                    ))}
                </div>
            </div>

            <div className="space-y-4 px-2">
                {filteredProducts.length > 0 ? (
                    filteredProducts.map(p => <ProductCard key={p.id} product={p} />)
                ) : (
                    <p className="text-center text-gray-500 py-10">У цій категорії поки немає товарів.</p>
                )}
            </div>
        </div>
    );
}