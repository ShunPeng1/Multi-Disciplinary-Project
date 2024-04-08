// Login.js
import React, { useState } from 'react';
import './Login.css';
import LeftImage1 from './LeftImage1';
import LeftImage2 from './LeftImage2';

import FetchRequest from '../../components/api/api';
function Login() {
  // State variables for username and password
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPopup, setShowPopup] = useState(false);
  const [popupMessage, setPopupMessage] = useState('');

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
    localStorage.setItem('token', data.Token);
    localStorage.setItem('username', data.UserName); // Store the username
    setPopupMessage('Login successful');
    setShowPopup(true);
    // wait for 1 second before redirecting to dashboard
    setTimeout(() => {
      window.location.href = '/dashboard';
    }, 1000);
    
    
  }
  
  const errorCallback = (error) => {
    console.error('Error:', error);
    setPopupMessage('Login failed. Wrong password or username.');
    setShowPopup(true);
  }

  return (
    <div className='login'>
      <LeftImage1 />
      <LeftImage2 />
      <section>
        <div className='form-loginbox'>
          <div className='form-value'>
            <form onSubmit={handleSubmit}>
              <div className='loginTitle'>Login</div>
              <div className='loginDesc'>Enter given account information that you <br/>created.</div>
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
      {showPopup && (
        <div className="popup">
          <p>{popupMessage}</p>
          <button onClick={() => setShowPopup(false)}>Close</button>
        </div>
      )}
    </div>
  );
}

export default Login;
