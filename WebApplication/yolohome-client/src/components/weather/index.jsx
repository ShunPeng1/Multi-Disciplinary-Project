import React, { useState, useEffect } from 'react';
import "./Weather.css"

const Weather = ({ city }) => {
  const [weather, setWeather] = useState(null);
  const [error, setError] = useState(null);

  // initialize time data
  const [currentDateTime, setCurrenDateTime] = useState(new Date());

  useEffect(() => {
    const fetchWeather = async () => {
      const apiKey = 'e3faffda95db2e3f0ab65d99e94dd2a5'; // Replace with your actual API key
      const url = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=metric`;

      try {
        const response = await fetch(url);
        if (!response.ok) {
          throw new Error('Failed to fetch weather data');
        }
        const data = await response.json();
        setWeather(data);
      } catch (error) {
        setError(error.message);
        console.error("Failed to fetch weather data:", error);
      }
    };

    fetchWeather();
    const intervalId = setInterval(fetchWeather, 600000); // Fetches every 10 minutes

    // set time each second
    const timeInterval = setInterval(() => {
      setCurrenDateTime(new Date());
    }, 1000);

    return () => {
      clearInterval(intervalId); // Cleanup on component unmount
      clearInterval(timeInterval); // Clear time interval
    };
  }, [city]);

  if (error) {
    return <p>Error: {error}</p>;
  }

  if (!weather) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <h2 className='mycity'>Weather in {city}</h2>
      <p className='currTemp'>Temperature: {weather.main.temp}°C</p>
      <p className='cond'>Weather Condition: {weather.weather[0].main}</p>
      <p className='humid'>Humidity: {weather.main.humidity}%</p>
      <p className='dateAndTime'>Current Time: {currentDateTime.toLocaleString()}</p>
    </div>
  );
};

export default Weather;
