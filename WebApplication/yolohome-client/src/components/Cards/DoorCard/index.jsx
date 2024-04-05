import React from "react";
import "./DoorCard.css"
import DoorButton from "../../Button/DoorButton"

const DoorCard = () => {

    return (
        <div className="door-container">
            <div className="doorButton">
                <DoorButton />
            </div>
            <div className="door-header">
                {'Door'}
            </div>
        </div>

    );
}

export default DoorCard;