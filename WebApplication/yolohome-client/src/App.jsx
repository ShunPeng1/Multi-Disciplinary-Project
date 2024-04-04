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

const MaybeShowNavbar = ({children}) => {
  const location = useLocation();
  const [showNavbar, setShowNavbar] = useState(false);

  useEffect(() => {
    if (location.pathname === '/' || location.pathname === '/Print' || location.pathname === '/PrintConfig') {
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
  // const [count, setCount] = useState(0);
 

  return (
    <Router>
      <div className="App">
        <MaybeShowNavbar>
          <NavBar/>
        </MaybeShowNavbar>
        <div className="content">
          <Routes>
            <Route exact path="/" element={<Login />} />
            <Route path="/dashboard" element={<Dashboard />} />
            {/* <Route path="/BuyPaper" element={<BuyPaper paperHistoryItems = {paperHistoryItems} updatePaperHistoryItems = {updatePaperHistoryItems} pageNumber = {page} updatePageNumber = {updatePage} />} /> */}
            <Route path="/history" element={<History/>} />
            {/* <Route path="/Print" element={<Print />} /> */}
            {/* <Route path="/Profile" element={<Profile image={image} handleImageChange={handleImageChange} printTimes={printTimes} page={page} waiting={countPrintStatusWait}/>} /> */}
            <Route path="/register" element={<Register/>} />
          </Routes>
        </div>
      </div>
    </Router>
  );
}
export default App;
