import React, { useState } from "react";
import styles from "./AddABarter.module.css";
import { MdError } from "react-icons/md";
import Navbar from "../../components/Navbar/Navbar";
import { categoryList } from "../../data";

const AddABarter = () => {
    const [formState, setFormState] = useState({
        wantToTrade: "",
        wantInReturn: "",
        category: "",
        subcategory: "",
    });
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);
    const [selectedCategory, setSelectedCategory] = useState('');

    

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

        if (!formState.wantToTrade || !formState.wantInReturn || !formState.category) {
            setError("All of the fields are required");
            setLoading(false);
        }
    };

    return (
        <div className={styles.container}>           
            <div className={styles.overlay}>
                <Navbar />
                <div className={styles.main}>
                   
                    <div
                        className={styles.formContainer}
                        onSubmit={(e) => handleSubmit(e)}
                    >
                        <h1 className={styles.heading}>Add a Barter</h1>
                        <form className={styles.addBarterForm}>
                            <input
                                type="text"
                                placeholder="What do you want to trade?"
                                id="wantToTrade"
                                name="wantToTrade"
                                value={formState.wantToTrade}
                                onChange={(e) => handleChange(e)}
                            />
                            <input
                                type="text"
                                placeholder="What do you want in return?"
                                id="wantInReturn"
                                name="wantInReturn"
                                value={formState.wantInReturn}
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
                                    formState.category != '' && categoryList.filter(obj => { return obj.category === formState.category })[0].subcategories.length!=0
                                    && categoryList.filter(obj => { return obj.category === formState.category })[0].subcategories.map((item) => {
                                        return <><option value={item} style={{ textTransform: 'capitalize' }}>{item}</option></>;
                                    })
                                }
                            </select>

                            {error && (
                                <div className={styles.error}>
                                    <MdError /> <span>{error}</span>
                                </div>
                            )}
                           
                            <button className={styles.addBarterBtn} type="submit">
                                ADD BARTER
                            </button>
                        </form>
                        
                    </div>
                </div>
                <div className={styles.circle1}></div>
                <div className={styles.circle2}></div>
                <div className={styles.circle3}></div>
            </div>
        </div>
    );
};

export default AddABarter;