// import { useState } from "react";
import { BrowserRouter as Router, Route, Routes, useLocation } from "react-router-dom";
// import { Home, BuyPaper, History, Print, Profile, Login, PrintConfig } from "./Pages";
import "./App.css";
import { useEffect, useState } from "react";
import NavBar  from "./components/NavBar";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import History from './pages/History';
import Register from "./pages/Register";
import Help from "./pages/Help";
import Profile from "./pages/Profile";

const MaybeShowNavbar = ({children}) => {
  const location = useLocation();
  const [showNavbar, setShowNavbar] = useState(false);

  useEffect(() => {
    const noNavbarRoutes = ['/', '/register', '/PrintConfig'];
    setShowNavbar(!noNavbarRoutes.includes(location.pathname));
  }, [location]);

  return (
    <div>{showNavbar && children}</div>
  )
}

function App() {
  // const [count, setCount] = useState(0);
 

  return (
    <Router>
      <div className="App">
        <MaybeShowNavbar className="custom-navbar">
          <NavBar/>
        </MaybeShowNavbar>
        <div className="content">
          <Routes>
            <Route exact path="/" element={<Login />} />
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/history" element={<History/>} />
            <Route path="/register" element={<Register/>} />
            <Route path="/help" element={<Help/>} />
            <Route path="/profile" element={<Profile/>} />

          </Routes>
        </div>
      </div>
    </Router>
  );
}
export default App;
