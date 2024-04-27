import React from "react";
import "./LightLivingRoomCard.css"
import LightLivingRoomButton from "../../Button/LightLivingRoomButton"

const LightLivingRoomCard = () => {

    return (
        <div className="LightLivingRoom-container">
            <div className="LightLivingRoomButton">
                <LightLivingRoomButton />
            </div>
            <div className="LightLivingRoom-header">
                {'LightLivingRoom'}
            </div>
        </div>

    );
}

export default LightLivingRoomCard;