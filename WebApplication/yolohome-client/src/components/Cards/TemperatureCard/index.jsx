import React, { useState, useEffect } from "react";
import './TemperatureCard.css';

const TemperatureCard = () => {
    const [temperature, setTemperature] = useState(null);
    const [showPopup, setShowPopup] = useState(false);

    // Simulate fetching temperature data from the server
    useEffect(() => {
        // Simulated API call
        const fetchTemperature = async () => {
            // Assuming fetchTemperatureData is a function to fetch temperature data from the server
            const temperatureData = await fetchTemperatureData();
            setTemperature(temperatureData);
            // Check if temperature is too high
            if (temperatureData > 35) {
                setShowPopup(true);
            } else {
                setShowPopup(false);
            }
        };
        fetchTemperature();
    }, []);

    // Simulated function to fetch temperature data
    const fetchTemperatureData = async () => {
        // You can replace this with actual API call to fetch temperature data
        return 50; // Simulated temperature data
    };

    return (
        <div className="temperature-container">
            {showPopup && (
                <div className="popup">
                    <p>Error!</p>
                    <p>The temperature is too high</p>
                </div>
            )}
            <div className="icon-and-temp">
                <img src="./Images/temperature.png" alt="Temperature Icon" />
                <div className="temperature-info">
                    {temperature !== null ? `${temperature}°C` : 'Loading...'}
                </div>
            </div>
            <div className="temperature-text">
                {'Temperature'}
            </div>
        </div>
    );
}

export default TemperatureCard;
