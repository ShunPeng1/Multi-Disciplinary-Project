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
        <Logo width={160} />
      </div>
      <div className="navbar-content">
        <Link to="/Print" className={getNavItemClass("/Dashboard")} id="mainFunction">
          <img src="./Images/addIcon.png" alt="Add Icon" />
          <p>Home</p>
        </Link>

        <Link to="/Home" className={getNavItemClass("/History")}>
          <img src="./Images/home.png" alt="Home Icon" />
          <p>History</p>
        </Link>

        <Link to="/BuyPaper" className={getNavItemClass("/Help")}>
          <img src="./Images/shopping-cart.png" alt="Shopping Cart Icon" />
          <p>Help</p>
        </Link>

        <Link to="/History" className={getNavItemClass("/Profile")}>
          <img src="./Images/history.png" alt="History Icon" />
          <p>Profile</p>
        </Link>

        <Link to="/" className={getNavItemClass("/")} id="logout">
          <img src="./Images/logout.png" alt="Logout Icon" />
          <p>Log out</p>
        </Link>
      </div>
    </nav>
  );
};

export default NavBar;
