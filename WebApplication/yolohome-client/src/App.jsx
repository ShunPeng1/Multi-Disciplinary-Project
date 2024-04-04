// App.js
import React from 'react';
import { useEffect, useState } from "react";
import { BrowserRouter as Router, Routes, Route, useLocation } from 'react-router-dom'; // Remove Switch
import Login from './pages/Login';
import Register from './pages/Register'; // Update import
import Dashboard from './pages/Dashboard';
import NavBar  from "./components/NavBar";
// import DefaultLayout from './components/layouts/DefaultLayout';
const MaybeShowNavbar = ({children}) => {
  const location = useLocation();
  const [showNavbar, setShowNavbar] = useState(false);

  useEffect(() => {
    if (location.pathname === '/' || location.pathname === '/register' || location.pathname === '/PrintConfig') {
      setShowNavbar(false);
    }
    else {
      setShowNavbar(true);
    }
  }, [location])

  return (
    <div>{showNavbar && children}</div>
  )
}
function App() {
  return (
    <Router>
      <div className="App">
        <MaybeShowNavbar>
          <NavBar/>
        </MaybeShowNavbar>
        <div className='content'>
          <Routes>
            <Route path="/" element={<Login></Login>} />
            <Route path="/register" element={<Register></Register>} /> 
            <Route path="/dashboard" element={<Dashboard></Dashboard>} />
          </Routes>
        </div>
      </div>

    </Router>
  );
}

export default App;
