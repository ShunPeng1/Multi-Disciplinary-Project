import "./Dashboard.css";
import React from 'react';
//import {Header, Footer} from "../../Components";
//import PrintingLog from "../../Components/PrintingLog/PrintingLog";
import OnOffButton from "../../components/Button"

const Dashboard = (props) => {
  const { numberOfPages, updatePage } = props;
  const {printTimes, updatePrintTimes} = props;
  const { printInfoItems } = props;
  const { updatePrintInfoItems } = props; 
  // const pendingPrints = printInfoItems.filter(item => item.printStatus === "Đang chờ");
  return (
    <div className="home">
      
      {/* <Header/> */}
        <div className="contentSection">
          <div className="welcome">
            <p className="specialWelcome">Chào mừng trở lại, Khoa</p>
            <p className="goodluck">Chúc bạn một ngày tốt lành</p>
          </div>

          <div className="importantView">
            <div className="viewItem">
              <img src="./Images/impPrinter.png" alt="impPrinter" className="viewImg"></img>
              <div className="data">
                <p className="value">{printTimes}</p>
                <p className="description">Số lần in</p>
              </div>
            </div>

            <div className="viewItem">
              <img src="./Images/file.png" alt="file" className="viewImg"></img>
              <div className="data">
                <p className="value">{numberOfPages}</p>
                <p className="description">Số giấy còn lại</p>
              </div>
            </div>
            
          </div>

          <div className="waitingLog">
            <p className="waitP">Đang chờ</p>
            
            {/* {pendingPrints.map((printInfo, i) => (
                <PrintingLog
                  key={i}
                  printItems={printInfoItems}
                  printTimes={printTimes}
                  numberOfPages={numberOfPages}
                  updatePage={updatePage}
                  updatePrintTimes={updatePrintTimes}
                  printingInfo={printInfo}
                  updatePrintInfoItems={updatePrintInfoItems}
                />
              ))} */}
            
          </div>
          <div class = "myButton">
              <OnOffButton />
          </div>
        </div>

        {/* <Footer/> */}
     
    </div>
  );
};

export default Dashboard;
