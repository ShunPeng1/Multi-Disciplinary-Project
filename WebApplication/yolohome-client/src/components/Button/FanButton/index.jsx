import React, { useState } from 'react';
import './FanButton.css'; // make sure your CSS file is properly linked

const FanButton = () => {
  const [isOn, setIsOn] = useState(false);

  const toggle = () => {
    setIsOn(!isOn);
  };

  // data handling here

  return (
    <div className="fan-button-container">
      <div className="icon-and-toggle">
        <div className="icon-fan" onClick={toggle}>
          <img src={isOn ? "./Images/fan_on.png" : "./Images/fan_off.png"} alt={isOn ? 'Fan On' : 'Fan Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-fan">
        {isOn ? 'On' : 'Off'}
      </div>
    </div>
  );
};

export default FanButton;
