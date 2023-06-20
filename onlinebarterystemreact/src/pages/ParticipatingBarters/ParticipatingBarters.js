import React, { useState, useEffect } from "react";
import styles from "./ParticipatingBarters.module.css";
import { NavLink, useNavigate } from "react-router-dom";
import { FaUserCircle } from "react-icons/fa";
import arrows from "../../assets/arrows.jpg";
import Navbar from "../../components/Navbar/Navbar";
import Profile from "../../components/Profile/Profile";

const ParticipatingBarters = () => {

    const [participatingBarters, setParticipatingBarters] = useState([]);
    const [currentUserInfo, setCurrentUserInfo] = useState(JSON.parse(localStorage.getItem('currentUserInfo')));
    const baseURL = "https://localhost:7073/api";
    const navigate = useNavigate();

    async function fetchParticipatingBarters() {
        try {
            const response = await fetch(baseURL + "/Barter");
            const barters = await response.json();
            console.log(barters);
            setParticipatingBarters(barters);
        } catch (err) {
            console.log(err.message);
        }
    }

    const handleCancel = async (barterId) => {
        try {
            const response = await fetch(baseURL + "/Barter/leaveBarter/" + barterId, {
                method: "GET",
                headers: {
                    Accept: "application/json",
                },
            });

            if (!response.ok) {
                throw new Error(
                    `Error! status: ${response.status} message ${response.message}`
                );
            }

            const result = await response.json();

            fetchParticipatingBarters();
        } catch (err) {
            console.log(err.message);
        } finally {
        }
    }

    useEffect(() => {
        var currentUsername = localStorage.getItem('currentUsername');
        if (currentUsername === '') {
            navigate("/signin")
        } else {
            fetchParticipatingBarters();               
        }
    },[]);

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
                        {participatingBarters
                            .filter(item => (item.joinerId === currentUserInfo.id) && item.barterState.name === "Pending Approval")
                            .map((item) => {
                            const { id, receiveType, giveType, description, initiator} = item;
                            return (
                                <div key={id} className={styles.item}>
                                    <div className={styles.infoContainer}>
                                        <div className={styles.info}>
                                            <div className={styles.userInfo}>
                                                Posted by <FaUserCircle style={{ fontSize: 20, position: 'relative', top: 4 }} /> <b className={styles.fullname}>{initiator.firstName} {initiator.lastName}</b>
                                            </div>
                                            <div className={styles.details}>
                                                <p><b>Want to trade: </b> {giveType.name}</p>
                                                <img className={styles.arrows} src={arrows} alt="arrows" />
                                                <p><b>For: </b> {receiveType.name}</p>
                                            </div>
                                            <div className={styles.description}>
                                                <p><b>Description: </b> {description}</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div className={styles.buttons}>
                                        <button onClick={(e)=>handleCancel(id)}>CANCEL</button>
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