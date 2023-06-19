import React, { useState } from "react";
import styles from "./ParticipatingBarters.module.css";
import { NavLink } from "react-router-dom";
import { FaUserCircle } from "react-icons/fa";
import arrows from "../../assets/arrows.jpg";
import Navbar from "../../components/Navbar/Navbar";
import Profile from "../../components/Profile/Profile";

const ParticipatingBarters = () => {

    const participatingBarters = [
        { username: "jane doe", barter: { wantToTrade: "levis jeans", wantInReturn: "600TL" } },
        { username: "sara lane", barter: { wantToTrade: "gold equipment", wantInReturn: "signed sports jersey" } },
        { username: "matt damon", barter: { wantToTrade: "Cooked meal prep", wantInReturn: "copywriting" } },
    ]

    return (
        <div className={styles.container}>
            <div className={styles.overlay}>
                <Navbar />
                <div className={styles.main}>
                    <Profile />
                    <div className={styles.tabBar}>
                        <NavLink to="/profile/mybarters">my barters</NavLink>
                        <NavLink to="/profile/participatingbarters" className={styles.active}>participating barters</NavLink>
                        <NavLink to="/profile/requests">barter requests</NavLink>
                    </div>
                    <div className={styles.list}>
                        {participatingBarters.map((item) => {
                            const { username, barter } = item;
                            return (
                                <div className={styles.item}>
                                    <div className={styles.infoContainer}>
                                        <div className={styles.info}>
                                            <div className={styles.userInfo}>
                                                Posted by <FaUserCircle style={{ fontSize: 20, position: 'relative', top: 4 }} /> <b className={styles.fullname}>{username}</b>
                                            </div>
                                            <div className={styles.details}>
                                                <p><b>Want to trade: </b> {barter.wantToTrade}</p>
                                                <img className={styles.arrows} src={arrows} alt="arrows" />
                                                <p><b>For: </b> {barter.wantInReturn}</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div className={styles.buttons}>
                                        <button>CANCEL</button>
                                    </div>
                                </div>
                            );
                        })}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default ParticipatingBarters;