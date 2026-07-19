import type {Product} from "../../types";

export const CartItem = ({ item }: { item: Product }) => (
    <div style={{ display: 'flex', justifyContent: 'space-between', padding: '10px 0', borderBottom: '1px solid #eee' }}>
        <span>{item.name}</span>
        <strong>{item.price} грн</strong>
    </div>
);