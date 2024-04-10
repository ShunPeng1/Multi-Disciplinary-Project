import React, { useState } from 'react';
import "./FanSlider.css"

const FanSpeedSlider = () => {
  const [fanSpeed, setFanSpeed] = useState(10); // Default speed set to 50

  const handleSliderChange = (event) => {
    setFanSpeed(event.target.value);
  };

  return (
    <div className="sliderContainer">
      <input
        type="range"
        min="0"
        max="100"
        value={fanSpeed}
        onChange={handleSliderChange}
        className="slider" // Add the class name for styling
      />
      <p>Fan Speed: {fanSpeed}%</p>
    </div>
  );
};

export default FanSpeedSlider;