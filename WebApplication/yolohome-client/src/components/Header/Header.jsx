import React from "react";
import "./Header.css";

const Header = () => {

    return (
        <div className="header">
          <div className="viewItem">
            <p className="specialWelcome">Welcome</p>
            <p className="controlSentence">Control your home from here!</p>
          </div>

          <div className="viewImg2">
            <img src="./Images/background_new.png" alt="background_new" ></img>
          </div>
          
        </div>
    )
}

export default Header;