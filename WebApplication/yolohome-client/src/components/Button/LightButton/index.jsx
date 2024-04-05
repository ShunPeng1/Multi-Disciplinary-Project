import React, { useState } from 'react';
import './LightButton.css'; // make sure your CSS file is properly linked

const LightButton = () => {
  const [isOn, setIsOn] = useState(false);

  const toggle = () => {
    setIsOn(!isOn);
  };

  return (
    <div className="light-button-container">
      <div className="icon-and-toggle">
        <div className="icon-light" onClick={toggle}>
          <img src={isOn ? "./Images/light_on.png" : "./Images/light_off.png"} alt={isOn ? 'Light On' : 'Light Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-light">
        {isOn ? 'On' : 'Off'}
      </div>
    </div>
  );
};

export default LightButton;
