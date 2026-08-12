import { useEffect, useMemo, useState, FC } from "react";
import axios from 'axios';

const API_URL = import.meta.env.VITE_API_URL;
const ADMIN_PASSWORD = import.meta.env.VITE_ADMIN_PASSWORD || "123";

export const StatisticsDashboard: FC = () => {
    const [orders, setOrders] = useState<any[]>([]);
    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(
        localStorage.getItem('isStatisticsAuth') === 'true'
    );
    const [passwordInput, setPasswordInput] = useState<string>('');
    const [error, setError] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(true);

    const handleLogin = (e: React.FormEvent) => {
        e.preventDefault();
        if (passwordInput === String(ADMIN_PASSWORD)) {
            setIsAuthenticated(true);
            localStorage.setItem('isStatisticsAuth', 'true');
            setError('');
        } else {
            setError('Невірний пароль!');
        }
    };
    
    useEffect(() => {
        if (isAuthenticated) {
            const fetchOrders = async () => {
                try {
                    const res = await axios.get(`${API_URL}/api/Order`);
                    setOrders(res.data);
                } catch (err) {
                    console.error("Помилка завантаження статистики замовлень:", err);
                } finally {
                    setLoading(false);
                }
            };
            fetchOrders();
        }
    }, [isAuthenticated]);

    const stats = useMemo(() => {
        const productStats: Record<string, number> = {};
        let revenue = 0;
        let count = 0;
        
        const actualOrders = Array.isArray(orders[0]) ? orders[0] : orders;

        actualOrders.forEach(order => {
            const s = String(order.status);
            const isCancelled = s === 'Cancelled' || s === '2' || s === 'Скасовано';

            if (!isCancelled) {
                revenue += parseFloat(order.totalAmount || order.total || 0);
                count++;

                order.orderItems?.forEach((item: any) => {
                    const name = item.productName || item.name || "Без назви";
                    productStats[name] = (productStats[name] || 0) + (item.quantity || 1);
                });
            }
        });

        const sortedProducts = Object.entries(productStats).sort((a: any, b: any) => (b[1] as number) - (a[1] as number));

        return { sortedProducts, revenue, count };
    }, [orders]);
    
    if (!isAuthenticated) {
        return (
            <div className="bg-[#1a2521] min-h-screen flex items-center justify-center text-white font-serif p-4">
                <form onSubmit={handleLogin} className="bg-[#2a3833] p-6 sm:p-8 rounded-2xl border border-[#3b4d47] w-full max-w-md shadow-2xl">
                    <h2 className="text-xl sm:text-2xl font-bold mb-6 text-center text-[#d4af37]">Вхід до статистики</h2>

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

    if (loading) {
        return <div className="text-center p-10 text-white font-serif bg-[#1a2521] min-h-screen">Завантаження статистики...</div>;
    }
    
    return (
        <div className="bg-[#1a2521] min-h-screen p-4 sm:p-8 text-white font-serif">
            <h2 className="text-2xl sm:text-3xl font-bold mb-6 sm:mb-8 text-[#d4af37]">Статистика</h2>

            <div className="grid grid-cols-1 sm:grid-cols-3 gap-4 mb-8">
                <div className="bg-[#2a3833] p-5 rounded-2xl border border-[#3b4d47] shadow-lg">
                    <p className="text-gray-400 text-sm mb-1">Загальний виторг</p>
                    <p className="text-2xl sm:text-3xl font-bold text-[#d4af37]">{stats.revenue.toFixed(2)} грн</p>
                </div>

                <div className="bg-[#2a3833] p-5 rounded-2xl border border-[#3b4d47] shadow-lg">
                    <p className="text-gray-400 text-sm mb-1">Топ товар</p>
                    <p className="text-lg sm:text-xl font-bold truncate text-white">
                        {stats.sortedProducts.length > 0 ? stats.sortedProducts[0][0] : "Немає даних"}
                    </p>
                </div>

                <div className="bg-[#2a3833] p-5 rounded-2xl border border-[#3b4d47] shadow-lg">
                    <p className="text-gray-400 text-sm mb-1">Всього замовлень</p>
                    <p className="text-2xl sm:text-3xl font-bold text-white">{stats.count}</p>
                </div>
            </div>

            <h3 className="text-lg sm:text-xl font-bold mb-4 text-[#d4af37]">Популярність товарів</h3>
            <div className="space-y-3">
                {stats.sortedProducts.length === 0 ? (
                    <p className="text-gray-400 text-center py-6">Ще немає даних для відображення статистики.</p>
                ) : (
                    stats.sortedProducts.map(([name, count]) => (
                        <div key={name} className="flex justify-between items-center bg-[#2a3833] p-4 rounded-xl border border-[#3b4d47]">
                            <span className="text-sm sm:text-base pr-2">{name}</span>
                            <span className="font-bold text-[#d4af37] shrink-0">{count} шт.</span>
                        </div>
                    ))
                )}
            </div>
        </div>
    );
};