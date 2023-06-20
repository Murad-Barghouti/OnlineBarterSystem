import React, { useState, } from "react";
import styles from "./AccountInfo.module.css";
import { NavLink,  } from "react-router-dom";
import Navbar from "../../components/Navbar/Navbar";
import Profile from "../../components/Profile/Profile";

const AccountInfo = () => {

    const [requests, setRequests] = useState([]);
    const [currentUserInfo, setCurrentUserInfo] = useState(JSON.parse(localStorage.getItem('currentUserInfo')));
    const baseURL = "https://localhost:7073/api";


    return (
        <div className={styles.container}>
            <div className={styles.overlay}>
                <Navbar />
                <div className={styles.main}>
                    <Profile />
                    
                    <div className={styles.infoBox}>
                        <p><b>First name:</b>{currentUserInfo.firstName}</p>
                        <p><b>Last name:</b>{currentUserInfo.lastName}</p>
                        <p><b>City:</b>{currentUserInfo.city.name}</p>
                        <p><b>Phone number:</b>{currentUserInfo.phoneNumber}</p>
                        <p><b>Username:</b>{currentUserInfo.userName}</p>
                        <p><b>Email:</b>{currentUserInfo.email}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AccountInfo;