import React, { useState, useEffect } from "react";
import styles from "./MyBarters.module.css";
import { NavLink, useNavigate } from "react-router-dom";
import { MdEdit, MdDelete } from "react-icons/md";
import { HiPlus } from 'react-icons/hi';
import arrows from "../../assets/arrows.jpg";
import Navbar from "../../components/Navbar/Navbar";
import Profile from "../../components/Profile/Profile";

const MyBarters = () => {    
    const [myBarters, setMyBarters] = useState([]);
    const [currentUserInfo, setCurrentUserInfo] = useState({});
    const baseURL = "https://localhost:7073/api";
    const navigate = useNavigate();


    async function fetchMyBarters() {
        
        try {
            const response2 = await fetch(baseURL + "/Barter");
            const barters = await response2.json();

            setMyBarters(barters);  
        } catch (err){
            console.log(err.message);
        }
    }

    const handleEdit = (barter) => {
        localStorage.setItem('editBarter', JSON.stringify(barter));
        navigate("/editbarter/"+barter.id)        
    }

    const handleDelete = async (barterId) => {
        try {
            const response = await fetch(baseURL + "/Barter/" + barterId, {
                method: "DELETE",
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

            fetchMyBarters();
        } catch (err) {
            console.log(err.message);
        } finally {
        }
    }

    useEffect(() => {
        var currentUsername = localStorage.getItem('currentUsername');
        if (currentUsername === '') {
            navigate("/signin")
        }


        fetch(baseURL + "/Account/" + currentUsername)
            .then((response) => response.json())
            .then((userInfo) => {
                console.log(userInfo);
                localStorage.setItem('currentUserInfo', JSON.stringify(userInfo));
                setCurrentUserInfo(userInfo);
                fetchMyBarters();
            })
            .catch((err) => {
                console.log(err.message);
            });     

    }, []);

    return (
        <div className={styles.container}>
            <div className={styles.overlay}>
                <Navbar />
                <div className={styles.main}>
                    <Profile/>
                    <div className={styles.tabBar}>
                        <NavLink to="/profile/mybarters" className={styles.active}>my barters</NavLink>
                        <NavLink to="/profile/participatingbarters">participating barters</NavLink>
                        <NavLink to="/profile/requests">barter requests</NavLink>
                    </div>
                    
                    <div className={styles.list}>
                        <div className={styles.addBarter}>
                            <NavLink to="/addbarter">
                                <button>
                                    <HiPlus style={{ fontSize: 25, position: 'relative', paddingRight: 5 , bottom:1}} />
                                    <span>Add a Barter</span>
                                </button>
                            </NavLink>
                        </div>
                        {
                            myBarters
                            .filter(item => item.initiatorId === currentUserInfo.id)
                            .map((item) => {
                            const { id, receiveType ,giveType, description } = item;
                        return (
                        <div className={styles.item} key={id}>
                                    <div className={styles.infoContainer}>
                                        <div className={styles.info}>
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
                                    <MdEdit style={{ fontSize: 28, color: '#0A0F0D' }} onClick={(e) => handleEdit(item)} />
                                    <MdDelete style={{ fontSize: 28, marginLeft: 15, cursor: 'pointer' }} onClick={(e)=>handleDelete(id)} />
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

export default MyBarters;