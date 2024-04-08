// LeftImage2.js
import React from 'react';
import image2 from '../../assets/LeftImage2.png';
import "./LeftImage2.css"

function LeftImage2() {
  return (
    <div className='img2'>
      <img src={image2} alt="Image 2" style={{ position: 'absolute', left: 300, top: 250, height: '60vh' }} />
    </div>
  );
}

export default LeftImage2;
