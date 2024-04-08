// LeftImage1.js
import React from 'react';
import image1 from '../../assets/LeftImage1.png';

function LeftImage1() {
  return (
    <img src={image1} alt="Image 1" style={{ position: 'absolute', left: 330, top: 80, height: 156 }} />
  );
}

export default LeftImage1;
