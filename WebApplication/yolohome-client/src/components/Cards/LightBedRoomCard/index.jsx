import React from "react";
import "./LightBedRoomCard.css"
import LightBedRoomButton from "../../Button/LightBedRoomButton"

const LightBedRoomCard = () => {

    return (
        <div className="LightBedRoom-container">
            <div className="LightBedRoomButton">
                <LightBedRoomButton />
            </div>
            <div className="LightBedRoom-header">
                {'Light Bed Room'}
            </div>
        </div>

    );
}

export default LightBedRoomCard;