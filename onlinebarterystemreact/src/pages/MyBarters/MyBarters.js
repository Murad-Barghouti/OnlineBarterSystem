import React, { useState, useEffect } from "react";
import styles from "./MyBarters.module.css";
import { NavLink } from "react-router-dom";
import { MdEdit, MdDelete } from "react-icons/md";
import { HiPlus } from 'react-icons/hi';
import arrows from "../../assets/arrows.jpg";
import Navbar from "../../components/Navbar/Navbar";
import Profile from "../../components/Profile/Profile";

const MyBarters = () => {

    /*const myBarters = [
        { id: 1, wantToTrade: "levis jeans", wantInReturn: "600TL"  },
        { id: 2, wantToTrade: "gold equipment", wantInReturn: "signed sports jersey" } ,
        { id: 3, wantToTrade: "Cooked meal prep", wantInReturn: "copywriting"  },

    ]*/

    const [currentUserInfo, setCurrentUserInfo] = useState({});
    const [myBarters, setMyBarters] = useState([]);
    const baseURL = "https://localhost:7073/api";

    useEffect(() => {
        var currentUsername = localStorage.getItem('currentUsername');
        fetch(baseURL + "/Account/" + currentUsername)
            .then((response) => response.json())
            .then((userInfo) => {
                console.log(userInfo);
                localStorage.setItem('currentUserInfo', JSON.stringify(userInfo));
                setCurrentUserInfo(userInfo);
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
                        {myBarters.map((item) => {
                            const { id, wantInReturn, wantToTrade } = item;
                            return (
                                <div className={styles.item}>
                                    <div className={styles.infoContainer}>
                                        <div className={styles.info}>
                                            <div className={styles.details}>
                                                <p><b>Want to trade: </b> {wantToTrade}</p>
                                                <img className={styles.arrows} src={arrows} alt="arrows" />
                                                <p><b>For: </b> {wantInReturn}</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div className={styles.buttons}>
                                        <NavLink to={"/editbarter/"+id}><MdEdit style={{ fontSize: 28, color: '#0A0F0D' }} /></NavLink>
                                        <MdDelete style={{ fontSize: 28, marginLeft: 15 }} />
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