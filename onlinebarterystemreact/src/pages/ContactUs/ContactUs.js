import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import styles from "./ContactUs.module.css";
import logo from "../../assets/obslogo.png";

const ContactUs = () => {
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
                    <h1>Get in touch with the OBS team!</h1>
                    <h3>Contact us regarding any issues, reviews and suggestions using the contact information below:</h3>
                    <h3><span>+90 345 726 254</span></h3>
                    <h3><span>support@obs.com.tr</span></h3>
                </div>
                <div className={styles.circle1}></div>
                <div className={styles.circle2}></div>
                <div className={styles.circle3}></div>
            </div>
        </div>
    );
};

export default ContactUs;