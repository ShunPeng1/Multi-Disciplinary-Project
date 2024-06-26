import React, { useState, useEffect } from 'react';
import './DoorButton.css';
import FetchRequest from "../../api/api"; // make sure your CSS file is properly linked

const DoorButton = () => {
  const storedDoorState = localStorage.getItem('isDoorOn');
  const [isOn, setIsOn] = useState(storedDoorState ? JSON.parse(storedDoorState) : false);
  const [isHandling, setIsHandling] = useState(false);

  const username = localStorage.getItem('username');
  
  useEffect(() => {
    localStorage.setItem('isDoorOn', JSON.stringify(isOn));
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
      sendControlSubmit();
    }
  };

  const sendControlSubmit = () => {
      FetchRequest('api/ManualControlApi/Control', 'POST', {
          UserName : username,
          Kind : 'Door',
          Command : isOn ? 'Close' : 'Open' // Toggle the command
      }, successCallback, errorCallback);
  };

  const fetchData = () => {
    setIsHandling(true);
    FetchRequest('api/IotDeviceApi/GetLatestSensorData', 'GET', {
      DeviceType: 'Door'
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

  // Data display
  return (
    <div className="door-button-container">
      <div className="icon-and-toggle">
        <div className={`icon-door ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <img src={isOn ? "./Images/door_open.png" : "./Images/door_close.png"} alt={isOn ? 'Door On' : 'Door Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''} ${isHandling ? 'pending' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-door">
        {isHandling ? 'Handling' : (isOn ? 'Opening' : 'Closing')}
      </div>
    </div>
  );

};

export default DoorButton;