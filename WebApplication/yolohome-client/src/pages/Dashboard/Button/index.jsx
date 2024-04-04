import React, { useState } from 'react';

function Button() {
  // State to keep track of how many times the button has been clicked
  const [clickCount, setClickCount] = useState(0);

  // Function to handle button click
  const handleClick = () => {
    // Update state to increment click count
    setClickCount(clickCount + 1);
  };

  return (
    <div>
      {/* Display the button and the current click count */}
      <button onClick={handleClick}>Click me!</button>
      <p>You have clicked the button {clickCount} times.</p>
    </div>
  );
}

export default Button;