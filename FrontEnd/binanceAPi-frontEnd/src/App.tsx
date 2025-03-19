import React from 'react';
import { WebSocketProvider } from './context/WebSocketContext';
import { SymbolList } from './components/SymbolList';
import { PriceTicker } from './components/PriceTicker';
import './App.css';

const App: React.FC = () => {
  return (
    <WebSocketProvider>
      <div className="app">
        <SymbolList />
        <PriceTicker />
      </div>
    </WebSocketProvider>
  );
};

export default App;