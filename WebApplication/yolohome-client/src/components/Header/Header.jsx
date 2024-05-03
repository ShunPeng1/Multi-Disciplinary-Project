import React from "react";
import "./Header.css";
import Weather from "../weather";

const Header = () => {

    return (
        <div className="header">
          <div className="viewItem">
            <p className="specialWelcome">Welcome</p>
            <p className="controlSentence">Control your home from here!</p>
          </div>
          <div className="weatherInfo">
            <Weather city="Ho Chi Minh" />
          </div>
          <div className="viewImg2">
            <img src="./Images/background_new.png" alt="background_new" ></img>
          </div>
          
        </div>
    )
}

export default Header;