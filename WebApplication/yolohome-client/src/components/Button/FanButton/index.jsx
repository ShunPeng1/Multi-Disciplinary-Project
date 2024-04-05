import React, { useState } from "react";
import './FanButton.css';

const FanButton = () => {
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
  
  export default FanButton;