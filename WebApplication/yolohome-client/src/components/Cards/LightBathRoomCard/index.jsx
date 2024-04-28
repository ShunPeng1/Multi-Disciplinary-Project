import React from "react";
import "./LightBathRoomCard.css"
import LightBathRoomButton from "../../Button/LightBathRoomButton"

const LightBathRoomCard = () => {

    return (
        <div className="LightBathRoom-container">
            <div className="LightBathRoomButton">
                <LightBathRoomButton />
            </div>
            <div className="LightBathRoom-header">
                {'Light Bath Room'}
            </div>
        </div>

    );
}

export default LightBathRoomCard;