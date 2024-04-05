import "./History.css";
import { useState, useEffect  } from "react";

const History = (props) => {
  const { paperHistoryItems , updatePaperHistoryItems, pageNumber, updatePageNumber } = props; 
  const [addModal, setAddModal] = useState(false)
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [addValue, setAddValue] = useState(0);

  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(5);

  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;

  return (

    <div className="buypaper">
      <div className="contentSection">
        <div className="titleContainer">
          <div className="viewItem">
            <p className="specialWelcome">Welcome</p>
            <p className="controlSentence">Control your home from here!</p>
          </div>
          
          <div className="viewImg2">
            <img src="./Images/background_new.png" alt="background_new" ></img>
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
          </table>

          <div className="pagination">
            <button
              onClick={() => setCurrentPage(currentPage - 1)}
              disabled={currentPage === 1}
            >
              &#9665;
            </button>
            <span>{currentPage}</span>
            <button
              onClick={() => setCurrentPage(currentPage + 1)}
            >
              &#9655;
            </button>
          </div>
          
        </div>
      </div>
    </div>
  );
};

export default History;
