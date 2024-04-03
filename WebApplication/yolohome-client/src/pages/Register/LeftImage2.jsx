// LeftImage2.js
import React from 'react';
import image2 from '../../assets/LeftImage2.png';

function LeftImage2() {
  return (
    <img src={image2} alt="Image 2" style={{ position: 'absolute', left: 300, top: 250, height: '60vh' }} />
  );
}

export default LeftImage2;
