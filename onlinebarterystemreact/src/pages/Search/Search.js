import React, { useState } from "react";
import styles from "./Search.module.css";
import { NavLink } from "react-router-dom";
import { MdError } from "react-icons/md";
import { FaSearch,FaUserCircle } from "react-icons/fa";
import arrows from "../../assets/arrows.jpg";
import Navbar from "../../components/Navbar/Navbar";
import {cities, categoryList }  from "../../data"

const Search = () => {

    const [formState, setFormState] = useState({
        searchTerm:"",
        category: "",
        subcategory: "",
        city:""
    });
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);



    const handleChange = (e) => {
        setFormState({
            ...formState,
            [e.target.name]: e.target.value,
        });
    };

    const handleSubmit = (e) => {
        setLoading(true);
        setError("");
        e.preventDefault();

        if (formState.searchTerm==="" && formState.category === "" && formState.subcategory === "" && formState.city === "") {
            setError("Fill atleast one of the fields above.");
            setLoading(false);
        }
    };


    const searchResults = [
        { username: "jane doe", barter: { wantToTrade: "levis jeans", wantInReturn: "600TL" } },
        { username: "sara lane", barter: { wantToTrade: "gold equipment", wantInReturn: "signed sports jersey" } },
        { username: "matt damon", barter: { wantToTrade: "Cooked meal prep", wantInReturn: "copywriting" } },

    ]



    return (
        <div className={styles.container}>
            <div className={styles.overlay}>
                <Navbar />
                <div className={styles.main}>                    
                    <div className={styles.formContainer} onSubmit={(e) => handleSubmit(e)}>
                        <form className={styles.searchForm}>
                            <input
                                type="text"
                                placeholder="Search"
                                id="searchTerm"
                                name="searchTerm"
                                value={formState.searchTerm}
                                onChange={(e) => handleChange(e)}
                            />
                            <select
                                id="category"
                                name="category"
                                value={formState.category}
                                onChange={(e) => handleChange(e)}
                                style={{ textTransform: 'capitalize' }}
                            >
                                <option value="" disabled selected >Select the category</option>
                                {
                                    categoryList.map((item) => {
                                        return <><option value={item.category} style={{ textTransform: 'capitalize' }}>{item.category}</option></>;
                                    })
                                }
                            </select>
                            <select
                                id="subcategory"
                                name="subcategory"
                                value={formState.subcategory}
                                onChange={(e) => handleChange(e)}
                                style={{ textTransform: 'capitalize' }}
                            >
                                <option value="" disabled selected >Select the subcategory</option>
                                {
                                    formState.category != '' && categoryList.filter(obj => { return obj.category === formState.category })[0].subcategories.length != 0
                                    && categoryList.filter(obj => { return obj.category === formState.category })[0].subcategories.map((item) => {
                                        return <><option value={item} style={{ textTransform: 'capitalize' }}>{item}</option></>;
                                    })
                                }
                            </select>
                            <select
                                id="city"
                                name="city"
                                onChange={(e) => handleChange(e)}
                                style={{ textTransform: 'capitalize' }}
                            >
                                <option value="" disabled selected >Select your city location</option>
                                {
                                    cities.map((item) => {
                                        return <><option value={item} style={{ textTransform: 'capitalize' }}>{item}</option></>;
                                    })
                                }

                            </select>
                            <button className={styles.headerItem} type="submit"><FaSearch style={{ fontSize: 25, position: 'relative', bottom:7, opacity: 0.8 }} /></button>

                        </form>
                        {error && (
                            <div className={styles.error}>
                                <MdError /> <span>{error}</span>
                            </div>
                        )}
                        
                    </div>
                    <div className={styles.list}>
                       
                        {searchResults.length != 0 ? searchResults.map((item) => {
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
                                        <button>PARTICIPATE</button>
                                    </div>
                                </div>
                            );
                        }) :
                            <div className={styles.searchResultsHeading}>no search results</div>
                        }
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Search;