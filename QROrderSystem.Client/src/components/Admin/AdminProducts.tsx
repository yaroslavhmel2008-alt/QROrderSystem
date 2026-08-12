import { useEffect, useState } from 'react';
import axios from 'axios';
import type { Product, Category } from "../../types";

const API_URL = import.meta.env.VITE_API_URL;

export const AdminProducts = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);
    
    const [name, setName] = useState("");
    const [price, setPrice] = useState<number | "">("");
    const [categoryId, setCategoryId] = useState("");
    const [imageFile, setImageFile] = useState<File | null>(null);
    
    const [isSubmitting, setIsSubmitting] = useState(false);
    
    const [fileInputKey, setFileInputKey] = useState<number>(Date.now());
    
    const [searchQuery, setSearchQuery] = useState("");
    
    const [editingId, setEditingId] = useState<string | null>(null);
    const [editName, setEditName] = useState("");
    const [editPrice, setEditPrice] = useState<number | "">("");
    const [editCategoryId, setEditCategoryId] = useState("");
    const [editImageFile, setEditImageFile] = useState<File | null>(null);

    useEffect(() => {
        fetchProducts();
        fetchCategories();
    }, []);

    const fetchProducts = async () => {
        try {
            const res = await axios.get(`${API_URL}/api/Product`);
            setProducts(res.data);
        } catch (err) {
            console.error("Помилка завантаження товарів:", err);
        }
    };

    const fetchCategories = async () => {
        try {
            const res = await axios.get(`${API_URL}/api/Categories`);
            setCategories(res.data);
        } catch (err) {
            console.error("Помилка завантаження категорій:", err);
        }
    };

    const createProduct = async () => {
        if (!name.trim() || price === "" || price <= 0 || !categoryId) {
            alert("Будь ласка, заповніть всі поля коректно");
            return;
        }

        setIsSubmitting(true);
        const formData = new FormData();
        formData.append("name", name);
        formData.append("price", price.toString());
        formData.append("categoryId", categoryId);
        if (imageFile) {
            formData.append("imageFile", imageFile);
        }

        try {
            await axios.post(`${API_URL}/api/Product`, formData, {
                headers: { "Content-Type": "multipart/form-data" }
            });
            setName("");
            setPrice("");
            setCategoryId("");
            setImageFile(null);
            setFileInputKey(Date.now()); // Очищає стан файлового інпуту в інтерфейсі
            fetchProducts();
        } catch (err) {
            console.error("Помилка створення товару:", err);
        } finally {
            setIsSubmitting(false);
        }
    };

    const deleteProduct = async (id: string) => {
        if (!window.confirm("Ви дійсно хочете видалити цей товар?")) return;

        try {
            await axios.delete(`${API_URL}/api/Product/${id}`);
            fetchProducts();
        } catch (err) {
            console.error("Помилка видалення товару:", err);
            alert("Не вдалося видалити товар.");
        }
    };

    const startEditing = (p: Product) => {
        setEditingId(p.id);
        setEditName(p.name);
        setEditPrice(p.price);
        setEditCategoryId(p.categoryId);
        setEditImageFile(null);
    };

    const cancelEditing = () => {
        setEditingId(null);
        setEditName("");
        setEditPrice("");
        setEditCategoryId("");
        setEditImageFile(null);
    };

    const saveEdit = async (id: string) => {
        if (!editName.trim() || editPrice === "" || editPrice <= 0 || !editCategoryId) {
            alert("Заповніть поля правильно для збереження");
            return;
        }

        const formData = new FormData();
        formData.append("id", id);
        formData.append("name", editName);
        formData.append("price", editPrice.toString());
        formData.append("categoryId", editCategoryId);
        if (editImageFile) {
            formData.append("imageFile", editImageFile);
        }

        try {
            await axios.put(`${API_URL}/api/Product/${id}`, formData, {
                headers: { "Content-Type": "multipart/form-data" }
            });
            setEditingId(null);
            setEditImageFile(null);
            fetchProducts();
        } catch (err: any) {
            console.error("Помилка оновлення товару:", err);
            const errorMsg = err.response?.data || "Не вдалося оновити товар.";
            alert("Помилка: " + (typeof errorMsg === 'string' ? errorMsg : JSON.stringify(errorMsg)));
        }
    };

    const filteredProducts = products.filter(p =>
        p.name.toLowerCase().includes(searchQuery.toLowerCase())
    );

    return (
        <div className="bg-[#1a2521] min-h-screen p-2 sm:p-8 text-white font-serif w-full max-w-full overflow-hidden">
            <h2 className="text-2xl sm:text-3xl font-bold mb-6 sm:mb-8">Товари</h2>

            {/* Форма додавання */}
            <div className="bg-[#2a3833] p-3 sm:p-6 rounded-2xl border border-[#3b4d47] mb-6 space-y-4 shadow-lg w-full">
                <h3 className="text-lg font-bold text-[#d4af37]">Додати новий товар</h3>
                <input
                    className="w-full bg-[#1a2521] border border-[#3b4d47] p-3 rounded-xl text-white placeholder-gray-500 focus:outline-none focus:border-[#d4af37] text-sm sm:text-base"
                    placeholder="Назва товару"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    disabled={isSubmitting}
                />

                <div className="grid grid-cols-1 sm:grid-cols-2 gap-3 w-full">
                    <input
                        className="w-full bg-[#1a2521] border border-[#3b4d47] p-3 rounded-xl text-white placeholder-gray-500 focus:outline-none focus:border-[#d4af37] text-sm sm:text-base"
                        type="number"
                        placeholder="Ціна (грн)"
                        value={price === "" ? "" : price}
                        onChange={(e) => setPrice(e.target.value === "" ? "" : Number(e.target.value))}
                        disabled={isSubmitting}
                    />
                    <select
                        className="w-full bg-[#1a2521] border border-[#3b4d47] p-3 rounded-xl text-white focus:outline-none focus:border-[#d4af37] text-sm sm:text-base"
                        value={categoryId}
                        onChange={(e) => setCategoryId(e.target.value)}
                        disabled={isSubmitting}
                    >
                        <option value="">Виберіть категорію</option>
                        {categories.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                    </select>
                </div>

                {/* Вибір файлу картинки з унікальним key для скидання */}
                <div className="space-y-1 w-full">
                    <label className="text-xs text-gray-400">Зображення з комп'ютера:</label>
                    <input
                        key={fileInputKey}
                        type="file"
                        accept="image/*"
                        onChange={(e) => {
                            if (e.target.files && e.target.files[0]) {
                                setImageFile(e.target.files[0]);
                            }
                        }}
                        disabled={isSubmitting}
                        className="w-full bg-[#1a2521] border border-[#3b4d47] p-2 rounded-xl text-white text-xs file:mr-2 file:py-1 file:px-2 file:rounded-lg file:border-0 file:text-xs file:font-semibold file:bg-[#d4af37] file:text-[#1a2521]"
                    />
                </div>

                <button
                    onClick={createProduct}
                    disabled={isSubmitting}
                    className="w-full bg-[#d4af37] text-[#1a2521] py-3 rounded-xl font-bold hover:bg-[#b89630] transition-colors text-sm sm:text-base flex items-center justify-center gap-2 disabled:opacity-50"
                >
                    {isSubmitting ? (
                        <>
                            {/* Крутілка (спінер) */}
                            <svg className="animate-spin h-5 w-5 text-[#1a2521]" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                                <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                                <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                            </svg>
                            Створення...
                        </>
                    ) : (
                        "+ Додати товар"
                    )}
                </button>
            </div>

            {/* Поле пошуку */}
            <div className="mb-6 w-full">
                <input
                    type="text"
                    placeholder="🔍 Пошук товару..."
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                    className="w-full bg-[#2a3833] border border-[#3b4d47] p-3.5 rounded-xl text-white placeholder-gray-400 focus:outline-none focus:border-[#d4af37] text-sm sm:text-base"
                />
            </div>

            {/* Список товарів */}
            <div className="space-y-3 w-full">
                {filteredProducts.length === 0 ? (
                    <p className="text-gray-400 text-center py-8">Товарів не знайдено.</p>
                ) : (
                    filteredProducts.map(p => {
                        const isEditing = editingId === p.id;

                        return (
                            <div key={p.id} className="bg-[#2a3833] p-3 sm:p-5 rounded-2xl border border-[#3b4d47] w-full overflow-hidden">
                                {isEditing ? (
                                    /* Режим редагування */
                                    <div className="w-full space-y-3">
                                        <input
                                            type="text"
                                            value={editName}
                                            onChange={(e) => setEditName(e.target.value)}
                                            className="w-full bg-[#1a2521] border border-[#3b4d47] p-2.5 rounded-xl text-white text-sm"
                                            placeholder="Назва"
                                        />
                                        <div className="grid grid-cols-1 sm:grid-cols-2 gap-2">
                                            <input
                                                type="number"
                                                value={editPrice}
                                                onChange={(e) => setEditPrice(e.target.value === "" ? "" : Number(e.target.value))}
                                                className="w-full bg-[#1a2521] border border-[#3b4d47] p-2.5 rounded-xl text-white text-sm"
                                                placeholder="Ціна"
                                            />
                                            <select
                                                value={editCategoryId}
                                                onChange={(e) => setEditCategoryId(e.target.value)}
                                                className="w-full bg-[#1a2521] border border-[#3b4d47] p-2.5 rounded-xl text-white text-sm"
                                            >
                                                {categories.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                                            </select>
                                        </div>
                                        <div className="space-y-1">
                                            <label className="text-xs text-gray-400">Змінити зображення:</label>
                                            <input
                                                type="file"
                                                accept="image/*"
                                                onChange={(e) => {
                                                    if (e.target.files && e.target.files[0]) {
                                                        setEditImageFile(e.target.files[0]);
                                                    }
                                                }}
                                                className="w-full bg-[#1a2521] border border-[#3b4d47] p-2 rounded-xl text-white text-xs file:mr-2 file:py-1 file:px-2 file:rounded-lg file:border-0 file:text-xs file:font-semibold file:bg-[#d4af37] file:text-[#1a2521]"
                                            />
                                        </div>
                                        <div className="flex gap-2 justify-end pt-1">
                                            <button
                                                onClick={() => saveEdit(p.id)}
                                                className="bg-green-600 hover:bg-green-500 text-white px-4 py-2 rounded-xl text-xs sm:text-sm font-bold"
                                            >
                                                Зберегти
                                            </button>
                                            <button
                                                onClick={cancelEditing}
                                                className="bg-gray-600 hover:bg-gray-500 text-white px-4 py-2 rounded-xl text-xs sm:text-sm font-bold"
                                            >
                                                Скасувати
                                            </button>
                                        </div>
                                    </div>
                                ) : (
                                    /* Звичайний перегляд */
                                    <div className="flex flex-col gap-3 w-full">
                                        <div className="flex items-start gap-3 w-full">
                                            {p.imageUrl && (
                                                <img
                                                    src={`${API_URL}${p.imageUrl}`}
                                                    alt={p.name}
                                                    className="w-14 h-14 sm:w-16 sm:h-16 object-cover rounded-xl border border-[#3b4d47] flex-shrink-0"
                                                />
                                            )}
                                            <div className="flex-1 min-w-0">
                                                <p className="font-bold text-base sm:text-lg break-words">{p.name}</p>
                                                <p className="text-[#d4af37] font-semibold text-sm sm:text-base">{p.price} грн</p>
                                                <span className="text-[11px] bg-[#1a2521] px-2.5 py-0.5 rounded-full border border-[#3b4d47] text-gray-300 inline-block mt-1">
                                                    {categories.find(c => c.id === p.categoryId)?.name || "Без категорії"}
                                                </span>
                                            </div>
                                        </div>

                                        <div className="flex items-center gap-2 w-full pt-2 border-t border-[#3b4d47] justify-end">
                                            <button
                                                onClick={() => startEditing(p)}
                                                className="flex-1 sm:flex-none text-xs sm:text-sm bg-[#1a2521] border border-[#d4af37] text-[#d4af37] px-4 py-2 rounded-xl hover:bg-[#d4af37]/10 transition-all text-center font-medium"
                                            >
                                                Редагувати
                                            </button>
                                            <button
                                                onClick={() => deleteProduct(p.id)}
                                                className="flex-1 sm:flex-none text-xs sm:text-sm bg-red-950/40 border border-red-500 text-red-400 px-4 py-2 rounded-xl hover:bg-red-500/20 transition-all text-center font-medium"
                                            >
                                                Видалити
                                            </button>
                                        </div>
                                    </div>
                                )}
                            </div>
                        );
                    })
                )}
            </div>
        </div>
    );
};