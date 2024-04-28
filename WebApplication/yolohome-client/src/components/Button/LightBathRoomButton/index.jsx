import React, { useState, useEffect } from 'react';
import './LightBathRoomButton.css';
import FetchRequest from "../../api/api";

const LightBathRoomButton = () => {
  const storedLightBathRoomState = localStorage.getItem('isLightBathRoomOn');
  const [isOn, setIsOn] = useState(storedLightBathRoomState ? JSON.parse(storedLightBathRoomState) : false);
  const [isHandling, setIsHandling] = useState(false);
  const username = localStorage.getItem('username');

  useEffect(() => {
    localStorage.setItem('isLightBathRoomOn', JSON.stringify(isOn));
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
      Kind : 'Light4',
      Command : isOn ? 'Off' : 'On' // Toggle the command
    }, successCallback, errorCallback);
  };

  const fetchData = () => {
    setIsHandling(true);
    FetchRequest('api/IotDeviceApi/GetLatestSensorData', 'GET', {
      DeviceType: 'Light4'
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
    <div className="LightBathRoom-button-container">
      <div className="icon-and-toggle">
        <div className={`icon-LightBathRoom ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <img src={isOn ? "./Images/LightBathRoom_on.png" : "./Images/LightBathRoom_off.png"} alt={isOn ? 'LightBathRoom On' : 'LightBathRoom Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''} ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-LightBathRoom">
        {isHandling ? 'Handling' : (isOn ? 'On' : 'Off')}
      </div>
    </div>
  );
};

export default LightBathRoomButton;