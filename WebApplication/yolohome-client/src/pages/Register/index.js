// Register.js
import React, { useState } from 'react';
import { Link } from "react-router-dom";
import './Register.css'; // Import CSS file for styling
import LeftImage1 from './LeftImage1';
import LeftImage2 from './LeftImage2';

function Register() {
  // State variables for registration fields
  const [firstname, setFirstname] = useState('')
  const [lastname, setLastname] = useState('')
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
        <div className='form-box'>
          <div className='form-value'>
            <form onSubmit={handleSubmit}>
              <h2>Sign up</h2>
              <div className='box-name'>
                <div className='inputbox'>
                  <input type='text' onChange={(e) => setFirstname(e.target.value)} required/>
                  <label>First Name</label>
                </div>
                <div className='inputbox'>
                  <input type='text' onChange={(e) => setLastname(e.target.value)} required/>
                  <label>Last Name</label>
                </div>
              </div>
              <div className="inputbox">
                  <input type="text" onChange={(event) => setUsername(event.target.value)} />
                  <label>Username</label>
              </div>
              <div className='inputbox'>
                <input type = 'password' onChange={(e) => setPassword(e.target.value)} required/>
                <label>Password</label>
              </div>
              <div className='registet-btn-wrap'>
                <button className='signup-btn'>Sign up</button>
                <div className='register-block'>
                <p>Already have an account?&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='/'>Log in</a></p>
                </div>
              </div>
            </form>
          </div>
        </div>
      </section>
    </div>
  );
}

export default Register;
