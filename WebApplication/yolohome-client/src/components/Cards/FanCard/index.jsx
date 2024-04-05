import React from "react";
import "./FanCard.css"
import FanButton from "../../Button/FanButton"

const FanCard = () => {

    return (
        <div className="fan-container">
            <div className="fanButton">
                <FanButton />
            </div>
            <div className="fan-header">
                {'Fan'}
            </div>
        </div>

    );
}

export default FanCard;