// src/components/SymbolList.tsx
import React, { useContext, useState } from 'react';
import { WebSocketContext } from '../context/WebSocketContext';
import './SymbolList.css';

export const SymbolList: React.FC = () => {
  const { symbols, addSymbol, removeSymbol } = useContext(WebSocketContext);
  const [input, setInput] = useState('');
  const [error, setError] = useState<string | null>(null);

  const handleAdd = () => {
    if (input.trim()) {
      const result = addSymbol(input.trim());
      if (result.success) {
        setInput('');
        setError(null);
      } else {
        setError(result.message || 'Erro ao adicionar o símbolo.');
        // Limpa o erro após 3 segundos para melhor UX
        setTimeout(() => setError(null), 3000);
      }
    }
  };

  return (
    <div className="symbol-list">
      <h2>Lista</h2>
      <div className="input-container">
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          placeholder="ex: ETHBTC"
        />
        <button onClick={handleAdd}>Adicionar</button>
      </div>
      {error && (
        <div className="error-message" style={{ color: 'red', marginTop: '10px' }}>
          {error}
        </div>
      )}
      <ul>
        {symbols.map((symbol) => (
          <li key={symbol}>
            {symbol} <button onClick={() => removeSymbol(symbol)}>Remover item</button>
          </li>
        ))}
      </ul>
    </div>
  );
};