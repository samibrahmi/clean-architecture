import React from 'react';
import './Card.css';

const Card = ({ title, children }) => {
  return (
    <div className="card">
      <div className="card-content">
        {title && <h2 className="card-title">{title}</h2>}
        <div className="card-body">
          {children}
        </div>
      </div>
    </div>
  );
};

export default Card;
