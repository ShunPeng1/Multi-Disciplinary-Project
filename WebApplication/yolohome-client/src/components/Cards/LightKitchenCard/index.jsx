import React from "react";
import "./LightKitchenCard.css"
import LightKitchenButton from "../../Button/LightKitchenButton"

const LightKitchenCard = () => {

    return (
        <div className="LightKitchen-container">
            <div className="LightKitchenButton">
                <LightKitchenButton />
            </div>
            <div className="LightKitchen-header">
                {'LightKitchen'}
            </div>
        </div>

    );
}

export default LightKitchenCard;