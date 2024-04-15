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

    useEffect(() => {
      fetchData();
      const intervalId = setInterval(fetchData, 5000); // Fetch data every 5 seconds

      return () => {
        clearInterval(intervalId); // Clear interval on component unmount
      };
    }, []);

    const fetchData = () => {
      FetchRequest('api/ActivityLogApi/GetAll', 'GET', {
        Username: username,
       
      }, successCallback, errorCallback);
    };

    const successCallback = (data) => {
      console.log('Success:', data);
      setHistoryData(data);
    }   

    const errorCallback = (error) => {
      console.error('Error:', error);
    }
    
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
                <tr>
                  <th>Username</th>
                  <th>Activity</th>
                  <th>Time</th>
                </tr>
              </thead>
              <tbody>
                {historyData && [...historyData].reverse().slice(indexOfFirstItem, indexOfLastItem).map((item, index) => (
                  <tr key={index}>
                    <td>{item.Activity}</td>
                    <td>{item.UserName}</td>
                    <td>
                      {new Date(item.TimeStamp).toLocaleDateString()} {new Date(item.TimeStamp).toLocaleTimeString()}
                    </td>
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
              <button onClick={() => setCurrentPage(currentPage + 1)}
              disabled={currentPage >= Math.ceil(historyData.length / itemsPerPage)}>
                &#9655;
              </button>
            </div>
          </div>
        </div>
      </div>
    );
  };

  export default History;
