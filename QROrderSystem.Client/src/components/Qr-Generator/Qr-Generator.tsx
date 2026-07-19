import React from 'react';
import QRCode from "react-qr-code";

interface Props {
    locationId: string; 
    locationName: string; 
}

export const QRCodeGenerator: React.FC<Props> = ({ locationId, locationName }) => {
    const menuUrlWithLocation = `http://192.168.0.65:5173/?locationId=${locationId}`;

    return (
        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', padding: '20px', border: '1px solid #ccc', borderRadius: '10px', background: 'white' }}>
            <h3 style={{ marginBottom: '10px' }}>QR-код для: {locationName}</h3>
            
            <div style={{ background: 'white', padding: '16px' }}>
                <QRCode
                    value={menuUrlWithLocation}
                    size={256} 
                    style={{ height: "auto", maxWidth: "100%", width: "100%" }}
                />
            </div>

            <p style={{ marginTop: '10px', color: '#666' }}>
                Посилання: <code style={{ fontSize: '0.8em', background: '#f0f0f0', padding: '2px 4px' }}>{menuUrlWithLocation}</code>
            </p>
        </div>
    );
};