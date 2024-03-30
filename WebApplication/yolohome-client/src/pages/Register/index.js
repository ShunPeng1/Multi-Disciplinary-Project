// Register.js
import React, { useState } from 'react';
import { Link } from "react-router-dom";
import './Register.css'; // Import CSS file for styling
import LeftImage1 from './LeftImage1';
import LeftImage2 from './LeftImage2';

function Register() {
  // State variables for registration fields
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  // Function to handle form submission
  const handleSubmit = (event) => {
    event.preventDefault();
    // Add logic to handle registration
    console.log('Registering with:', { username, email, password });
  };

  return (
    <div className='register'>
      <LeftImage1 />
      <LeftImage2 />
      <section>
        <div className='form-loginbox'>
          <div className='form-value'>
            <form onSubmit={handleSubmit}>
              <h2>Register</h2>
              <div className='inputbox'>
                <input type = 'text' onChange={(e) => setUsername(e.target.value)} required/>
                <label>Email</label>
              </div>
              <div className='inputbox'>
                <input type = 'password' onChange={(e) => setPassword(e.target.value)} required/>
                <label>Password</label>
              </div>
              <button type='submit' className='login-btn'>Register</button>
              <div>
                <Link className='register-link' to='/'>Had an account</Link>
              </div>


            </form>
          </div>
        </div>
      </section>
    </div>
  );
}

export default Register;
