import React, { useState, useEffect } from 'react';
import './LightButton.css';
import FetchRequest from "../../api/api";

const LightButton = () => {
  const storedLightState = localStorage.getItem('isLightOn');
  const [isOn, setIsOn] = useState(storedLightState ? JSON.parse(storedLightState) : false);
  const [isHandling, setIsHandling] = useState(false);
  const username = localStorage.getItem('username');

  useEffect(() => {
    localStorage.setItem('isLightOn', JSON.stringify(isOn));
  }, [isOn]);

  useEffect(() => {
    fetchData();
    const intervalId = setInterval(fetchData, 5000); // Fetch data every 5 seconds

    return () => {
      clearInterval(intervalId); // Clear interval on component unmount
    };
  }, []);

  const toggle = () => {
    if (!isHandling) {
      setIsHandling(true);
      handleSubmit();
    }
  };

  const handleSubmit = () => {
    FetchRequest('api/ManualControlApi/Control', 'POST', {
      UserName : username,
      Kind : 'Light',
      Command : isOn ? 'Off' : 'On' // Toggle the command
    }, successCallback, errorCallback);
  };

  const fetchData = () => {
    setIsHandling(true);
    FetchRequest('api/IotDeviceApi/GetLatestSensorData', 'GET', {
      DeviceType: 'Light'
    }, successCallback, errorCallback);
  };

  const successCallback = (data) => {
    console.log('Success:', data);
    setIsOn(data.Response === '1');
    setIsHandling(false);
  }
  
  const errorCallback = (error) => {
    console.error('Error:', error);
    setIsHandling(false);
  }

  // data display
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