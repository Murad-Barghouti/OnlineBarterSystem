import React, { useState } from "react";
import styles from "./Profile.module.css";
import { NavLink } from "react-router-dom";
import { MdLocationOn, MdEdit } from "react-icons/md";
import profilepic from "../../assets/profilepic.png"

const Profile = () => {

    const [currentUserInfo, setCurrentUserInfo] = useState(JSON.parse(localStorage.getItem('currentUserInfo')));
    //console.log(currentUserInfo)

    return (
        <div className={styles.profile}>
            <img className={styles.profilepic} src={profilepic} alt="user profile pic" />
            <div className={styles.profileInfo}>
                <p className={styles.fullname}>{currentUserInfo.firstName} {currentUserInfo.lastName} <NavLink to="/account"><MdEdit style={{ fontSize: 23, color: '#0A0F0D', position: 'relative', top: 5 }} /></NavLink></p>
                <p className={styles.email}>{currentUserInfo.email}</p>
                <p className={styles.city}><span style={{ paddingRight: 3, fontSize: 22, position: 'relative', top: 5 }}><MdLocationOn /></span>{currentUserInfo.city.name} </p>
            </div>
        </div>
    );
};

export default Profile ;