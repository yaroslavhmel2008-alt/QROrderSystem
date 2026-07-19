import { useEffect, useState } from 'react';
import axios from 'axios';
import type { Product, Category } from "../../types";

export const AdminProducts = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);
    const [name, setName] = useState("");
    const [price, setPrice] = useState(0);
    const [categoryId, setCategoryId] = useState("");

    useEffect(() => {
        fetchProducts();
        fetchCategories();
    }, []);

    const fetchProducts = async () => {
        const res = await axios.get('http://192.168.0.65:5219/api/Product');
        setProducts(res.data);
    };

    const fetchCategories = async () => {
        const res = await axios.get('http://192.168.0.65:5219/api/Categories');
        setCategories(res.data);
    };

    const createProduct = async () => {
        if (!name || price <= 0 || !categoryId) {
            alert("Будь ласка, заповніть всі поля");
            return;
        }

        await axios.post('http://192.168.0.65:5219/api/Product', { name, price, categoryId });
        
        setName("");
        setPrice(0); 
        setCategoryId("");

        fetchProducts();
    };

    return (
        <div className="bg-[#1a2521] min-h-screen p-8 text-white font-serif">
            <h2 className="text-3xl font-bold mb-8">Товари</h2>

            {/* Форма додавання */}
            <div className="bg-[#2a3833] p-6 rounded-2xl border border-[#3b4d47] mb-8 space-y-4">
                <input className="w-full bg-[#1a2521] border border-[#3b4d47] p-3 rounded-xl" placeholder="Назва товару" value={name} onChange={(e) => setName(e.target.value)} />
                <div className="flex gap-4">
                    <input className="flex-1 bg-[#1a2521] border border-[#3b4d47] p-3 rounded-xl" type="number" placeholder="Ціна" value={price === 0 ? "" : price} onChange={(e) => setPrice(Number(e.target.value))} />
                    <select className="flex-1 bg-[#1a2521] border border-[#3b4d47] p-3 rounded-xl" value={categoryId} onChange={(e) => setCategoryId(e.target.value)}>
                        <option value="">Виберіть категорію</option>
                        {categories.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                    </select>
                </div>
                <button onClick={createProduct} className="w-full bg-[#d4af37] text-[#1a2521] py-3 rounded-xl font-bold hover:bg-[#b89630]">
                    + Додати товар
                </button>
            </div>
            
            <div className="space-y-3">
                {products.map(p => (
                    <div key={p.id} className="bg-[#2a3833] p-4 rounded-xl border border-[#3b4d47] flex justify-between items-center">
                        <div>
                            <p className="font-bold text-lg">{p.name}</p>
                            <p className="text-[#d4af37]">{p.price} грн</p>
                        </div>
                        <span className="text-xs bg-[#1a2521] px-3 py-1 rounded-full border border-[#3b4d47]">
                            {categories.find(c => c.id === p.categoryId)?.name || "Без категорії"}
                        </span>
                    </div>
                ))}
            </div>
        </div>
    );
};