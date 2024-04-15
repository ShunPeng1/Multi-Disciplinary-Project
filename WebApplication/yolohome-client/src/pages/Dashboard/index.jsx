import "./Dashboard.css";
import React from 'react';
import LightCard from "../../components/Cards/LightCard";
import FanCard from "../../components/Cards/FanCard";
import DoorCard from "../../components/Cards/DoorCard";
import HumidityCard from "../../components/Cards/HumidityCard";
import TemperatureCard from "../../components/Cards/TemperatureCard";
import Header from "../../components/Header/Header";
import FanSpeedSlider from "../../components/FanSlider";
import TemperatureChart from "../../components/Charts/TempChart";
import HumidityChart from "../../components/Charts/HumidChart";

const Dashboard = (props) => {

  // data for TempChart
  const temperatureData = [
    { time: '0AM', value: 22 },
    { time: '1PM', value: 26 },
    { time: '2PM', value: 24 },
    { time: '3PM', value: 22 },
    { time: '4PM', value: 20 },
    { time: '5PM', value: 21 },
    { time: '6PM', value: 23 },
    { time: '7PM', value: 25 },
    { time: '8PM', value: 27 },
    { time: '9PM', value: 29 },
  ];

  // data for the HumidChart
  const humidityData = [
    { time: '0AM', value: 50 },
    { time: '1PM', value: 60 },
    { time: '2PM', value: 70 },
    { time: '3PM', value: 20 },
    { time: '4PM', value: 20 },
    { time: '5PM', value: 10 },
    { time: '6PM', value: 30 },
    { time: '7PM', value: 58 },
    { time: '8PM', value: 90 },
    { time: '9PM', value: 99 },
  ];

  // layout
  return (
    <div className="home">
      <div className="contentSection">
        {/* {Import Header} */}
        <div className="import-header">
          <Header />
        </div>
        
        {/* Display Information */}

        <div className="light-and-humid">
          <div className="lightCard">
            <LightCard />
          </div>
          <div className="humidityCard">
            <HumidityCard />
          </div>
          {/* Humidity Chart */}
          <div className="humidityChart">
            {/* <img src="./Images/humidity_chart.png" alt ="Humidity Chart"/> */}
            <HumidityChart data={humidityData} />
          </div>
        </div>
        
        <div className="door-and-temp">
          <div className="doorCard">
            <DoorCard />
          </div>
          <div className="temperatureCard">
            <TemperatureCard />
          </div>
          {/* Temperature Chart */}
          <div className="tempChart">
            {/* <img src="./Images/temp_chart.png" alt="Temperature Chart" /> */}
            <TemperatureChart data={temperatureData} />
          </div>
        </div>
        <div className="fan-and-slider">
          <div className="fanCard">
            <FanCard />
          </div>
          <div className="sliderBar">
            <FanSpeedSlider />
          </div>
        </div>
        
      </div>     
    </div>
  );
};

export default Dashboard;