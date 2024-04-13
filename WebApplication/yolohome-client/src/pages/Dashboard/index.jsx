import "./Dashboard.css";
import React from 'react';
import LightCard from "../../components/Cards/LightCard";
import FanCard from "../../components/Cards/FanCard";
import DoorCard from "../../components/Cards/DoorCard";
import HumidityCard from "../../components/Cards/HumidityCard";
import TemperatureCard from "../../components/Cards/TemperatureCard";
import Header from "../../components/Header/Header";
import FanSpeedSlider from "../../components/FanSlider";

const Dashboard = (props) => {

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
            <img src="./Images/humidity_chart.png" alt ="Humidity Chart"/>
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
            <img src="./Images/temp_chart.png" alt="Temperature Chart" />
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