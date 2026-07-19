import { useEffect, useState } from 'react';
import axios from 'axios';
import { QRCodeGenerator } from '../Qr-Generator/Qr-Generator';

export const AdminLocations = () => {
    const [locations, setLocations] = useState<{id: string, name: string}[]>([]);

    useEffect(() => {
        axios.get('http://192.168.0.65:5219/api/Location')
            .then(res => setLocations(res.data))
            .catch(err => console.error("Помилка завантаження альтанок:", err));
    }, []);

    return (
        <div className="bg-[#1a2521] min-h-screen p-8 text-white font-serif">
            <h2 className="text-3xl font-bold mb-8">Альтанки та QR-коди</h2>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                {locations.map(loc => (
                    <div key={loc.id} className="bg-[#2a3833] p-6 rounded-2xl border border-[#3b4d47] shadow-lg flex flex-col items-center">
                        <h3 className="text-xl font-bold mb-4 text-[#d4af37]">{loc.name}</h3>

                        {/* Контейнер для QR-коду */}
                        <div className="bg-white p-2 rounded-lg">
                            <QRCodeGenerator
                                locationId={loc.id}
                                locationName={loc.name}
                            />
                        </div>

                        <p className="mt-4 text-xs text-gray-400 uppercase tracking-widest">
                            ID: {loc.id.slice(0, 8)}...
                        </p>
                    </div>
                ))}
            </div>
        </div>
    );
};