import { useEffect, useMemo, useState } from 'react';
import axios from 'axios';

interface AdminOrdersProps {
    orders: any[];
    setOrders: React.Dispatch<React.SetStateAction<any[]>>;
    fetchOrders: () => void;
}

export const AdminOrders = ({ orders, setOrders, fetchOrders }: AdminOrdersProps) => {
    const [activeTab, setActiveTab] = useState<'active' | 'history'>('active');

    useEffect(() => {
        fetchOrders();
    }, []);

    const updateStatus = async (orderId: string, newStatus: string) => {
        const statusMap: Record<string, number> = { 'Pending': 0, 'Paid': 1, 'Cancelled': 2 };
        try {
            await axios.put(`${import.meta.env.VITE_API_URL}/api/Order/${orderId}/status`,
                statusMap[newStatus],
                { headers: { 'Content-Type': 'application/json' } }
            );
            setOrders(prev => prev.map(order =>
                order.id === orderId ? { ...order, status: newStatus } : order
            ));
        } catch (error: any) {
            alert("Не вдалося оновити статус.");
        }
    };
    
    const activeOrdersCount = useMemo(() => {
        return orders.filter(o => {
            const s = String(o.status);
            return s !== 'Paid' && s !== '1' && s !== 'Cancelled' && s !== '2';
        }).length;
    }, [orders]);

    const displayedOrders = useMemo(() => {
        return orders.filter(o => {
            const s = String(o.status);
            if (activeTab === 'active') {
                return s !== 'Paid' && s !== '1' && s !== 'Cancelled' && s !== '2';
            }
            return s === 'Paid' || s === 'Cancelled' || s === '1' || s === '2';
        });
    }, [orders, activeTab]);
    
    const formatOrderTime = (dateString?: string) => {
        if (!dateString) return "";
        const date = new Date(dateString);
        return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    };

    return (
        <div className="bg-[#1a2521] min-h-screen p-4 sm:p-8 text-white font-serif">

            <h2 className="text-2xl sm:text-3xl font-bold mb-6 sm:mb-8">Замовлення</h2>
            
            <div className="flex gap-3 sm:gap-4 mb-6 sm:mb-8">
                <button
                    onClick={() => setActiveTab('active')}
                    className={`relative flex-1 sm:flex-none px-6 py-2.5 rounded-lg border transition-all ${activeTab === 'active' ? 'bg-[#d4af37] text-[#1a2521] border-[#d4af37] font-bold' : 'border-[#d4af37] text-[#d4af37]'}`}
                >
                    Активні
                    {activeOrdersCount > 0 && (
                        <span className="absolute -top-2 -right-2 bg-red-600 text-white text-xs font-bold w-5 h-5 flex items-center justify-center rounded-full shadow-md animate-pulse">
                            {activeOrdersCount}
                        </span>
                    )}
                </button>
                <button
                    onClick={() => setActiveTab('history')}
                    className={`flex-1 sm:flex-none px-6 py-2.5 rounded-lg border transition-all ${activeTab === 'history' ? 'bg-[#d4af37] text-[#1a2521] border-[#d4af37] font-bold' : 'border-[#d4af37] text-[#d4af37]'}`}
                >
                    Історія
                </button>
            </div>
            
            <div className="space-y-4">
                {displayedOrders.length === 0 ? (
                    <p className="text-gray-400 text-center py-8">Немає замовлень у цій категорії.</p>
                ) : (
                    displayedOrders.map(o => (
                        <div key={o.id} className="bg-[#2a3833] p-4 sm:p-6 rounded-2xl border border-[#3b4d47]">
                            <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-2 mb-4">
                                <div>
                                    <h3 className="text-lg sm:text-xl font-bold">{o.locationName || "Альтанка"}</h3>
                                    {o.createdAt && (
                                        <p className="text-xs text-gray-400 mt-0.5">
                                            Час замовлення: <span className="text-[#d4af37] font-semibold">{formatOrderTime(o.createdAt)}</span>
                                        </p>
                                    )}
                                </div>
                                <span className="bg-[#1a2521] border border-[#d4af37] text-[#d4af37] px-3 py-1 rounded-lg text-xs sm:text-sm">
                                    {o.status === 1 || o.status === 'Paid' ? 'Оплачено' : o.status === 2 || o.status === 'Cancelled' ? 'Скасовано' : 'В очікуванні'}
                                </span>
                            </div>

                            <ul className="mb-6 space-y-2 border-t border-b border-[#3b4d47] py-3">
                                {o.orderItems?.map((item: any, i: number) => (
                                    <li key={i} className="flex justify-between text-sm sm:text-base text-gray-300">
                                        <span className="pr-2">{item.productName}</span>
                                        <span className="shrink-0 font-medium">{item.quantity} шт.</span>
                                    </li>
                                ))}
                            </ul>
                            
                            <div className="grid grid-cols-1 sm:grid-cols-3 gap-2">
                                {['Pending', 'Paid', 'Cancelled'].map((status) => (
                                    <button
                                        key={status}
                                        onClick={() => updateStatus(o.id, status)}
                                        className={`py-2.5 px-3 rounded-lg border text-sm transition-all text-center ${
                                            (o.status === status || (o.status === 0 && status === 'Pending') || (o.status === 1 && status === 'Paid') || (o.status === 2 && status === 'Cancelled'))
                                                ? 'bg-[#d4af37] text-[#1a2521] border-[#d4af37] font-bold'
                                                : 'bg-[#2a3833] border-[#d4af37] text-[#d4af37] hover:bg-[#d4af37]/10'
                                        }`}
                                    >
                                        {status === 'Pending' ? 'В очікуванні' : status === 'Paid' ? 'Оплачено' : 'Скасовано'}
                                    </button>
                                ))}
                            </div>
                        </div>
                    ))
                )}
            </div>
        </div>
    );
};