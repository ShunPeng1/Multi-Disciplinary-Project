// App.js
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'; // Remove Switch
import Login from './pages/Login';
import Register from './pages/Register'; // Update import
import Dashboard from './pages/Dashboard';
// import DefaultLayout from './components/layouts/DefaultLayout';
function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login></Login>} />
        <Route path="/register" element={<Register></Register>} /> {/* Update Route */}
        <Route path="/dashboard" element={<Dashboard />} />
      </Routes>
    </Router>
  );
}

export default App;
