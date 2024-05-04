import React from "react";
import "./FanCard.css"
import FanButton from "../../Button/FanButton"

const FanCard = ({ fanSpeed, changeFanSpeed, isFanHandling }) => {

    return (
        <div className="fan-container">
            <div className="fanButton">
                <FanButton fanSpeed={fanSpeed} changeFanSpeed={changeFanSpeed} isFanHandling={isFanHandling} />
            </div>
            <div className="fan-header">
                {'Fan'}
            </div>
        </div>

    );
}

export default FanCard;