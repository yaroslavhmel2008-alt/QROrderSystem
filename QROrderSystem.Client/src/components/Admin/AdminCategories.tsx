import { useEffect, useState } from 'react';
import axios from 'axios';
import type {Category} from "../../types";

export const AdminCategories = () => {
    const [categories, setCategories] = useState<Category[]>([]);
    const [name, setName] = useState("");

    useEffect(() => { fetchCategories(); }, []);

    const fetchCategories = async () => {
        const res = await axios.get('http://192.168.0.65:5219/api/Categories');
        setCategories(res.data);
    };

    const createCategory = async () => {
        if (!name) return;
        await axios.post('http://192.168.0.65:5219/api/Categories', { name });
        setName("");
        fetchCategories();
    };

    const deleteCategory = async (id: string) => {
        await axios.delete(`http://192.168.0.65:5219/api/Categories/${id}`);
        fetchCategories();
    };

    return (
        <div className="bg-[#1a2521] min-h-screen p-8 text-white font-serif">
            <h2 className="text-3xl font-bold mb-8">Категорії</h2>

            <div className="bg-[#2a3833] p-6 rounded-2xl border border-[#3b4d47] mb-6 flex gap-4">
                <input
                    value={name}
                    onChange={e => setName(e.target.value)}
                    placeholder="Назва категорії"
                    className="flex-1 bg-[#1a2521] border border-[#3b4d47] p-3 rounded-xl text-white placeholder-gray-500"
                />
                <button
                    onClick={createCategory}
                    className="bg-[#d4af37] text-[#1a2521] px-6 py-3 rounded-xl font-bold hover:bg-[#b89630] transition-colors"
                >
                    + Створити
                </button>
            </div>

            <div className="space-y-3">
                {categories.map(c => (
                    <div key={c.id} className="bg-[#2a3833] p-4 rounded-xl border border-[#3b4d47] flex justify-between items-center">
                        <span className="text-lg">{c.name}</span>
                        <button onClick={() => deleteCategory(c.id)} className="text-red-400 hover:text-red-300">
                            Видалити
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};