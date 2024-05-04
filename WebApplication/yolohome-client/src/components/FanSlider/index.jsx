import React, { useState, useEffect } from 'react';
import "./FanSlider.css"

const FanSpeedSlider = ({ fanSpeed, changeFanSpeed, isFanHandling }) => {
  const minSpeed = 0;
  const maxSpeed = 100;
  const [sliderValue, setSliderValue] = useState(fanSpeed);
  const [lastSliderValue, setLastSliderValue] = useState(fanSpeed);
  const [timeoutId, setTimeoutId] = useState(null);

  const handleSliderChange = (event) => {
    const value = event.target.value;
    if (!isNaN(value) && value >= minSpeed && value <= maxSpeed) {
      setSliderValue(value);
    }
  };
  useEffect(() => {
    if (Number(lastSliderValue) !== Number(sliderValue)) {
      clearTimeout(timeoutId);
      const id = setTimeout(() => {
        changeFanSpeed(sliderValue);
        setLastSliderValue(sliderValue);
      }, 1000); // Adjust the debounce delay as needed
      setTimeoutId(id);
    }
  
    if (Number(fanSpeed) !== Number(sliderValue) && Number(fanSpeed) !== Number(lastSliderValue)) {
      setSliderValue(fanSpeed);
      setLastSliderValue(fanSpeed);
      clearTimeout(timeoutId)
    }

    return () => clearTimeout(timeoutId);
  }, [sliderValue, lastSliderValue, fanSpeed, changeFanSpeed]);
  const handleInputChange = (event) => {
    const value = parseInt(event.target.value, 10);
    if (!isNaN(value) && value >= minSpeed && value <= maxSpeed) {
      setLastSliderValue(sliderValue);
      setSliderValue(value);
    }
  };

  return (
    <div className="sliderContainer">
      <input
        type="range"
        min={minSpeed}
        max={maxSpeed}
        value={sliderValue}
        onChange={handleSliderChange}
        className="slider"
      />
      <input
        type="number"
        min={minSpeed}
        max={maxSpeed}
        value={sliderValue}
        onChange={handleInputChange}
        className="numberInput"
      />
      <p>Fan Speed: {sliderValue}%</p>
    </div>
  );
};

export default FanSpeedSlider;