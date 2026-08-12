import { useEffect, useMemo, useState } from 'react';
import axios from 'axios';
import * as signalR from "@microsoft/signalr";
import notificationSound from '../assets/sounds/ding.mp3';
import { AdminProducts } from '../components/Admin/AdminProducts';
import { AdminCategories } from '../components/Admin/AdminCategories';
import { AdminOrders } from '../components/Admin/AdminOrders';
import { AdminLocations } from "../components/Admin/AdminLocations.tsx";

const API_URL = import.meta.env.VITE_API_URL;
const ADMIN_PASSWORD = import.meta.env.VITE_ADMIN_PASSWORD || 123;

export function AdminPage() {
    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(
        localStorage.getItem('isAdminAuth') === 'true'
    );
    const [passwordInput, setPasswordInput] = useState<string>('');
    const [error, setError] = useState<string>('');

    const [activeTab, setActiveTab] = useState<'products' | 'categories' | 'orders' | 'locations'>('products');
    
    const [orders, setOrders] = useState<any[]>([]);
    const [audioUnlocked, setAudioUnlocked] = useState<boolean>(false);

    const audio = useMemo(() => new Audio(notificationSound), []);

    const unlockAudio = () => {
        if (!audioUnlocked) {
            audio.play().then(() => {
                audio.pause();
                audio.currentTime = 0;
                setAudioUnlocked(true);
            }).catch(err => console.log("Не вдалося розблокувати аудіо", err));
        }
    };

    useEffect(() => {
        if (isAuthenticated) {
            fetchOrders();
        }
    }, [isAuthenticated]);
    
    useEffect(() => {
        if (!isAuthenticated) return;

        const connection = new signalR.HubConnectionBuilder()
            .withUrl(`${API_URL}/orderHub`)
            .withAutomaticReconnect()
            .build();

        connection.on("ReceiveOrder", (newOrder) => {
            setOrders(prev => [newOrder, ...prev]);
            audio.play().catch(err => {
                console.log("Звук заблоковано браузером:", err);
            });
        });

        connection.start().catch(err => console.error("Помилка SignalR:", err));

        return () => { connection.stop(); };
    }, [isAuthenticated, audio]);

    const fetchOrders = async () => {
        try {
            const res = await axios.get(`${API_URL}/api/Order`);
            setOrders(res.data);
        } catch (err) {
            console.error("Помилка завантаження замовлень:", err);
        }
    };

    const activeOrdersCount = useMemo(() => {
        return orders.filter(o => {
            const s = String(o.status);
            return s !== 'Paid' && s !== '1' && s !== 'Cancelled' && s !== '2';
        }).length;
    }, [orders]);

    const handleLogin = (e: React.FormEvent) => {
        e.preventDefault();
        if (passwordInput === String(ADMIN_PASSWORD)) {
            setIsAuthenticated(true);
            localStorage.setItem('isAdminAuth', 'true');
            setError('');
        } else {
            setError('Невірний пароль!');
        }
    };

    const handleLogout = () => {
        setIsAuthenticated(false);
        localStorage.removeItem('isAdminAuth');
    };

    if (!isAuthenticated) {
        return (
            <div className="bg-[#1a2521] min-h-screen flex items-center justify-center text-white font-serif p-4">
                <form onSubmit={handleLogin} className="bg-[#2a3833] p-6 sm:p-8 rounded-2xl border border-[#3b4d47] w-full max-w-md shadow-2xl">
                    <h2 className="text-xl sm:text-2xl font-bold mb-6 text-center text-[#d4af37]">Вхід в Адмін-панель</h2>

                    <div className="mb-4">
                        <label className="block text-sm text-gray-300 mb-2">Введіть пароль:</label>
                        <input
                            type="password"
                            value={passwordInput}
                            onChange={(e) => setPasswordInput(e.target.value)}
                            className="w-full bg-[#1a2521] border border-[#3b4d47] rounded-lg px-4 py-3 text-white focus:outline-none focus:border-[#d4af37]"
                            placeholder="Пароль..."
                            autoFocus
                        />
                    </div>

                    {error && <p className="text-red-500 text-sm mb-4">{error}</p>}

                    <button
                        type="submit"
                        className="w-full bg-[#d4af37] text-[#1a2521] font-bold py-3 rounded-lg hover:bg-[#c29b30] transition-colors"
                    >
                        Увійти
                    </button>
                </form>
            </div>
        );
    }

    return (
        <div className="p-4 sm:p-8 bg-[#1a2521] min-h-screen text-white font-serif" onClick={unlockAudio}>
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-2xl sm:text-3xl font-bold">Адмін-панель</h1>
                <button
                    onClick={handleLogout}
                    className="border border-red-500 text-red-400 px-3 sm:px-4 py-2 rounded-lg hover:bg-red-500/10 transition-all text-xs sm:text-sm shrink-0"
                >
                    Вийти
                </button>
            </div>
            
            <div className="grid grid-cols-2 sm:flex gap-2 sm:gap-4 mb-6 sm:mb-8">
                {(['products', 'categories', 'orders', 'locations'] as const).map(tab => (
                    <button
                        key={tab}
                        onClick={() => setActiveTab(tab)}
                        className={`relative px-3 sm:px-4 py-2.5 rounded-lg font-semibold transition-colors border text-center text-sm sm:text-base ${
                            activeTab === tab
                                ? 'bg-[#d4af37] text-[#1a2521] border-[#d4af37]'
                                : 'bg-[#2a3833] text-[#d4af37] border-[#3b4d47] hover:bg-[#d4af37]/10'
                        }`}
                    >
                        {tab === 'products' ? 'Товари' :
                            tab === 'categories' ? 'Категорії' :
                                tab === 'orders' ? (
                                    <>
                                        Замовлення
                                        {activeOrdersCount > 0 && (
                                            <span className="absolute -top-2 -right-2 bg-red-600 text-white text-xs font-bold w-5 h-5 flex items-center justify-center rounded-full shadow-md animate-pulse">
                                                {activeOrdersCount}
                                            </span>
                                        )}
                                    </>
                                ) : 'Альтанки'}
                    </button>
                ))}
            </div>

            <div className={activeTab === 'products' ? 'block' : 'hidden'}>
                <AdminProducts />
            </div>
            <div className={activeTab === 'categories' ? 'block' : 'hidden'}>
                <AdminCategories />
            </div>
            <div className={activeTab === 'orders' ? 'block' : 'hidden'}>
                <AdminOrders orders={orders} setOrders={setOrders} fetchOrders={fetchOrders} />
            </div>
            <div className={activeTab === 'locations' ? 'block' : 'hidden'}>
                <AdminLocations />
            </div>
        </div>
    );
}