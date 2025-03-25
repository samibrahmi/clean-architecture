import React from 'react';
import './App.css';
import Card from './components/Card';
import logoMelccfp from './assets/logo.jpg';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logoMelccfp} alt="Logo MELCCFP" className="logo" />
      </header>
      <main className="App-main">
        <Card title="Template de départ">
          <p className="welcome-text">Ceci est un template de départ pour les applications du MELCCFP</p>
          <div className="card-footer">
            <div className="card-info">
              <span className="card-info-label">Technologie:</span> React + Vite
            </div>
            <div className="card-info">
              <span className="card-info-label">Architecture:</span> Clean Architecture
            </div>
            <div className="card-info">
              <span className="card-info-label">Usage:</span> Utilisez ce modèle pour démarrer votre projet
            </div>
          </div>
        </Card>
      </main>
    </div>
  );
}

export default App;
