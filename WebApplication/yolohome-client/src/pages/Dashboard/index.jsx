import "./Dashboard.css";
import React from 'react';
import LightCard from "../../components/Cards/LightCard";
import FanCard from "../../components/Cards/FanCard";
import DoorCard from "../../components/Cards/DoorCard";
import HumidityCard from "../../components/Cards/HumidityCard";
import TemperatureCard from "../../components/Cards/TemperatureCard";
const Dashboard = (props) => {

  return (
    <div className="home">
        <div className="contentSection">
          <div className="importantView">
            <div className="viewItem">
              <p className="specialWelcome">Welcome</p>
              <p className="controlSentence">Control your home from here!</p>
            </div>

            <div className="viewImg2">
              <img src="./Images/background_new.png" alt="background_new" ></img>
            </div>
            
          </div>

          <div className="waitingLog">
            <p className="waitP">Đang chờ</p>         
          </div>
          
          {/* {CardButton and Information part} */}
          <div className="">
            
          </div>
        </div>     
    </div>
  );
};

export default Dashboard;
