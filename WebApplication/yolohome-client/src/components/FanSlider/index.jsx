import React, { useState } from 'react';
import "./FanSlider.css"

const FanSpeedSlider = () => {
  const [fanSpeed, setFanSpeed] = useState(10); // Default speed set to 10

  const handleSliderChange = (event) => {
    setFanSpeed(event.target.value);
  };

  const handleInputChange = (event) => {
    const value = parseInt(event.target.value, 10);
    if (!isNaN(value) && value >= 0 && value <= 100) {
      setFanSpeed(value);
    }
  };

  return (
    <div className="sliderContainer">
      <input
        type="range"
        min="0"
        max="100"
        value={fanSpeed}
        onChange={handleSliderChange}
        className="slider"
      />
      <input
        type="number"
        min="0"
        max="100"
        value={fanSpeed}
        onChange={handleInputChange}
        className="numberInput" // Add another class name for styling if needed
      />
      <p>Fan Speed: {fanSpeed}%</p>
    </div>
  );
};

export default FanSpeedSlider;