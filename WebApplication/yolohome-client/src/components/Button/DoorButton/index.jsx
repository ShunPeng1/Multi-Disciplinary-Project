import React, { useState } from 'react';
import './DoorButton.css';
import FetchRequest from "../../api/api"; // make sure your CSS file is properly linked

const DoorButton = () => {
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
          Kind : 'Door',
          Command : isOn ? 'Open' : 'Close'
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


    // data handling here

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
