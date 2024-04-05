import React from "react";
import "./LightCard.css"
import LightButton from "../../Button/LightButton"

const LightCard = () => {

    return (
        <div className="light-container">
            <div className="lightButton">
                <LightButton />
            </div>
            <div className="light-header">
                {'Light'}
            </div>
        </div>

    );
}

export default LightCard;