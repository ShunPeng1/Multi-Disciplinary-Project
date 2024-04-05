import React, { useState } from 'react';
import './DoorButton.css'; 

const DoorButton = () => {
  const [isOn, setIsOn] = useState(false);

  const toggle = () => {
    setIsOn(!isOn);
  };  
  
  //send data
  
  return (
    <div class = "button-container">
      <div className={`toggle-button ${isOn ? 'on' : ''}`} onClick={toggle}>
        <div className="toggle-circle"></div>
      </div>
      <div className = "stateDoor"> 
        {isOn ? 'Openning' : 'Closing'}
      </div>
    </div>
  );
};

export default DoorButton;