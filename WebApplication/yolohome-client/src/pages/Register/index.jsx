// Register.js
import React, { useState } from 'react';
// import { Link } from "react-router-dom";
import './Register.css'; // Import CSS file for styling
import LeftImage1 from './LeftImage1';
import LeftImage2 from './LeftImage2';
import FetchRequest from "../../components/api/api";

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
        FetchRequest('api/AuthenticationApi/Register', 'POST', {
            Email: email,
            FirstName: firstname,
            LastName: lastname,
            UserName: username,
            Password: password,
            
        }, successCallback, errorCallback);
    };

    const successCallback = (data) => {
      console.log('Success:', data);
      localStorage.setItem('token', data.Token);
      localStorage.setItem('username', data.UserName); // Store the username
      window.location.href = '/';
    }

    const errorCallback = (error) => {
        console.error('Error:', error);
    }

  return (
    <div className='register'>
      <LeftImage1 />
      <LeftImage2 />
      <section>
        <div className='form-box'>
          <div className='form-value'>
            <form onSubmit={handleSubmit}>
              <div className='signupTitle'>Sign Up</div>
              <div className='signupDesc'>Enter your information to create a new account.</div>
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
                  <input type="text" onChange={(event) => setEmail(event.target.value)} />
                  <label>Email</label>
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
