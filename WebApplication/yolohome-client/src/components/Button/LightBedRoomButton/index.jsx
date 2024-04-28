import React, { useState, useEffect } from 'react';
import './LightBedRoomButton.css';
import FetchRequest from "../../api/api";

const LightBedRoomButton = () => {
  const storedLightBedRoomState = localStorage.getItem('isLightBedRoomOn');
  const [isOn, setIsOn] = useState(storedLightBedRoomState ? JSON.parse(storedLightBedRoomState) : false);
  const [isHandling, setIsHandling] = useState(false);
  const username = localStorage.getItem('username');

  useEffect(() => {
    localStorage.setItem('isLightBedRoomOn', JSON.stringify(isOn));
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
      Kind : 'Light2',
      Command : isOn ? 'Off' : 'On' // Toggle the command
    }, successCallback, errorCallback);
  };

  const fetchData = () => {
    setIsHandling(true);
    FetchRequest('api/IotDeviceApi/GetLatestSensorData', 'GET', {
      DeviceType: 'Light2'
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
    <div className="LightBedRoom-button-container">
      <div className="icon-and-toggle">
        <div className={`icon-LightBedRoom ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <img src={isOn ? "./Images/LightBedRoom_on.png" : "./Images/LightBedRoom_off.png"} alt={isOn ? 'LightBedRoom On' : 'LightBedRoom Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''} ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-LightBedRoom">
        {isHandling ? 'Handling' : (isOn ? 'On' : 'Off')}
      </div>
    </div>
  );
};

export default LightBedRoomButton;