import React, { useState, useEffect } from "react";
import './TemperatureCard.css';
import FetchRequest from "../../api/api";

const TemperatureCard = () => {
    const [temperature, setTemperature] = useState(null);
    const [errorMessage, setErrorMessage] = useState(null);
    const [showPopup, setShowPopup] = useState(false);

    useEffect(() => {
        fetchData();
        const intervalId = setInterval(fetchData, 5000); // Fetch data every 5 seconds

        return () => {
            clearInterval(intervalId); // Clear interval on component unmount
        };
    }, []);

    const fetchData = () => {
        FetchRequest('api/IotDeviceApi/GetLatestSensorData', 'GET', {
            DeviceType: 'Temper'
        }, successCallback, errorCallback);
    };

    const successCallback = (data) => {
        console.log('Success:', data);
        setTemperature(data.Response);
        if (data.Response > 35) {
            setErrorMessage("Temperature is too high!");
            setShowPopup(true);
        } else {
            setShowPopup(false);
        }
    }

    const errorCallback = (error) => {
        console.error('Error:', error);
    }

    const handleDismiss = () => {
        setShowPopup(false);
    };

    return (
      <div className="temperature-container">
          {showPopup && (
            <div className="popup">
                <p>🛈 Temperature Alert!</p>
                <p>{errorMessage}</p>
                <button onClick={handleDismiss}>I know</button>
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