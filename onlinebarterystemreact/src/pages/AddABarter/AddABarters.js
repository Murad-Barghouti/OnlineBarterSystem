import React, { useState, useEffect } from "react";
import styles from "./AddABarter.module.css";
import { MdError } from "react-icons/md";
import Navbar from "../../components/Navbar/Navbar";

const AddABarter = () => {
    const [formState, setFormState] = useState({       
        giveType: 0,
        receiveType: 0,
        giveTypeId: 0,
        receiveTypeId: 0,
        description:"",
    });
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);
    const [categoryList, setCategoryList] = useState([]);

    const baseURL = "https://localhost:7073/api";
    useEffect(() => {
        fetch(baseURL + "/Category")
            .then((response) => response.json())
            .then((categories) => {
                //console.log(categories);
                setCategoryList(categories);
            })
            .catch((err) => {
                console.log(err.message);
            });
    }, []);
    

    const handleChange = (e) => {
        console.log(e.target.value==="1");
        setFormState({
            ...formState,
            [e.target.name]: e.target.value,
        });
        
    };

    const handleSubmit = (e) => {
        setLoading(true);
        setError("");
        e.preventDefault();

        if (!formState.description || formState.receiveType === 0 || formState.giveType === 0 || formState.receiveTypeId === 0 || !formState.giveTypeId === 0) {
            setError("All of the fields are required");
            setLoading(false);
        } else {
            //console.log(formState);
            
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    initiatorId: 1,
                    receiveTypeId: parseInt(formState.receiveTypeId),
                    giveTypeId: parseInt(formState.giveTypeId),
                    description: formState.description,
                    giveValue: 1,
                    receiveValue: 1
                })
            };
            console.log(requestOptions);
            fetch(baseURL + "/Barter", requestOptions)
                .then(response => response.json())
                .then(data => console.log(data))
                .catch((err) => {
                    console.log(err.message);
                });
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
                                placeholder="Description of the barter"
                                id="description"
                                name="description"
                                value={formState.description}
                                onChange={(e) => handleChange(e)}
                            />
                            <p>Select category and subcategory what you want to trade:</p>
                            <select
                                id="giveType"
                                name="giveType"
                                value={formState.giveType}
                                onChange={(e) => handleChange(e)}
                                style={{ textTransform: 'capitalize' }}
                            >
                                <option value={0} disabled selected >Select the category</option>
                                {
                                    categoryList.map((item) => {
                                        return <><option value={item.id} style={{ textTransform: 'capitalize' }}>{item.name}</option></>;
                                    })
                                }
                            </select>
                            <select
                                id="giveTypeId"
                                name="giveTypeId"
                                value={formState.giveTypeId}
                                onChange={(e) => handleChange(e)}
                                style={{ textTransform: 'capitalize' }}
                            >
                                <option value={0} disabled selected >Select the subcategory</option>
                                {
                                    formState.giveType !== 0 &&
                                    categoryList.filter(obj => { return ("" + obj.id) === formState.giveType })[0].subCategories.map((item) => {
                                        return <option value={item.id} style={{ textTransform: 'capitalize' }}>{item.name}</option>;
                                    })}
                                  
                                
                            </select>
                          
                            <p>Select category and subcategory what you want in return:</p>
                            <select
                                id="receiveType"
                                name="receiveType"
                                value={formState.receiveType}
                                onChange={(e) => handleChange(e)}
                                style={{ textTransform: 'capitalize' }}
                            >
                                <option value={0} disabled selected >Select the category</option>
                                {
                                    categoryList.map((item) => {
                                        return <><option value={item.id} style={{ textTransform: 'capitalize' }}>{item.name}</option></>;
                                    })
                                }
                            </select>
                            <select
                                id="receiveTypeId"
                                name="receiveTypeId"
                                value={formState.receiveTypeId}
                                onChange={(e) => handleChange(e)}
                                style={{ textTransform: 'capitalize' }}
                            >
                                <option value={0} disabled selected >Select the subcategory</option>
                                {
                                    formState.receiveType !== 0 &&
                                    
                                    categoryList.filter(obj => { return ("" + obj.id) === formState.receiveType })[0].subCategories.map((item) => {
                                        return <option value={item.id} style={{ textTransform: 'capitalize' }}>{item.name}</option>;
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
            </div>
        </div>
    );
};

export default AddABarter;