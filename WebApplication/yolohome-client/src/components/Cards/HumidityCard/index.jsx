import React, { useState, useEffect } from "react";
import "./HumidityCard.css";
import FetchRequest from "../../api/api";

const HumidityCard = () => {
  const [humidity, setHumidity] = useState('0%');

  useEffect(() => {
    fetchData();
    const intervalId = setInterval(fetchData, 5000); // Fetch data every 5 seconds

    return () => {
      clearInterval(intervalId); // Clear interval on component unmount
    };
  }, []);

  const fetchData = () => {
    FetchRequest('api/IotDeviceApi/GetLatestSensorData', 'GET', {
      DeviceType: 'Humidity'
    }, successCallback, errorCallback);
  };

  const successCallback = (data) => {
    console.log('Success:', data);
    setHumidity(data.Response + '%');
  }

  const errorCallback = (error) => {
    console.error('Error:', error);
  }

  return (
    <div className = "humidity-container">
      <div className = "icon-and-humid">
        <img src="./Images/humidity.png" alt = "Humidity Icon"/>
        <div className="humidity-info">
          {humidity}
        </div>
      </div>
      <div className="humidity-text">
        {'Humidity'}
      </div>
    </div>
  );
}

export default HumidityCard;