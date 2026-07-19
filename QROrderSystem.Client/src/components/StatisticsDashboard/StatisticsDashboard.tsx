import { useEffect, useMemo, FC } from "react";

interface StatisticsDashboardProps {
    orders: any[];
}

export const StatisticsDashboard: FC<StatisticsDashboardProps> = ({ orders }) => {
    useEffect(() => {
        console.log("Дані замовлень у дашборді:", orders);
    }, [orders]);

    const stats = useMemo(() => {
        const productStats: Record<string, number> = {};
        let revenue = 0;
        let count = 0;

        // Витягуємо масив замовлень
        const actualOrders = Array.isArray(orders[0]) ? orders[0] : orders;

        actualOrders.forEach(order => {
            // Фільтрація: ігноруємо скасовані
            const isCancelled = order.status === 'Cancelled' || order.status === 2 || order.status === 'Скасовано';

            if (!isCancelled) {
                revenue += parseFloat(order.totalAmount || 0);
                count++;

                order.orderItems?.forEach((item: any) => {
                    const name = item.productName || item.name || "Без назви";
                    productStats[name] = (productStats[name] || 0) + item.quantity;
                });
            }
        });

        const sortedProducts = Object.entries(productStats).sort((a: any, b: any) => (b[1] as number) - (a[1] as number));

        return { sortedProducts, revenue, count };
    }, [orders]);

    return (
        <div className="bg-[#1a2521] p-6 text-white font-serif rounded-2xl shadow-lg border border-[#3b4d47]">
            <h2 className="text-2xl font-bold mb-6 text-[#d4af37]">Статистика</h2>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
                <div className="bg-[#223729] p-4 rounded-xl border border-[#3b4d47]">
                    <p className="text-gray-400 text-sm">Загальний виторг</p>
                    <p className="text-3xl font-bold text-[#d4af37]">{stats.revenue.toFixed(2)} грн</p>
                </div>

                <div className="bg-[#223729] p-4 rounded-xl border border-[#3b4d47]">
                    <p className="text-gray-400 text-sm">Топ товар</p>
                    <p className="text-xl font-bold truncate">
                        {stats.sortedProducts.length > 0 ? stats.sortedProducts[0][0] : "Немає даних"}
                    </p>
                </div>

                <div className="bg-[#223729] p-4 rounded-xl border border-[#3b4d47]">
                    <p className="text-gray-400 text-sm">Всього замовлень</p>
                    <p className="text-3xl font-bold text-white">{stats.count}</p>
                </div>
            </div>

            <h3 className="text-lg font-bold mb-4 text-[#d4af37]">Популярність товарів</h3>
            <div className="space-y-2">
                {stats.sortedProducts.map(([name, count]) => (
                    <div key={name} className="flex justify-between bg-[#2a3833] p-3 rounded-lg">
                        <span>{name}</span>
                        <span className="font-bold text-[#d4af37]">{count} шт.</span>
                    </div>
                ))}
            </div>
        </div>
    );
};