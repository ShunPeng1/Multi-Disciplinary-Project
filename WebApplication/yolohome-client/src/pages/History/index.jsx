import "./History.css";
import { useState, useEffect } from "react";
import Header from "../../components/Header/Header";
import FetchRequest from "../../components/api/api";

const History = (props) => {
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(4);
  const [historyData, setHistoryData] = useState([]);
  const [usernameFilter, setUsernameFilter] = useState('');
  const [activityFilter, setActivityFilter] = useState('');
  const [timestampFilter, setTimestampFilter] = useState('');
  const [sortColumn, setSortColumn] = useState(null);
  const [sortDirection, setSortDirection] = useState('asc');

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

  const handleSort = (column) => {
    if (sortColumn === column) {
      // If already sorted by this column, reverse sort direction
      setSortDirection(sortDirection === 'asc' ? 'desc' : 'asc');
    } else {
      // If sorting a new column, set it as the sort column and default to ascending order
      setSortColumn(column);
      setSortDirection('asc');
    }
  };

  const sortedData = () => {
    if (!sortColumn) return historyData;
    
    return [...historyData].sort((a, b) => {
      if (sortDirection === 'asc') {
        return a[sortColumn] > b[sortColumn] ? 1 : -1;
      } else {
        return a[sortColumn] < b[sortColumn] ? 1 : -1;
      }
    });
  };

  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;

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
            <label htmlFor="username-filter" className="filter-label">Filter by username:</label>
            <input id="username-filter" type="text" value={usernameFilter} onChange={(e) => setUsernameFilter(e.target.value)} className="filter-input" />
            <label htmlFor="activity-filter" className="filter-label">Filter by activity:</label>
            <input id="activity-filter" type="text" value={activityFilter} onChange={(e) => setActivityFilter(e.target.value)} className="filter-input" />
            <label htmlFor="timestamp-filter" className="filter-label">Filter by timestamp:</label>
            <input id="timestamp-filter" type="text" value={timestampFilter} onChange={(e) => setTimestampFilter(e.target.value)} className="filter-input" />
          </div>
          <table className="table">
            <thead>
            <tr>
              <th onClick={() => handleSort('UserName')}>
                Username {sortColumn === 'UserName' && (sortDirection === 'asc' ? '▲' : '▼')}
              </th>
              <th onClick={() => handleSort('Activity')}>
                Activity {sortColumn === 'Activity' && (sortDirection === 'asc' ? '▲' : '▼')}
              </th>
              <th onClick={() => handleSort('Device')}>
                Device {sortColumn === 'Device' && (sortDirection === 'asc' ? '▲' : '▼')}
              </th>
              <th onClick={() => handleSort('TimeStamp')}>
                Time {sortColumn === 'TimeStamp' && (sortDirection === 'asc' ? '▲' : '▼')}
              </th>
            </tr>
            </thead>
            <tbody>
              {sortedData()
                .filter(item => 
                  item.UserName.toLowerCase().includes(usernameFilter.toLowerCase()) &&
                  item.Activity.toLowerCase().includes(activityFilter.toLowerCase()) &&
                  new Date(item.TimeStamp).toLocaleString().toLowerCase().includes(timestampFilter.toLowerCase())
                )
                .slice(indexOfFirstItem, indexOfLastItem)
                .map((item, index) => (
                  <tr key={index}>
                    <td>{item.UserName}</td>
                    <td>{item.Activity}</td>
                    <td>
                      {(() => {
                        const words = item.Activity.split(" ");
                        if (words.includes("Light")) {
                          if (words.includes("Kitchen")) {
                            return words.slice(-2).join(" ");
                          }
                          return words.slice(-3).join(" ");
                        } else {
                          return words.pop();
                        }
                      })()}
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
