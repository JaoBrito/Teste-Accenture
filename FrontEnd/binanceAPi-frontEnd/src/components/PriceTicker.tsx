import React, { useContext } from 'react';
import { WebSocketContext } from '../context/WebSocketContext';

export const PriceTicker: React.FC = () => {
  const { tickerData, symbols } = useContext(WebSocketContext);

  return (
    <div className="price-ticker">
      <h2>Preços atualizados</h2>
      {symbols.length === 0 ? (
        <p>Nenhum simbolo adicionado ainda</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Symbol</th>
              <th>Ultimo valor</th>
              <th>Melhor oferta de compra</th>
              <th>Melhor oferta de venda</th>
              <th>Variação de preco %</th>
            </tr>
          </thead>
          <tbody>
            {symbols.map((symbol) => {
              const data = tickerData[symbol];
              return (
                <tr key={symbol}>
                  <td>{symbol}</td>
                  <td>{data?.c || '-'}</td>
                  <td>{data?.b || '-'}</td>
                  <td>{data?.a || '-'}</td>
                  <td>{data?.P || '-'}%</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      )}
    </div>
  );
};