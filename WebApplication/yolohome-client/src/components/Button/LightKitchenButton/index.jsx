import React, { useState, useEffect } from 'react';
import './LightKitchenButton.css';
import FetchRequest from "../../api/api";

const LightKitchenButton = () => {
  const storedLightKitchenState = localStorage.getItem('isLightKitchenOn');
  const [isOn, setIsOn] = useState(storedLightKitchenState ? JSON.parse(storedLightKitchenState) : false);
  const [isHandling, setIsHandling] = useState(false);
  const username = localStorage.getItem('username');

  useEffect(() => {
    localStorage.setItem('isLightKitchenOn', JSON.stringify(isOn));
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
      Kind : 'LightKitchen',
      Command : isOn ? 'Off' : 'On' // Toggle the command
    }, successCallback, errorCallback);
  };

  const fetchData = () => {
    setIsHandling(true);
    FetchRequest('api/IotDeviceApi/GetLatestSensorData', 'GET', {
      DeviceType: 'LightKitchen'
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
    <div className="LightKitchen-button-container">
      <div className="icon-and-toggle">
        <div className={`icon-LightKitchen ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <img src={isOn ? "./Images/LightKitchen_on.png" : "./Images/LightKitchen_off.png"} alt={isOn ? 'LightKitchen On' : 'LightKitchen Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''} ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-LightKitchen">
        {isHandling ? 'Handling' : (isOn ? 'On' : 'Off')}
      </div>
    </div>
  );
};

export default LightKitchenButton;