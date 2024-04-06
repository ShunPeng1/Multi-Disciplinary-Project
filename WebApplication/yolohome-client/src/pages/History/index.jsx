import "./History.css";
import { useState, useEffect } from "react";

const History = (props) => {
  const [addModal, setAddModal] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [addValue, setAddValue] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(5);
  const [historyData, setHistoryData] = useState([]);

  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;

  useEffect(() => {
    // Fetch data from your database here
    // Example fetch:
    fetch("your_database_endpoint")
      .then((response) => response.json())
      .then((data) => setHistoryData(data))
      .catch((error) => console.error("Error fetching data:", error));
  }, []);

  return (
    <div className="buypaper">
      <div className="contentSection">
        <div className="titleContainer">
          <div className="viewItem">
            <p className="specialWelcome">Welcome</p>
            <p className="controlSentence">Control your home from here!</p>
          </div>

          <div className="viewImg2">
            <img src="./Images/background_new.png" alt="background_new" />
          </div>
        </div>

        <div className="buyLog">
          <div className="title">
            <span className="boxTitle">History</span>
          </div>

          <table className="table">
            <thead>
              <tr className="buyHeader">
                <th>No.</th>
                <th>Device</th>
                <th>Room</th>
                <th>Description</th>
                <th>Time</th>
              </tr>
            </thead>
            <tbody>
              {historyData
                .slice(indexOfFirstItem, indexOfLastItem)
                .map((item, index) => (
                  <tr key={index}>
                    <td>{index + 1}</td>
                    <td>{item.device}</td>
                    <td>{item.room}</td>
                    <td>{item.description}</td>
                    <td>{item.time}</td>
                  </tr>
                ))}
            </tbody>
          </table>

          <div className="pagination">
            <button
              onClick={() => setCurrentPage(currentPage - 1)}
              disabled={currentPage === 1}
            >
              &#9665;
            </button>
            <span>{currentPage}</span>
            <button onClick={() => setCurrentPage(currentPage + 1)}>
              &#9655;
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default History;
