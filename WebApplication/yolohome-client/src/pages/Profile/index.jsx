import React, { useState, useEffect } from 'react';
import "./Profile.css";
import Header from "../../components/Header/Header";
import FetchRequest from "../../components/api/api";

const Profile = () => {
  const [userData, setUserData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    username: ''
  });

  useEffect(() => {
    const username = localStorage.getItem('username');
    FetchRequest(`api/UserApi/GetUserInformation`, 'GET', {
        UserName: username
    }, successCallback, errorCallback);
  }, []);

  const successCallback = (data) => {
    setUserData({
      firstName: data.FirstName,
      lastName: data.LastName,
      email: data.Email,
      username: data.UserName
    });
  }

  const errorCallback = (error) => {
    console.error('Error:', error);
  }

  return (
    <div className="profile">
      <div className="contentSection2">
        <div className="import-header2">
          <Header />
        </div>

        <div className="contentSection">
          <div className="welcome">
            <p className="specialWelcomee">Your Profile</p>
          </div>
          <div className="profilee">
            <div className="info">
              <div className="accountProp">
                <div className="column">
                  {/* first column */}
                  <p>First Name</p>
                  <input
                    type="text"
                    disabled="disabled"
                    value={userData.firstName}
                    className="disableInput"
                  />
                  <p>Last Name</p>
                  <input
                    type="text"
                    disabled="disabled"
                    value={userData.lastName}
                    className="disableInput"
                  />

                </div>
                <div className="column">
                  {/* second column */}
                  <p>Email</p>
                  <input
                    type="text"
                    disabled="disabled"
                    value={userData.email}
                    className="disableInput"
                  />
                  <p>Username</p>
                  <input
                    type="text"
                    disabled="disabled"
                    value={userData.username}
                    className="disableInput"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Profile;