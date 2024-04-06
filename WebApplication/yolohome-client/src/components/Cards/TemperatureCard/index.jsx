    import React, { useState, useEffect } from "react";
    import './TemperatureCard.css';

    const TemperatureCard = () => {
        const [temperature, setTemperature] = useState(null);
        const [errorMessage, setErrorMessage] = useState(null);
        const [showPopup, setShowPopup] = useState(false);

        useEffect(() => {
            const fetchTemperature = async () => {
                const temperatureData = await fetchTemperatureData();
                setTemperature(temperatureData.temperature);
                if (temperatureData.temperature > 35) {
                    setErrorMessage(temperatureData.message);
                    setShowPopup(true);
                } else {
                    setShowPopup(false);
                }
            };
            fetchTemperature();
        }, []);

        // const fetchTemperatureData = async () => {
        //     try {
        //         const response = await fetch('your_api_endpoint');
        //         if (!response.ok) {
        //             throw new Error('Failed to fetch temperature data');
        //         }
        //         const data = await response.json();
        //         return data;
        //     } catch (error) {
        //         console.error('Error fetching temperature data:', error);
        //         return { temperature: null, message: 'Error fetching temperature data' };
        //     }
        // };

        // example fetchTemperatureData function, replace by the above function or recreate one
        const fetchTemperatureData = async () => {
            return { temperature: 45, message: "Temperature is too high!" };
        };

        const handleDismiss = () => {
            setShowPopup(false);
        };

        return (
            <div className="temperature-container">
                {showPopup && (
                    <div className="popup">
                        <p>🛈 Temperature Alert!</p>
                        <p>{errorMessage}</p>
                        <button onClick={handleDismiss}>I know</button>
                    </div>
                )}
                <div className="icon-and-temp">
                    <img src="./Images/temperature.png" alt="Temperature Icon" />
                    <div className="temperature-info">
                        {temperature !== null ? `${temperature}°C` : 'Loading...'}
                    </div>
                </div>
                <div className="temperature-text">
                    {'Temperature'}
                </div>
            </div>
        );
    }

    export default TemperatureCard;
