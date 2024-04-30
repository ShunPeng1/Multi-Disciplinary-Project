// help.jsx

import "./Help.css";
import React, { useState } from 'react';
import Header from "../../components/Header/Header";

const Help = (props) => {
  const [expanded, setExpanded] = useState(Array(5).fill(false));

  const toggleExpansion = (index) => {
    const newExpanded = [...expanded];
    newExpanded[index] = !newExpanded[index];
    setExpanded(newExpanded);
  };

  const faqData = [
    {
      question: "How do I add a new device to my smart home network?",
      answer: "To add a new device, first ensure that it's compatible with your smart home system. \
              In our app, navigate to the Home Page or Rooms section, select Add Device, \
              and follow the prompts to pair the new device with your account."
    },
    {
      question: "Why is my smart device not responding or connecting to the app?",
      answer: "If you forget your password, you can..."
    },
    {
      question: "What should I do if I forget my account password?",
      answer: "You can contact support by..."
    },
    {
      question: "How can I view my activity history in the app?",
      answer: "Yes, we have a mobile app available for..."
    },
    {
      question: "Can I access my smart home devices remotely when I'm away from home?",
      answer: "To update your profile, go to..."
    }
  ];

  return (
    <div className="help">
      <div className="contentSection">
        <div className="faqSection">
          <h2>Frequently Asked Questions</h2>
          <div className="faqq">
          {faqData.map((faq, index) => (
            <div key={index} className={`faq ${expanded[index] ? 'expanded' : ''}`}>
              <div className="question">
                <h3>{faq.question}</h3>
                <button className="expandButton" onClick={() => toggleExpansion(index)}>+</button>
              </div>
              {expanded[index] && (
                <div className="answer">
                  <p>{faq.answer}</p>
                </div>
              )}
            </div>
          ))}
          </div>
        </div>
        <a href="https://forms.gle/sZ3vMUUJJxgo1QLc8" target="_blank" rel="noopener noreferrer">
          <button className="helpButton">Need help</button>
        </a>  
      </div>
    </div>
  );
};

export default Help;
