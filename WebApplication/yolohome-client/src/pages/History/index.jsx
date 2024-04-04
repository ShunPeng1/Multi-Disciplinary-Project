import "./History.css";
// import {Header, Footer, PaperLog, AddPaperModal, Modal} from "../../components";
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
//   const currentItems = paperHistoryItems.slice(indexOfFirstItem, indexOfLastItem);

//   useEffect(() => {
//     setCurrentPage(1); // Reset current page when items change
//   }, [paperHistoryItems]);

//   const handleCloseModal = () => {
//     setAddModal(false);
//   };

//   const handleConfirmModal = (value) => {
//     setAddValue(Number(value));
//     setAddModal(false);
//     setIsModalOpen(true);
//   }  

//   const handleCloseModalOpen = () => {
//     setIsModalOpen(false);
//   };

//   const handleConfirmModalOpen = () => {
//     updatePageNumber(prev => prev + addValue);
//     const now = new Date();
//     const hours = now.getHours();
//     const minutes = now.getMinutes();
//     const yyyy = now.getFullYear();
//     let mm = now.getMonth() + 1; // Months start at 0!
//     let dd = now.getDate();

//     if (dd < 10) dd = '0' + dd;
//     if (mm < 10) mm = '0' + mm;

//     const formattedToday = dd + '/' + mm + '/' + yyyy;
//     const newObj = {
//       quantity: addValue, cost: (addValue*1000).toLocaleString("de-DE") + " VND", buyStatus: "Đang thanh toán", time: hours + ":" + minutes + ", " + formattedToday
//     }

//     const newArray = paperHistoryItems;
//     newArray.unshift(newObj);
//     updatePaperHistoryItems(newArray);
//     setIsModalOpen(false);
//   }  


  return (

    <div className="buypaper">
      {/* <Header/> */}
        <div className="contentSection">
            <div className="titleContainer">
              <div className="viewItem">
                {/* a big word say welcome, center it */}
                <p className="specialWelcome">Welcome</p>
                <p className="controlSentence">Control your home from here!</p>
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
              {/* <tbody>
                {currentItems.map((buyPaperInfo, i) => (
                  <PaperLog key={i} id={i + indexOfFirstItem} buyInfo={buyPaperInfo} />
                ))}
              </tbody> */}
            </table>

            {/* Pagination controls */}
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
                // disabled={indexOfLastItem >= paperHistoryItems.length}
              >
                &#9655;
              </button>
            </div>
            
          </div>
          {/* {addModal && 
          <AddPaperModal onConfirm={handleConfirmModal} onClose={handleCloseModal}/>}

          {isModalOpen && 
          <Modal onConfirm={handleConfirmModalOpen} onClose={handleCloseModalOpen} 
          modalTitle={`Xác nhận huỷ đăng ký`} modalMessage={`Khi bấm "Đồng ý", hệ thống sẽ tự động trừ tiền của bạn trên BKPay.`}/>} */}
        </div>
      {/* <Footer/> */}
    </div>
  );
};

export default History;
