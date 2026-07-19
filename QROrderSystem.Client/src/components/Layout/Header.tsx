import { useContext } from 'react';
import { Link } from 'react-router-dom';
import { ShoppingCart } from 'lucide-react';
import { CartContext } from '../../context/CartContext';

interface HeaderProps {
    title: string;
    locationName?: string;
}

export const Header = ({ title, locationName }: HeaderProps) => {
    const { cart } = useContext(CartContext);

    return (
        <div className="relative bg-[#223729] pt-8 pb-12 px-6 shadow-[0_10px_15px_-3px_rgba(0,0,0,0.3)]">
            <Link to="/cart" className="absolute top-8 right-6 z-20 flex items-center p-2 bg-[#1a2521]/40 rounded-full border border-[#d4af37]/20">
                <ShoppingCart className="w-6 h-6 text-[#d4af37]" />
                {cart.length > 0 && (
                    <span className="absolute -top-1 -right-1 bg-[#d4af37] text-[#1a2521] text-[10px] font-bold w-5 h-5 flex items-center justify-center rounded-full shadow-lg">
                        {cart.reduce((sum: number, item: any) => sum + item.quantity, 0)}
                    </span>
                )}
            </Link>

            <div className="absolute top-0 right-0 w-40 h-40 bg-[#d4af37] opacity-[0.07] blur-3xl rounded-full"></div>

            <div className="relative z-10">
                <p className="text-[10px] uppercase tracking-[0.2em] text-[#d4af37]">Відпочинок у Смусика</p>
                <h1 className="text-4xl font-bold text-white mb-4">{title}</h1>
                {locationName && (
                    <div className="inline-flex items-center gap-2 bg-[#1a2521]/40 border border-[#d4af37]/20 px-4 py-2 rounded-full">
                        <span className="text-[#d4af37]">🏠</span>
                        <span className="text-sm">Альтанка: <span className="font-bold">№{locationName}</span></span>
                    </div>
                )}
            </div>

            <div className="absolute bottom-0 left-0 w-full overflow-hidden leading-none">
                <svg className="relative block w-full h-8" viewBox="0 0 1200 120" preserveAspectRatio="none">
                    <path d="M0,0V46.29c47.79,22.2,103.59,32.17,158,28,61.79-4.8,116.36-21.29,179-28,78.23-8.31,162.77-9.52,241,12,65.86,18.39,127.18,48.8,193,48.8,79.52,0,157.57-25.77,236-40.84,65.71-12.67,133.25-18.7,201-18.7h0V0Z" fill="#1a2521"></path>
                </svg>
            </div>
        </div>
    );
};