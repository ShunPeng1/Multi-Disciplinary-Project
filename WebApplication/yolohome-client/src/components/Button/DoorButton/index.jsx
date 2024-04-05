import React, { useState } from 'react';
import './DoorButton.css'; // make sure your CSS file is properly linked

const DoorButton = () => {
  const [isOn, setIsOn] = useState(false);

  const toggle = () => {
    setIsOn(!isOn);
  };

  // data handling here

  return (
    <div className="door-button-container">
      <div className="icon-and-toggle">
        <div className="icon-door" onClick={toggle}>
          <img src={isOn ? "./Images/door_open.png" : "./Images/door_close.png"} alt={isOn ? 'Door On' : 'Door Off'} />
        </div>
        <div className={`toggle-button ${isOn ? 'on' : ''}`} onClick={toggle}>
          <div className="toggle-circle"></div>
        </div>
      </div>
      <div className="state-door">
        {isOn ? 'Openning' : 'Closing'}
      </div>
    </div>
  );
};

export default DoorButton;
