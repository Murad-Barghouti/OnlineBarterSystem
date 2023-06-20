import React, { useState, useEffect } from "react";
import styles from "./BarterRequests.module.css";
import { NavLink, useNavigate } from "react-router-dom";
import { FaUserCircle } from "react-icons/fa";
import { IoIosCloseCircle, IoIosCheckmarkCircle } from 'react-icons/io';
import arrows from "../../assets/arrows.jpg";
import Navbar from "../../components/Navbar/Navbar";
import Profile from "../../components/Profile/Profile";

const BarterRequests = () => {

    const [requests, setRequests] = useState([]);
    const [currentUserInfo, setCurrentUserInfo] = useState(JSON.parse(localStorage.getItem('currentUserInfo')));
    const baseURL = "https://localhost:7073/api";
    const navigate = useNavigate();

    async function fetchBarterRequests() {
        try {
            const response = await fetch(baseURL + "/Barter");
            const barters = await response.json();
            console.log(barters);
            setRequests(barters);
        } catch (err) {
            console.log(err.message);
        }
    }

    const handleAccept = async (barterId) => {
        try {
            const response = await fetch(baseURL + "/Barter/approveBarter/" + barterId, {
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

            fetchBarterRequests();
        } catch (err) {
            console.log(err.message);
        } finally {
        }
    }


    const handleReject = async (barterId) => {
        try {
            const response = await fetch(baseURL + "/Barter/rejectBarter/" + barterId, {
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

            fetchBarterRequests();
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
            fetchBarterRequests();
        }
    }, []);

    return (
        <div className={styles.container}>
            <div className={styles.overlay}>
            <Navbar/>
                <div className={styles.main}>
                    <Profile/>
                    <div className={styles.tabBar}>
                        <NavLink to="/profile/mybarters">my barters</NavLink>
                        <NavLink to="/profile/participatingbarters">participating barters</NavLink>
                        <NavLink to="/profile/requests" className={styles.active}>barter requests</NavLink>
                    </div>
                    <div className={styles.list}>
                        {requests
                            .filter(item => (item.initiatorId === currentUserInfo.id) && item.barterState.name === "Pending Approval")
                            .map((item) => {
                                const { id, receiveType, giveType, description, joiner } = item;
                                return (
                                    <div key={id} className={styles.item}>
                                    <div className={styles.infoContainer}>
                                        <div className={styles.info}>
                                            <div className={styles.userInfo}>
                                                <FaUserCircle style={{ fontSize: 20, position: 'relative', top: 4 }} /> <b className={styles.fullname}>{joiner.firstName} {joiner.lastName}</b> requested to participate in your following barter:
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
                                            <IoIosCheckmarkCircle style={{ fontSize: 35, color: '#0f950d' }} onClick={(e)=>handleAccept(id)} />
                                            <IoIosCloseCircle style={{ fontSize: 35, color: '#c00e0e', marginLeft: 15 }} onClick={(e) => handleReject(id)} />
                                    </div>
                                </div>
                            );
                        }) }
                    </div>
                </div>
            </div>
        </div>
    );
};

export default BarterRequests;