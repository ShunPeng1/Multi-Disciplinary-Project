import React from 'react';
import { Link } from 'react-router-dom';

function DefaultLayout() {
  return (
    <div>
      <Link to="/login">Login</Link>
      <Link to="/register">Register</Link>
      {/* Make sure the path is correct */}
    </div>
  );
}

export default DefaultLayout;
