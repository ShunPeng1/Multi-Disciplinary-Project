import React from "react";
import "./Profile.css";
import Header from "../../components/Header/Header";

const Profile = () => {
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
                    value="Thanh"
                    className="disableInput"
                  />
                  <p>Last Name</p>
                  <input
                    type="text"
                    disabled="disabled"
                    value="Thanh"
                    className="disableInput"
                  />
                  
                </div>
                <div className="column">
                  {/* second column */}
                  <p>Email</p>
                  <input
                    type="text"
                    disabled="disabled"
                    value="test@hcmut.edu.vn"
                    className="disableInput"
                  />
                  <p>Username</p>
                  <input
                    type="text"
                    disabled="disabled"
                    value="Thanh"
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
