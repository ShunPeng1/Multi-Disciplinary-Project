import React, { useState } from 'react';
import './ToggleButton.css'; // Make sure to create a CSS file with the styles provided above

const ToggleButton = () => {
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
      <div class = "state">
        {isOn ? 'On' : 'Off'}
      </div>
    </div>
  );
};

export default ToggleButton;