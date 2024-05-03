import React, { useState, useEffect, useCallback } from 'react';
import "./Weather.css";

const Weather = ({ city }) => {
  const [weather, setWeather] = useState(null);
  const [error, setError] = useState(null);

  // Initialize time data
  const [currentDateTime, setCurrentDateTime] = useState(new Date());
  const [storeDay, setStoreDay] = useState('');
  const [storeMon, setStoreMon] = useState('');
  const [storeYear, setStoreYear] = useState('');
  const [storeHour, setStoreHour] = useState('');
  const [storeMin, setStoreMin] = useState('');
  const [storeSec, setStoreSec] = useState('');

  const updateDateComponents = useCallback((date) => {
    const options = {year: 'numeric', month: 'short', day: '2-digit', hour: '2-digit', minute: '2-digit', second: '2-digit', hour12: false};
    const formattedString = new Intl.DateTimeFormat('en-US', options).format(date);

    const [monthDay, year, time] = formattedString.split(', ');
    const [month, day] = monthDay.split(' ');
    const [hour, minute, second] = time.split(':');

    setStoreDay(day);
    setStoreMon(month);
    setStoreYear(year);
    setStoreHour(hour);
    setStoreMin(minute);
    setStoreSec(second);
  }, []);

  useEffect(() => {
    const fetchWeather = async () => {
      const apiKey = '9da53d479d778433cad8243dfc07a742';
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
    
    const timeInterval = setInterval(() => {
      const now = new Date();
      setCurrentDateTime(now);
      updateDateComponents(now);
    }, 1000);

    return () => {
      clearInterval(intervalId);
      clearInterval(timeInterval);
    };
  }, [city, updateDateComponents]);

  if (error) {
    return <p>Error: {error}</p>;
  }

  if (!weather) {
    return <p>Loading...</p>;
  }

  return (
    <div className="weather-widget">
      <div className="date-time">{storeMon} {storeDay}</div>
      <div className="time">{storeHour}:{storeMin}</div>
      <div className="weather-info">
        <div className="condition">{weather.weather[0].main}</div>
        <div className="location">{city}</div>
        <div className="temperature">{Math.round(weather.main.temp)}°C</div>
      </div>
    </div>  
  );
};

export default Weather;