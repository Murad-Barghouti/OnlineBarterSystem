import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import styles from "./Homepage.module.css";
import logo from "../../assets/obslogo.png";

const Homepage = () => {
    return (
        <div className={styles.container}>
            <div className={styles.overlay}>
                {/*<img className={styles.logo} src={logo} alt="Yardnirvana" />*/}
                <div className={styles.header}>
                    <div className={styles.leftHeader}>
                        <NavLink to="/"><img className={styles.logo} src={logo} alt="Online barter system" /></NavLink>
                    </div>
                    <div className={styles.rightHeader}>
                        <NavLink to="/signin"><span className={styles.headerItem}>Sign in</span></NavLink>
                        <NavLink to="/signup"><span className={styles.headerItem}>Sign up</span></NavLink>
                        <NavLink to="/contactus"><span className={styles.headerItem}>Contact us</span></NavLink>
                    </div>
                </div>
                <div className={styles.main}>
                    <h1>Welcome to the Online Barter System!</h1>
                    <h3>Join OBS today to trade <span>goods, services, precious metals, and currencies!</span> in exchange for equal value with other users!</h3>
                    <h3>Not only trade, take advantage of the <span>donation</span> feature!</h3>
                    <NavLink to="/signup"><button>Join us today!</button></NavLink>
                </div>
                <div className={styles.circle1}></div>
                <div className={styles.circle2}></div>
                <div className={styles.circle3}></div>
            </div>
        </div>
    );
};

export default Homepage;