import React from "react";
import { Link, useLocation } from "react-router-dom";
import "./NavBar.css";

import { ReactComponent as Logo } from "../../assets/logo.svg";

const NavBar = () => {
  const location = useLocation();

  const getNavItemClass = (pathname) => {
    return location.pathname === pathname ? "navbar-item current-page" : "navbar-item";
  };

  return (
    <nav className="navbar">
      <div className="logo">
        <Logo width={200} />
      </div>
      <div className="navbar-content">
        <Link to="/dashboard" className={getNavItemClass("/dashboard")} id="mainFunction">
          <img src="./Images/home.png" alt="Home Icon" />
          <p>Home</p>
        </Link>

        <Link to="/history" className={getNavItemClass("/history")}>
          <img src="./Images/file.png" alt="File Icon" />
          <p>History</p>
        </Link>

        <Link to="/help" className={getNavItemClass("/help")}>
          <img src="./Images/question_icon.png" alt="Question Mark Icon" />
          <p>Help</p>
        </Link>

        <Link to="/profile" className={getNavItemClass("/profile")}>
          <img src="./Images/profile_icon.png" alt="Profile Icon" />
          <p>Profile</p>
        </Link>

        <Link to="/" className={getNavItemClass("/")} id="logout">
          <img src="./Images/log_out.png" alt="Logout Icon" />
          <p>Log out</p>
        </Link>
      </div>
    </nav>
  );
};

export default NavBar;
