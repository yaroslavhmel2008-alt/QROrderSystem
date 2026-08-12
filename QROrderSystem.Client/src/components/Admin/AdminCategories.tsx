import { useEffect, useState } from 'react';
import axios from 'axios';
import type { Category } from "../../types";

const API_URL = import.meta.env.VITE_API_URL;

export const AdminCategories = () => {
    const [categories, setCategories] = useState<Category[]>([]);
    const [name, setName] = useState("");

    useEffect(() => { fetchCategories(); }, []);

    const fetchCategories = async () => {
        try {
            const res = await axios.get(`${API_URL}/api/Categories`);
            setCategories(res.data);
        } catch (err) {
            console.error("Помилка завантаження категорій", err);
        }
    };

    const createCategory = async () => {
        if (!name.trim()) return;
        try {
            await axios.post(`${API_URL}/api/Categories`, { name });
            setName("");
            fetchCategories();
        } catch (err) {
            console.error("Помилка створення категорії", err);
        }
    };

    const deleteCategory = async (id: string) => {
        try {
            await axios.delete(`${API_URL}/api/Categories/${id}`);
            fetchCategories();
        } catch (err) {
            console.error("Помилка видалення категорії", err);
        }
    };

    return (
        <div className="bg-[#1a2521] min-h-screen p-4 sm:p-8 text-white font-serif">
            <h2 className="text-2xl sm:text-3xl font-bold mb-6 sm:mb-8">Категорії</h2>
            
            <div className="bg-[#2a3833] p-4 sm:p-6 rounded-2xl border border-[#3b4d47] mb-6 flex flex-col sm:flex-row gap-4">
                <input
                    value={name}
                    onChange={e => setName(e.target.value)}
                    placeholder="Назва категорії"
                    className="flex-1 bg-[#1a2521] border border-[#3b4d47] p-3 rounded-xl text-white placeholder-gray-500 focus:outline-none focus:border-[#d4af37]"
                />
                <button
                    onClick={createCategory}
                    className="bg-[#d4af37] text-[#1a2521] px-6 py-3 rounded-xl font-bold hover:bg-[#b89630] transition-colors w-full sm:w-auto text-center"
                >
                    + Створити
                </button>
            </div>
            
            <div className="space-y-3">
                {categories.map(c => (
                    <div
                        key={c.id}
                        className="bg-[#2a3833] p-4 rounded-xl border border-[#3b4d47] flex justify-between items-center gap-4"
                    >
                        <span className="text-base sm:text-lg break-all">{c.name}</span>
                        <button
                            onClick={() => deleteCategory(c.id)}
                            className="text-red-400 hover:text-red-300 text-sm sm:text-base px-2 py-1 shrink-0"
                        >
                            Видалити
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};