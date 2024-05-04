  import "./History.css";
  import { useState, useEffect } from "react";
  import Header from "../../components/Header/Header";
  import FetchRequest from "../../components/api/api";

  const History = (props) => {
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage, setItemsPerPage] = useState(4);
    const [historyData, setHistoryData] = useState([]);
    const [filter, setFilter] = useState('');
    const [usernameSort, setUsernameSort] = useState(true);
    const [activitySort, setActivitySort] = useState(true);
    const [deviceSort, setDeviceSort] = useState(true);
    const [timeSort, setTimeSort] = useState(true);

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;


    const username = localStorage.getItem("username");

    const sortData = (data, column) => {
      return data.sort((a, b) => {
        let comparison = 0;
        if (a[column] > b[column]) {
          comparison = 1;
        } else if (a[column] < b[column]) {
          comparison = -1;
        }
        return comparison;
      });
    };

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
      const sortedData = sortData(data, 'UserName');
      if (!usernameSort) {
        sortedData.reverse();
      }
      setHistoryData(sortedData);
    }    
    
    const errorCallback = (error) => {
      console.error('Error:', error);
    }

    const handleSort = (column, setSort) => {
      setSort(prev => !prev);
      const sortedData = sortData([...historyData], column);
      if (!setSort) {
        sortedData.reverse();
      }
      setHistoryData(sortedData);
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

            <div className="filter-container">
              <label htmlFor="filter" className="filter-label">Filter by device:</label>
              <input id="filter" type="text" value={filter} onChange={(e) => setFilter(e.target.value)} className="filter-input" />
            </div>
            <table className="table">
              <thead>
              <tr>
                <th onClick={() => handleSort('UserName', setUsernameSort)}>Username</th>
                <th onClick={() => handleSort('Activity', setActivitySort)}>Activity</th>
                <th onClick={() => handleSort('Device', setDeviceSort)}>Device</th>
                <th onClick={() => handleSort('TimeStamp', setTimeSort)}>Time</th>
              </tr>
              </thead>
              <tbody>
                {historyData && [...historyData].reverse().filter(item => item.Activity.split(" ").slice(-3).join(" ").toLowerCase().includes(filter.toLowerCase())).slice(indexOfFirstItem, indexOfLastItem).map((item, index) => (
                  <tr key={index}>
                    <td>{item.UserName}</td>
                    <td>{item.Activity}</td>
                    <td>
                      {
                        (() => {
                          const words = item.Activity.split(" ");
                          if (words.includes("Light")) {
                            if (words.includes("Kitchen")) {
                              return words.slice(-2).join(" ");
                            }
                            return words.slice(-3).join(" ");
                          } else {
                            return words.pop();
                          }
                        })()
                      }
                    </td>
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
