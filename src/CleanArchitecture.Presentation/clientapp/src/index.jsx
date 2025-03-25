import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App.jsx';  // Make sure extension is included
import reportWebVitals from './reportWebVitals';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
reportWebVitals();
