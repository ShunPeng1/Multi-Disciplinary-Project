import React, { useState, useEffect } from 'react';
import './LightLivingRoomButton.css';
import FetchRequest from "../../api/api";

const LightLivingRoomButton = () => {
  const storedLightLivingRoomState = localStorage.getItem('isLightLivingRoomOn');
  const [isOn, setIsOn] = useState(storedLightLivingRoomState ? JSON.parse(storedLightLivingRoomState) : false);
  const [isHandling, setIsHandling] = useState(false);
  const username = localStorage.getItem('username');

  useEffect(() => {
    localStorage.setItem('isLightLivingRoomOn', JSON.stringify(isOn));
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
      Kind : 'LightLivingRoom',
      Command : isOn ? 'Off' : 'On' // Toggle the command
    }, successCallback, errorCallback);
  };

  const fetchData = () => {
    setIsHandling(true);
    FetchRequest('api/IotDeviceApi/GetLatestSensorData', 'GET', {
      DeviceType: 'LightLivingRoom'
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
    <div className="LightLivingRoom-button-container">
      <div className="icon-and-toggle">
        <div className={`icon-LightLivingRoom ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <img src={isOn ? "./Images/LightLivingRoom_on.png" : "./Images/LightLivingRoom_off.png"} alt={isOn ? 'LightLivingRoom On' : 'LightLivingRoom Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''} ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-LightLivingRoom">
        {isHandling ? 'Handling' : (isOn ? 'On' : 'Off')}
      </div>
    </div>
  );
};

export default LightLivingRoomButton;