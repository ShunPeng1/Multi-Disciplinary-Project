import "./Help.css";
import React from 'react';

import Header from "../../components/Header/Header";
const Help = (props) => {

  return (
    <div className="help">
      <div className="contentSection">
        {/* {Import Header} */}
        <div className="import-header">
          <Header />
        </div>
        <a href="https://forms.gle/sZ3vMUUJJxgo1QLc8" target="_blank" rel="noopener noreferrer"> {/* Add this line */}
          <button className="helpButton">Need help</button>
        </a> {/* Add this line */}
      </div>


    </div>  
   
  );
};

export default Help;