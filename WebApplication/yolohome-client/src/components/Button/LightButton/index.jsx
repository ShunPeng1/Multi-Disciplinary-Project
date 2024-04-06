import React, { useState, useEffect } from 'react';
import './LightButton.css';
import FetchRequest from "../../api/api";

const LightButton = () => {
  const storedLightState = localStorage.getItem('isOn');
  const [isOn, setIsOn] = useState(storedState ? JSON.parse(storedState) : false);
  const [isHandling, setIsHandling] = useState(false);

  useEffect(() => {
    localStorage.setItem('isOn', JSON.stringify(isOn));
  }, [isOn]);

  const toggle = () => {
    if (!isHandling) {
      setIsHandling(true);
      handleSubmit();
    }
  };

  const handleSubmit = () => {
    FetchRequest('api/ManualControlApi/Control', 'POST', {
      UserName : 'TODO',
      Kind : 'Light',
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
    <div className="light-button-container">
      <div className="icon-and-toggle">
        <div className={`icon-light ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <img src={isOn ? "./Images/light_on.png" : "./Images/light_off.png"} alt={isOn ? 'Light On' : 'Light Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''} ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-light">
        {isHandling ? 'Handling' : (isOn ? 'On' : 'Off')}
      </div>
    </div>
  );
};

export default LightButton;