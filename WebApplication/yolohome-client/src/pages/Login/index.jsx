// Login.js
import React, { useState } from 'react';
import './Login.css'; // Import CSS file for styling
import LeftImage1 from './LeftImage1';
import LeftImage2 from './LeftImage2';

import FetchRequest from '../../components/api/api';
function Login() {
  // State variables for username and password
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  // Function to handle form submission
  const handleSubmit = (event) => {
    event.preventDefault();
    FetchRequest('api/AuthenticationApi/Login', 'POST', {
        UserName: username,
        Password: password
        }, successCallback, errorCallback);
  };
  
  const successCallback = (data) => {
    console.log('Success:', data);
    localStorage.setItem('token', data);
    window.location.href = '/dashboard';
  }
  
  const errorCallback = (error) => {
    console.error('Error:', error);
  }

  return (
    <div className='login'>
      <LeftImage1 />
      <LeftImage2 />
      <section>
        <div className='form-loginbox'>
          <div className='form-value'>
            <form onSubmit={handleSubmit}>
              <h2>Login</h2>
              <div className='inputbox'>
                <input type = 'text' onChange={(e) => setUsername(e.target.value)} required/>
                <label>User Name</label>
              </div>
              <div className='inputbox'>
                <input type = 'password' onChange={(e) => setPassword(e.target.value)} required/>
                <label>Password</label>
              </div>
              <button type='submit' className='login-btn'>Log in</button>
              <div class='register-block'>
                  <p>Don't have an account?&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='/register'>Register</a></p>
              </div>

            </form>
          </div>
        </div>
      </section>
    </div>
  );
}

export default Login;
