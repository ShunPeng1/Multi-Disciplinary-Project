import React, { useState } from 'react';
import './FanButton.css';
import FetchRequest from "../../api/api";

const FanButton = () => {
  const [isOn, setIsOn] = useState(false);
  const [isHandling, setIsHandling] = useState(false);

  const toggle = () => {
    if (!isHandling) {
      setIsHandling(true);
      handleSubmit();
    }
  };

  const handleSubmit = () => {
    FetchRequest('api/ManualControlApi/Control', 'POST', {
      UserName : 'TODO',
      Kind : 'Fan',
      Command : isOn ? 'On' : 'Off'
    }, successCallback, errorCallback);
  };

  const successCallback = (data) => {
    console.log('Success:', data);
    setIsOn(!isOn);
    setIsHandling(false);
  }

  const errorCallback = (error) => {
    console.error('Error:', error);
    setIsHandling(false);
  }

  return (
    <div className="fan-button-container">
      <div className="icon-and-toggle">
        <div className={`icon-fan ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <img src={isOn ? "./Images/fan_on.png" : "./Images/fan_off.png"} alt={isOn ? 'Fan On' : 'Fan Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''} ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-fan">
        {isHandling ? 'Handling' : (isOn ? 'On' : 'Off')}
      </div>
    </div>
  );
};

export default FanButton;