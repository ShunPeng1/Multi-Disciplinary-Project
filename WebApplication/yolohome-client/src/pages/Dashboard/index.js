// Dashboard/index.js
import { useState, useEffect } from "react";
import { ToastContainer, toast } from 'react-toastify';
import mountain from "../../assets/mountain.jpg";
import water from "../../assets/water.jpg";
import React from 'react';
import './Dashboard.css';

function Dashboard() {
    const [humis, setHumi] = useState([]);
    const [tempers, setTemper] = useState([]);
    const [ledBtn, setLed] = useState(0);
    const clickLed = async () => {
        let message = {
            content: "",
            type: "1"
        }
        if (ledBtn == 0) {
            message.content = "Bật đèn"
        }
        else {
            message.content = "Tắt đèn"
        }
        setLed(!ledBtn);
    }
    const [fanBtn, setFan] = useState(0);
    const clickFan = async () => {
        let message = {
            content: "",
            type: "1"
        }
        if (fanBtn == 0) {
            message.content = "Bật quạt"
        }
        else {
            message.content = "Tắt quạt"
        }
        setFan(!fanBtn);
    }
    const [doorBtn, setDoor] = useState(0);
    const clickDoor = async () => {
        let message = {
            content: "",
            type: "1"
        }
        if (doorBtn == 0) {
            message.content = "Mở cửa"
        }
        else {
            message.content = "Đóng cửa"
        }
        setDoor(!doorBtn);
    }
    const listImage = ["https://sbshouse.vn/wp-content/uploads/2021/06/bi%E1%BB%87t-th%E1%BB%B1-2-t%E1%BA%A7ng-%C4%91%E1%BA%B9p-4.jpg",
        "https://i.pinimg.com/564x/39/1f/5a/391f5ab7fc3cbc8ea4b8a7b0a094aac5.jpg",
        "https://i.pinimg.com/564x/4e/2b/fb/4e2bfb3e366780228be3e7d78b345427.jpg",
        "https://i.pinimg.com/564x/1b/4f/dd/1b4fdd6bc731b6f270c2f003507b4574.jpg",
        "https://i.pinimg.com/564x/3e/4a/6b/3e4a6b9249d6b415415e0ece0e60b315.jpg"];

    const [img, setImg] = useState(0);
    return (
        <div className="dashBoard">
            <div className="dashBoard__left">
                <ul className="dashBoard__left-heading">
                    <li className="heading__item" style={{ color: img == 0 ? '#b9bccc' : '#394374' }}>
                        Living room
                    </li>
                    <li className="heading__item" style={{ color: img == 1 ? '#b9bccc' : '#394374' }}>
                        Dinning room
                    </li>
                    <li className="heading__item" style={{ color: img == 2 ? '#b9bccc' : '#394374' }}>
                        Bed room
                    </li>
                    <li className="heading__item" style={{ color: img == 3 ? '#b9bccc' : '#394374' }}>
                        Bath room
                    </li>
                    <li className="heading__item" style={{ color: img == 4 ? '#b9bccc' : '#394374' }}>
                        Backyard
                    </li>
                </ul>
                <div className="dashBoard__left-content">
                    <div className="content__img">
                        <img className="image" alt="" src={listImage[img]} />
                        <div className="left-right">
                            <button className="left" onClick={() => setImg(img > 0 ? img - 1 : 4)}>
                                <i className="left-icon fa-solid fa-chevron-left"></i>
                            </button>
                            <button className="right" onClick={() => setImg(img < 4 ? img + 1 : 0)}>
                                <i className="right-icon fa-solid fa-chevron-right"></i>
                            </button>
                        </div>
                    </div>
                    <div className="content__btn">
                        <button className="btn-led btn" style={{ backgroundColor: ledBtn && '#1babfc' }} >
                            <span className='span1'></span>
                            <span className='span2'></span>
                            <span className='span3'></span>
                            <span className='span4'></span>
                            <div className="led-border">
                                <i className="led-btn-icon fa-solid fa-lightbulb"></i>
                            </div>
                            <div className="led-text">
                                <span className="led-infor">LED</span>
                                <span className="led-connect"> {!ledBtn ? "Not Connected" : "Connected"}</span>
                            </div>
                            <label class="switch">
                                <input className="btn-switch" type="checkbox" onClick={clickLed} />
                                <span class="slider">
                                    <ul className="switch__select">
                                        <li className="switch__select-item">Off</li>
                                        <li className="switch__select-item">On</li>
                                    </ul>
                                </span>
                            </label>
                        </button>

                        <button className="btn-fan btn" style={{ backgroundColor: fanBtn && '#1babfc' }} >
                            <span className='span1'></span>
                            <span className='span2'></span>
                            <span className='span3'></span>
                            <span className='span4'></span>
                            <div className="fan-border">
                                <i className="fan-btn-icon fa-solid fa-fan"></i>
                            </div>
                            <div className="fan-text">
                                <span className="fan-infor">FAN</span>
                                <span className="fan-connect"> {!fanBtn ? "Not Connected" : "Connected"}</span>
                            </div>
                            <label class="switch">
                                <input className="btn-switch" type="checkbox" onClick={clickFan} />
                                <span class="slider">
                                    <ul className="switch__select">
                                        <li className="switch__select-item">Off</li>
                                        <li className="switch__select-item">On</li>
                                    </ul>
                                </span>
                            </label>
                        </button>

                        <button className="btn-door btn" style={{ backgroundColor: doorBtn && '#1babfc' }} >
                            <span className='span1'></span>
                            <span className='span2'></span>
                            <span className='span3'></span>
                            <span className='span4'></span>
                            <div className="door-border">
                                <i className="door-btn-icon fa-solid fa-door-closed"></i>
                            </div>
                            <div className="door-text">
                                <span className="door-infor">DOOR</span>
                                <span className="door-connect"> {!doorBtn ? "Not Connected" : "Connected"}</span>
                            </div>
                            <label class="switch">
                                <input className="btn-switch" type="checkbox" onClick={clickDoor} checked={doorBtn} />
                                <span class="slider">
                                    <ul className="switch__select">
                                        <li className="switch__select-item">Off</li>
                                        <li className="switch__select-item">On</li>
                                    </ul>
                                </span>
                            </label>
                        </button>
                    </div>
                </div>
                <ToastContainer
                    position="top-right"
                    autoClose={2000}
                    hideProgressBar={false}
                    newestOnTop={false}
                    closeOnClick
                    rtl={false}
                    pauseOnFocusLoss
                    draggable
                    pauseOnHover
                    theme="light"
                />
            </div>
            <div className="dashBoard__right">
                <div className="dashBoard__right-temper">
                    <h3 className="temper__infor">It's hot</h3>
                    <h3 className="temper__text"> {

                        tempers.length > 0 ? tempers : 0
                    }°C</h3>
                    <img src={mountain} className="temper__scene" />
                </div>
                <div className="dashBoard__right-humi">
                    <h3 className="temper__infor">High humidity</h3>
                    <h3 className="temper__text"> {
                        humis.length > 0 ? humis : 0
                    }%</h3>
                    <img src={water} className="temper__scene" />
                </div>





            </div>
        </div>

    );
}

export default Dashboard;
