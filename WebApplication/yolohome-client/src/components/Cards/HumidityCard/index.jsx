import React from "react";
import "./HumidityCard.css";

const HumidityCard = () => {
    // receive data from server

    return (
        <div className = "humidity-container">
            <div className = "icon-and-humid">
                <img src="./Images/humidity.png" atl = "Humidity Icon"/>
                <div className="humidity-info">
                    {'80%'}
                </div>
            </div>
            <div className="humidity-text">
                {'Humidity'}
            </div>
        </div>
    );
}

export default HumidityCard;