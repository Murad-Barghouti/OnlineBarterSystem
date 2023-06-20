import React, { useState } from "react";
import styles from "./Navbar.module.css";
import { NavLink } from "react-router-dom";
import { FaUserCircle, FaSearch } from "react-icons/fa";
import logo from "../../assets/obslogo.png";

const Navbar = () => {

    const [currentUserInfo, setCurrentUserInfo] = useState(JSON.parse(localStorage.getItem('currentUserInfo')));

    return (
            <div className={styles.header}>
                <div className={styles.leftHeader}>
                    <NavLink to="/"><img className={styles.logo} src={logo} alt="Online barter system" /></NavLink>
                </div>
                <div className={styles.rightHeader}>
                <NavLink to="/search">
                    <span className={styles.headerItem}>
                        <span style={{ fontSize: 25, position: 'relative', top: 5 }}>
                        <FaSearch />
                        </span>
                    </span>
                </NavLink>
                <NavLink to="/profile/mybarters">
                    <span className={styles.headerItem}>
                        <span style={{ padding: 10, fontSize: 27, position: 'relative', top: 7 }}>
                            <FaUserCircle />
                        </span>
                        {currentUserInfo.firstName} {currentUserInfo.lastName}
                    </span>
                </NavLink>
                </div>
            </div>   
    );
};

export default Navbar;