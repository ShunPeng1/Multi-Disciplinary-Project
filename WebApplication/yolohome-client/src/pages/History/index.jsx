  import "./History.css";
  import { useState, useEffect } from "react";
  import Header from "../../components/Header/Header";

  const History = (props) => {
    const [addModal, setAddModal] = useState(false);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [addValue, setAddValue] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage, setItemsPerPage] = useState(5);
    const [historyData, setHistoryData] = useState([]);

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;

    // useEffect(() => {
    //   // Fetch data from your database here
    //   // Example fetch:
    //   fetch("your_database_endpoint")
    //     .then((response) => response.json())
    //     .then((data) => setHistoryData(data))
    //     .catch((error) => console.error("Error fetching data:", error));
    // }, []);

    useEffect(() => {
      const mockHistoryData = [
        {
          device: "Light",
          room: "House",
          description: "On",
          time: "2024-04-06 13:00:00"
        },
        {
          device: "Door",
          room: "House",
          description: "Close",
          time: "2024-04-06 13:30:00"
        },
      ];
      setHistoryData(mockHistoryData);
    }, []);
    

    return (
      <div className="buypaper">
        <div className="contentSection">
          <div className="import-header">
            <Header />
          </div>
          <div className="buyLog">
            <div className="title">
              <span className="boxTitle">History</span>
            </div>

            <table className="table">
              <thead>
                <tr className="buyHeader">
                  <th>No.</th>
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
