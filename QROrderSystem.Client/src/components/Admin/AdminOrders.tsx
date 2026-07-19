import { useEffect, useMemo, useState } from 'react';
import axios from 'axios';
import * as signalR from "@microsoft/signalr";
import notificationSound from '../../assets/sounds/ding.mp3';

export const AdminOrders = () => {
    const [orders, setOrders] = useState<any[]>([]);
    const [locations, setLocations] = useState<any[]>([]);
    const [activeTab, setActiveTab] = useState<'active' | 'history'>('active');

    const audio = useMemo(() => new Audio(notificationSound), []);

    useEffect(() => {
        fetchOrders();
        axios.get('http://192.168.0.65:5219/api/Location').then(res => setLocations(res.data));
    }, []);

    // --- SIGNALR ТУТ ---
    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://192.168.0.65:5219/orderHub")
            .withAutomaticReconnect()
            .build();

        connection.on("ReceiveOrder", (newOrder) => {
            console.log("Отримано нове замовлення через SignalR:", newOrder);
            setOrders(prev => [newOrder, ...prev]);
            audio.play().catch(err => console.log("Звук заблоковано:", err));
        });

        connection.start().catch(err => console.error("Помилка SignalR:", err));

        return () => { connection.stop(); };
    }, [audio]);
    // -------------------

    const fetchOrders = async () => {
        try {
            const res = await axios.get('http://192.168.0.65:5219/api/Order');
            setOrders(res.data);
        } catch (err) {
            console.error("Помилка:", err);
        }
    };

    const updateStatus = async (orderId: string, newStatus: string) => {
        const statusMap: Record<string, number> = { 'Pending': 0, 'Paid': 1, 'Cancelled': 2 };
        try {
            await axios.put(`http://192.168.0.65:5219/api/Order/${orderId}/status`,
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

    const displayedOrders = useMemo(() => {
        return orders.filter(o => {
            const s = String(o.status);
            if (activeTab === 'active') {
                return s === 'Pending' || s === '0';
            }
            return s === 'Paid' || s === 'Cancelled' || s === '1' || s === '2';
        });
    }, [orders, activeTab]);

    return (
        <div className="bg-[#1a2521] min-h-screen p-8 text-white font-serif">
            <h2 className="text-3xl font-bold mb-8">Замовлення</h2>

            <div className="flex gap-4 mb-8">
                <button
                    onClick={() => setActiveTab('active')}
                    className={`px-6 py-2 rounded-lg border ${activeTab === 'active' ? 'bg-[#d4af37] text-[#1a2521]' : 'border-[#d4af37]'}`}
                >
                    Активні
                </button>
                <button
                    onClick={() => setActiveTab('history')}
                    className={`px-6 py-2 rounded-lg border ${activeTab === 'history' ? 'bg-[#d4af37] text-[#1a2521]' : 'border-[#d4af37]'}`}
                >
                    Історія
                </button>
            </div>

            <div className="space-y-4">
                {displayedOrders.length === 0 ? (
                    <p className="text-gray-400">Немає замовлень у цій категорії.</p>
                ) : (
                    displayedOrders.map(o => (
                        <div key={o.id} className="bg-[#2a3833] p-6 rounded-2xl border border-[#3b4d47]">
                            <div className="flex justify-between items-center mb-4">
                                <h3 className="text-xl font-bold">{o.locationName || "Альтанка"}</h3>
                                <span className="bg-[#1a2521] border border-[#d4af37] text-[#d4af37] px-3 py-1 rounded-lg text-sm">
                                    {o.status === 1 || o.status === 'Paid' ? 'Оплачено' : o.status === 2 || o.status === 'Cancelled' ? 'Скасовано' : 'В очікуванні'}
                                </span>
                            </div>

                            <ul className="mb-6 space-y-1">
                                {o.orderItems?.map((item: any, i: number) => (
                                    <li key={i} className="flex justify-between text-gray-300">
                                        <span>{item.productName}</span>
                                        <span>{item.quantity} шт.</span>
                                    </li>
                                ))}
                            </ul>

                            <div className="flex gap-2">
                                {['Pending', 'Paid', 'Cancelled'].map((status) => (
                                    <button
                                        key={status}
                                        onClick={() => updateStatus(o.id, status)}
                                        className={`flex-1 py-2 rounded-lg border transition-all ${
                                            (o.status === status)
                                                ? 'bg-[#d4af37] text-[#1a2521] border-[#d4af37]'
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