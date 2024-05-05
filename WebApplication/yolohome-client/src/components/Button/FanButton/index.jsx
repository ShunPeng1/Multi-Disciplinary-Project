import React, { useState, useEffect } from 'react';
import './FanButton.css';
import FetchRequest from "../../api/api";

const FanButton =  ({fanSpeed, changeFanSpeed, isFanHandling }) => {
  const storedFanState = localStorage.getItem('isFanOn');
  const [isOn, setIsOn] = useState(storedFanState ? JSON.parse(storedFanState) : false);
  
  useEffect(() => {
    setIsOn(fanSpeed > 0);
  }, [fanSpeed]);

  const toggle = () => {
    if (!isFanHandling) {
      changeFanSpeed(isOn ? 0 : 100); // Toggle the fan speed
    }
  };

  // Data display
  return (
    <div className="fan-button-container">
      <div className="icon-and-toggle">
        <div className={`icon-fan ${isFanHandling ? 'pending' : ''}`} onClick={toggle}>
          <img src={isOn ? "./Images/fan_on.png" : "./Images/fan_off.png"} alt={isOn ? 'Fan On' : 'Fan Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''} ${isFanHandling ? 'pending' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-fan">
        {isFanHandling ? 'Handling' : (isOn ? 'On' : 'Off')}
      </div>
    </div>
  );
};

export default FanButton;