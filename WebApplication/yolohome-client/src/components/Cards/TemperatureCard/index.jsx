import React from "react";
import './TemperatureCard.css';

const TemperatureCard = () => {

    // receive data from sensor here

    return (
        <div className="temperature-container">
            <div className="icon-and-temp">
                <img src="./Images/temperature.png" alt="Temperature Icon" />
                <div className="temperature-info">
                    {'30°C'}
                </div>
            </div>
            <div className="temperature-text">
                {'Temperature'}
            </div>
        </div>
    );
}

export default TemperatureCard;