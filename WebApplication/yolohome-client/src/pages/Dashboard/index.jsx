import "./Dashboard.css";
import LightLivingRoomCard from "../../components/Cards/LightLivingRoomCard";
import LightBedRoomCard from "../../components/Cards/LightBedRoomCard";
import LightKitchenCard from "../../components/Cards/LightKitchenCard";
import LightBathRoomCard from "../../components/Cards/LightBathRoomCard";
import FanCard from "../../components/Cards/FanCard";
import DoorCard from "../../components/Cards/DoorCard";
import HumidityCard from "../../components/Cards/HumidityCard";
import TemperatureCard from "../../components/Cards/TemperatureCard";
import Header from "../../components/Header/Header";
import FanSpeedSlider from "../../components/FanSlider";
import TemperatureChart from "../../components/Charts/TempChart";
import HumidityChart from "../../components/Charts/HumidChart";

import React, { useState, useEffect } from "react";
import FetchRequest from "../../components/api/api";


const Dashboard = (props) => {
  
  const [temperatureData, setTemperatureData] = useState([]);
  const [humidityData, setHumidityData] = useState([]);
  const [fanSpeed, setFanSpeed] = useState(0);
  const [isFanHandling, setIsFanHandling] = useState(false);

  const username = localStorage.getItem('username');

  useEffect(() => {
    fetchData('Temper', setTemperatureData);
    fetchData('Humidity', setHumidityData);
    fetchFanData();
    const intervalId = setInterval(() => {
      fetchData('Temper', setTemperatureData);
      fetchData('Humidity', setHumidityData);
      fetchFanData();
    }, 5000); // Fetch data every 5 seconds

    return () => {
      clearInterval(intervalId); // Clear interval on component unmount
    };
  }, []);

  const fetchData = (deviceType, setFunction) => {
    const now = new Date();
    const twentyFourHoursAgo = new Date(now.getTime() - 24 * 60 * 60 * 1000); // 24 hours ago

    FetchRequest('api/IotDeviceApi/GetAllSensorData', 'GET', {
      DeviceType: deviceType,
      Start: twentyFourHoursAgo,
      End: now
    }, (data) => successCallback(data, setFunction), errorCallback);
  };
  
  const successCallback = (data, setFunction) => {
    console.log('Success:', data);
    const formattedData = data.map(item => {
      const date = new Date(item.TimeStamp);
      const hours = date.getHours();
      const minutes = date.getMinutes();
      const formattedTime = `${hours}:${minutes < 10 ? '0' : ''}${minutes}`; // Format time as 'HH:MM'
      return {
        time: formattedTime,
        value: parseFloat(item.Response) // Ensure the value is a number
      };
    });
    setFunction(formattedData);
  }
  const errorCallback = (error) => {
    console.error('Error:', error);
  }
  
  const fetchFanData = () => {
    FetchRequest('api/IotDeviceApi/GetLatestSensorData', 'GET', {
      DeviceType: 'Fan'
    }, successFanCallback, errorFanCallback);
  }

  const changeFanSpeed = (fanSpeed) => {
    if (isFanHandling) {
      return;
    }
    
    setIsFanHandling(true);
    setFanSpeed(fanSpeed);
    FetchRequest('api/ManualControlApi/Control', 'POST', {
      UserName: username,
      Kind: 'Fan',
      Command: fanSpeed // Use the passed fan speed
    }, successFanCallback, errorFanCallback);
  }
  const successFanCallback = (data) => {
    console.log('Success:', data);
    setFanSpeed(data.Response);
    setIsFanHandling(false);
  }
  const errorFanCallback = (error) => {
    console.error('Error:', error);
    setIsFanHandling(false);
  }

  // layout
  return (
    <div className="home">
      <div className="contentSection">
        {/* {Import Header} */}
        <div className="import-header">
          <Header />
        </div>
        
        {/* Display Information */}

        <div className="living_kitchen_humid_charthumid">
          <div className="LightLivingRoomCard">
            <LightLivingRoomCard />
          </div>
          <div className="LightKitchenCard">
            <LightKitchenCard />
          </div>
          <div className="humidityCard">
            <HumidityCard />
          </div>
          {/* Humidity Chart */}
          <div className="humidityChart">
            {/* <img src="./Images/humidity_chart.png" alt ="Humidity Chart"/> */}
            <HumidityChart data={[...humidityData].reverse()} />
          </div>
        </div>
        
        <div className="bath_door_temp">
          <div className="LightBathRoomCard">
            <LightBathRoomCard />
          </div>
          <div className="doorCard">
            <DoorCard />
          </div>   
          <div className="temperatureCard">
            <TemperatureCard />
          </div>
          {/* Temperature Chart */}
          <div className="tempChart">
            {/* <img src="./Images/temp_chart.png" alt="Temperature Chart" /> */}
            <TemperatureChart data={[...temperatureData].reverse()} />
          </div>
        </div>  
        <div className="fan-and-slider">
          <div className="LightBedRoomCard">
            <LightBedRoomCard />
          </div>
          <div className="fanCard">
            <FanCard fanSpeed={fanSpeed} changeFanSpeed={changeFanSpeed} isFanHandling={isFanHandling}/>
          </div>
          <div className="sliderBar">
            <FanSpeedSlider fanSpeed={fanSpeed} changeFanSpeed={changeFanSpeed} isFanHandling={isFanHandling}/>
          </div>
        </div>
        
      </div>     
    </div>
  );
};

export default Dashboard;