// src/context/WebSocketContext.tsx
import React, { createContext, useState, useEffect } from 'react';
import axios from 'axios';

interface TickerData {
  s: string;  // Symbol
  c: string;  // Last price
  b: string;  // Best bid price
  a: string;  // Best ask price
  P: string;  // Price change percent
}

interface AddSymbolResult {
  success: boolean;
  message?: string; // Mensagem de erro, se aplicável
}

interface WebSocketContextType {
  symbols: string[];
  tickerData: { [symbol: string]: TickerData };
  addSymbol: (symbol: string) => AddSymbolResult;
  removeSymbol: (symbol: string) => void;
}

export const WebSocketContext = createContext<WebSocketContextType>({
  symbols: [],
  tickerData: {},
  addSymbol: () => ({ success: false }),
  removeSymbol: () => {},
});

export const WebSocketProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [symbols, setSymbols] = useState<string[]>([]);
  const [tickerData, setTickerData] = useState<{ [symbol: string]: TickerData }>({});
  const [ws, setWs] = useState<WebSocket | null>(null);
  const [validSymbols, setValidSymbols] = useState<Set<string>>(new Set());

  useEffect(() => {
    axios
      .get('https://api.binance.com/api/v3/exchangeInfo')
      .then((response) => {
        const symbolsSet: Set<string> = new Set(
          response.data.symbols
            .filter((s: any) => s.status === 'TRADING')
            .map((s: any) => s.symbol.toUpperCase())
        );
        setValidSymbols(symbolsSet);
      })
      .catch((error) => {
        console.error('Erro ao carregar exchangeInfo:', error);
      });
  }, []);

  useEffect(() => {
    if (symbols.length === 0) return;

    const streams = symbols.map((s) => `${s.toLowerCase()}@ticker`).join('/');
    const wsUrl = `wss://data-stream.binance.com/stream?streams=${streams}`;
    const websocket = new WebSocket(wsUrl);

    websocket.onmessage = (event) => {
      const data = JSON.parse(event.data).data;
      if (data && data.e === '24hrTicker') {
        setTickerData((prev) => ({
          ...prev,
          [data.s]: {
            s: data.s,
            c: data.c,
            b: data.b,
            a: data.a,
            P: data.P,
          },
        }));
      }
    };

    websocket.onopen = () => console.log('WebSocket connected');
    websocket.onclose = () => console.log('WebSocket disconnected');
    setWs(websocket);

    return () => websocket.close();
  }, [symbols]);

  const addSymbol = (symbol: string): AddSymbolResult => {
    const upperSymbol = symbol.toUpperCase();
    if (!validSymbols.has(upperSymbol)) {
      return { success: false, message: `O símbolo "${upperSymbol}" não existe ou não está disponível para negociação.` };
    }
    if (symbols.includes(upperSymbol)) {
      return { success: false, message: `O símbolo "${upperSymbol}" já foi adicionado.` };
    }
    setSymbols((prev) => [...prev, upperSymbol]);
    return { success: true };
  };

  const removeSymbol = (symbol: string) => {
    setSymbols((prev) => prev.filter((s) => s !== symbol));
  };

  return (
    <WebSocketContext.Provider value={{ symbols, tickerData, addSymbol, removeSymbol }}>
      {children}
    </WebSocketContext.Provider>
  );
};