import { useState } from 'react';
import { AdminProducts } from '../components/Admin/AdminProducts';
import { AdminCategories } from '../components/Admin/AdminCategories';
import { AdminOrders } from '../components/Admin/AdminOrders';
import {AdminLocations} from "../components/Admin/AdminLocations.tsx";


export function AdminPage() {
    const [activeTab, setActiveTab] = useState<'products' | 'categories' | 'orders' | 'locations'>('products');

    return (
        <div className="p-8">
            <h1 className="text-3xl font-bold mb-6">Адмін-панель</h1>
            
            <div className="flex gap-4 mb-8">
                {(['products', 'categories', 'orders', 'locations'] as const).map(tab => (
                    <button
                        key={tab}
                        onClick={() => setActiveTab(tab)}
                        className={`px-4 py-2 rounded font-semibold transition-colors ${
                            activeTab === tab
                                ? 'bg-blue-600 text-white'
                                : 'bg-gray-200 text-gray-700 hover:bg-gray-300'
                        }`}
                    >
                        {tab === 'products' ? 'Товари' :
                            tab === 'categories' ? 'Категорії' :
                                tab === 'orders' ? 'Замовлення' : 'Альтанки'}
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
                <AdminOrders />
            </div>
            <div className={activeTab === 'locations' ? 'block' : 'hidden'}>
                <AdminLocations />
            </div>
        </div>
    );
}