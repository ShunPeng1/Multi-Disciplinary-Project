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
      question: "Why is my smart device not responding or connecting to the app?",
      answer: "Your smart device may not be responding or connecting to the app due to potential reasons such as network issues, device compatibility, app permissions, outdated firmware, or temporary glitches. Try troubleshooting by checking Wi-Fi connections, ensuring device compatibility, granting necessary app permissions, updating firmware, and restarting devices. If issues persist, contact customer support for assistance."
    },
    {
      question: "What should I do if I forget my account password?",
      answer: (
        <div>
          <p>If you forget your account password, simply follow these steps:</p>
          <ol>
            <li>Visit the login page and click on "Forgot Password."</li>
            <li>Enter your email address.</li>
            <li>Check your email for a password reset link.</li>
            <li>Follow the instructions to set a new password.</li>
            <li>Log in using your new password.</li>
          </ol>
          <p>For further assistance, please contact us for more support.</p>
        </div>
      )
    },    
    {
      question: "How can I view my activity history in the app?",
      answer: (
        <div>
          <p>To view your activity history in the app, follow these steps:</p>
          <ol>
            <li>Open the app on your mobile device.</li>
            <li>Navigate to the activity history section.</li>
            <li>Here, you can access a log of your past activities and interactions.</li>
          </ol>
          <p>For further assistance, please contact us for more support.</p>
        </div>
      )
    },    
    {
      question: "Can I access my smart home devices remotely when I'm away from home?",
      answer: (
        <div>
          <p>Yes, you can access your smart home devices remotely when you're away from home by using your mobile device's 4G or internet connection.</p>
          <p> </p>
          <p>Simply open the app on your mobile device and navigate to the remote access section. From there, you can control your smart home devices from anywhere with an internet connection.</p>
          <p> </p>
          <p>For further assistance, refer to the app's user manual or contact support.</p>
        </div>
      )
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
