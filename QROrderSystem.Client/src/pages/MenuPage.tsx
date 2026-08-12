import { useContext, useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate, useSearchParams, Link } from 'react-router-dom';
import { ShoppingCart } from 'lucide-react';
import type { Product } from "../types";
import { ProductCard } from "../components/Product/ProductCard.tsx";
import { CartContext } from '../context/CartContext';
import * as signalR from "@microsoft/signalr";

const API_URL = import.meta.env.VITE_API_URL;

export function MenuPage() {
    const { cart } = useContext(CartContext);
    const [products, setProducts] = useState<Product[]>([]);
    const [categories, setCategories] = useState<{ id: string, name: string }[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [selectedCategoryId, setSelectedCategoryId] = useState<string | null>(null);
    const [locationName, setLocationName] = useState<string>("");
    
    const [searchQuery, setSearchQuery] = useState<string>("");

    const [searchParams] = useSearchParams();
    const navigate = useNavigate();
    const locationId = searchParams.get('locationId');

    useEffect(() => {
        const fetchLocationName = async () => {
            const locId = locationId || localStorage.getItem('currentLocationId');

            if (locId) {
                if (locationId) {
                    localStorage.setItem('currentLocationId', locationId);
                }
                try {
                    const res = await axios.get(`${API_URL}/api/Location`);
                    const found = res.data.find((l: any) => l.id === locId);
                    if (found) setLocationName(found.name);
                } catch (err) {
                    console.error("Не вдалося завантажити ім'я альтанки", err);
                }
            }
        };
        fetchLocationName();
    }, [locationId]);

    useEffect(() => {
        if (locationId) {
            localStorage.setItem('currentLocationId', locationId);
        }
    }, [locationId, navigate]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const [catRes, prodRes] = await Promise.all([
                    axios.get(`${API_URL}/api/Categories`),
                    axios.get(`${API_URL}/api/Product`)
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
            .withUrl(`${API_URL}/orderHub`)
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

    
    const filteredProducts = products.filter(p => {
        const matchesCategory = selectedCategoryId ? p.categoryId === selectedCategoryId : true;
        const matchesSearch = p.name.toLowerCase().includes(searchQuery.toLowerCase());
        return matchesCategory && matchesSearch;
    });

    if (loading) return <div className="text-center p-10 text-white font-serif">Завантаження...</div>;
    if (error) return <div className="text-center p-10 text-red-500 font-serif">{error}</div>;

    return (
        <div className="bg-[#1a2521] min-h-screen pb-12 text-white font-serif">
            {/* Адаптивна шапка */}
            <div className="relative bg-[#223729] pt-6 pb-10 px-4 sm:px-6 shadow-[0_10px_15px_-3px_rgba(0,0,0,0.3)]">
                {/* Кошик */}
                <Link to="/cart" className="absolute top-6 right-4 sm:right-6 z-20 flex items-center p-2.5 bg-[#1a2521]/40 rounded-full border border-[#d4af37]/20 shadow-md">
                    <ShoppingCart className="w-5 h-5 sm:w-6 sm:h-6 text-[#d4af37]" />
                    {cart.length > 0 && (
                        <span className="absolute -top-1 -right-1 bg-[#d4af37] text-[#1a2521] text-[10px] font-bold w-5 h-5 flex items-center justify-center rounded-full shadow-lg">
                            {cart.reduce((sum: number, item: any) => sum + item.quantity, 0)}
                        </span>
                    )}
                </Link>

                <div className="absolute top-0 right-0 w-40 h-40 bg-[#d4af37] opacity-[0.07] blur-3xl rounded-full"></div>

                <div className="relative z-10">
                    <p className="text-[10px] uppercase tracking-[0.2em] text-[#d4af37] mb-1">Відпочинок у Смусика</p>
                    <h1 className="text-3xl sm:text-4xl font-bold text-white mb-3">Меню</h1>
                    <div className="inline-flex items-center gap-2 bg-[#1a2521]/40 border border-[#d4af37]/20 px-3.5 py-1.5 rounded-full text-xs sm:text-sm">
                        <span>Ваша альтанка: <span className="font-bold">№{locationName || "..."}</span></span>
                    </div>
                </div>

                <div className="absolute bottom-0 left-0 w-full overflow-hidden leading-none">
                    <svg className="relative block w-full h-6 sm:h-8" viewBox="0 0 1200 120" preserveAspectRatio="none">
                        <path d="M0,0V46.29c47.79,22.2,103.59,32.17,158,28,61.79-4.8,116.36-21.29,179-28,78.23-8.31,162.77-9.52,241,12,65.86,18.39,127.18,48.8,193,48.8,79.52,0,157.57-25.77,236-40.84,65.71-12.67,133.25-18.7,201-18.7h0V0Z" fill="#1a2521"></path>
                    </svg>
                </div>
            </div>
            
            <div className="px-4 sm:px-6 mt-4">
                <input
                    type="text"
                    placeholder="🔍 Знайти у меню..."
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                    className="w-full bg-[#2a3833] border border-[#3b4d47] p-3 rounded-2xl text-white placeholder-gray-400 focus:outline-none focus:border-[#d4af37] text-sm sm:text-base shadow-md"
                />
            </div>
            
            <div className="px-4 sm:px-6 mt-3 mb-4">
                <div className="flex gap-2.5 overflow-x-auto pb-2 scrollbar-hide">
                    {categories.map((cat) => (
                        <button
                            key={cat.id}
                            onClick={() => setSelectedCategoryId(cat.id)}
                            className={`flex-none px-5 py-2 rounded-full text-sm font-medium transition-all ${
                                selectedCategoryId === cat.id ? 'bg-[#d4af37] text-[#1a2521] font-bold shadow-md' : 'bg-[#2a3833] text-gray-300 border border-[#3b4d47]'
                            }`}
                        >
                            {cat.name}
                        </button>
                    ))}
                </div>
            </div>
            
            <div className="space-y-4 px-4 sm:px-6">
                {filteredProducts.length > 0 ? (
                    filteredProducts.map(p => <ProductCard key={p.id} product={p} />)
                ) : (
                    <p className="text-center text-gray-500 py-10 text-sm">Нічого не знайдено за вашим запитом.</p>
                )}
            </div>
        </div>
    );
}