import React, { useState, useEffect } from 'react';
import "./Weather.css"

const Weather = ({ city }) => {
  const [weather, setWeather] = useState(null);
  const [error, setError] = useState(null);

  // initialize time data
  const [currentDateTime, setCurrentDateTime] = useState(new Date());
  const [storeDay, setStoreDay] = useState('');
  const [storeMon, setStoreMon] = useState('');
  const [storeYear, setStoreYear] = useState('');
  const [storeHour, setStoreHour] = useState('');
  const [storeMin, setStoreMin] = useState('');
  const [storeSec, setStoreSec] = useState('');
  const [humid, setHumid] = useState(0);
  const [tempe, setTempe] = useState(0);
  const [cond, setCond] = useState('');

  useEffect(() => {
    const fetchWeather = async () => {
      const apiKey = 'e3faffda95db2e3f0ab65d99e94dd2a5'; // Replace with your actual API key
      const url = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=metric`;

      try {
        const response = await fetch(url);
        if (!response.ok) {
          throw new Error('Failed to fetch weather data');
        }
        else {

        }
        const data = await response.json();
        setWeather(data);
      } catch (error) {
        setError(error.message);
        console.error("Failed to fetch weather data:", error);
      }
      setHumid(weather.main.humidity);
      setTempe(weather.main.temp);
      setCond(weather.weather[0].main);
    };

    fetchWeather();
    const intervalId = setInterval(fetchWeather, 600000); // Fetches every 10 minutes

    // set time each second
    const timeInterval = setInterval(() => {
      setCurrentDateTime(new Date());
      updateDateComponents(new Date());
    }, 1000);

    const updateDateComponents = (date) => {
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
    }

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
      <p className='currTemp'>Temperature: {tempe}°C</p>
      <p className='cond'>Weather Condition: {cond}</p>
      <p className='humid'>Humidity: {humid}%</p>
      <p className='dateAndTime'>{storeDay}, {storeMon}, {storeYear}, {storeHour}, {storeMin}, {storeSec}</p>
    </div>
  );
};

export default Weather;
