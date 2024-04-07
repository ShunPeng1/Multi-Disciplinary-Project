  import "./History.css";
  import { useState, useEffect } from "react";
  import Header from "../../components/Header/Header";
  import FetchRequest from "../../components/api/api";

  const History = (props) => {
    const [addModal, setAddModal] = useState(false);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [addValue, setAddValue] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage, setItemsPerPage] = useState(5);
    const [historyData, setHistoryData] = useState([]);

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;

    const username = localStorage.getItem("username");
    
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
          description: "On",
          time: "2024-04-06 13:00:00"
        },
        {
          device: "Door",
          description: "Close",
          time: "2024-04-06 13:30:00"
        },
      ];
      setHistoryData(mockHistoryData);
    }, []);

    // useEffect(() => {
    //   fetchData();
    //   const intervalId = setInterval(fetchData, 5000); // Fetch data every 5 seconds

    //   return () => {
    //     clearInterval(intervalId); // Clear interval on component unmount
    //   };
    // }, []);

    // const fetchData = () => {
    //   FetchRequest('api/ActivityLogApi/GetAll', 'GET', {
    //     Username: username,
       
    //   }, successCallback, errorCallback);
    // };

    // const a = "";

    // const successCallback = (data) => {
    //   console.log('Success:', data);
    //   setHistoryData(data.Response);
    //   a = data.Response;
    // }   

    // const errorCallback = (error) => {
    //   console.error('Error:', error);
    // }
    

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
                  <th>Device</th>
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
                    <td>{item.device}</td> {/* Change to item.UserName */}
                    <td>{item.description}</td> {/* Change to item.Activity */}
                    <td>{item.time}</td> {/* Change to item.TimeStamp */}
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
