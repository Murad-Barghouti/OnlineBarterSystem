import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import styles from "./EditBarter.module.css";
import { MdError, MdCheckCircle } from "react-icons/md";
import Navbar from "../../components/Navbar/Navbar";

const EditBarter = () => {
    const { barterId } = useParams();
    const [formState, setFormState] = useState({
        giveType: 0,
        receiveType: 0,
        giveTypeId: 0,
        receiveTypeId: 0,
        description: "",
    });
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);
    const [categoryList, setCategoryList] = useState([]);
 
    //console.log(barterId);

    const baseURL = "https://localhost:7073/api";
    useEffect(() => {
        setFormState({
            description: JSON.parse(localStorage.getItem('editBarter')).description,
            giveType: JSON.parse(localStorage.getItem('editBarter')).giveType.category.id+"",
            receiveType: JSON.parse(localStorage.getItem('editBarter')).receiveType.category.id+"",
            giveTypeId: 0,
            receiveTypeId: 0,

        });
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
        //console.log(formState);
        setFormState({
            ...formState,
            [e.target.name]: e.target.value,
        });

    };

    const handleSubmit = (e) => {
        setLoading(true);
        setError("");
        setSuccess("");
        e.preventDefault();

        if (!formState.description ) {
            setError("Description is required");
            setLoading(false);
        } else {
            //console.log(formState);

            const requestOptions = {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    description: formState.description
                })
            };
            console.log(requestOptions);
            fetch(baseURL + "/Barter/"+barterId, requestOptions)
                .then(data => {
                    console.log(data);
                    setSuccess("Barter updated!")
                })
                .catch((err) => {
                    console.log(err.message);
                    setError("Server error");
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
                        <h1 className={styles.heading}>Edit Your Barter</h1>
                        <form className={styles.editBarterForm}>
                            <input
                                type="text"
                                placeholder="Description of the barter"
                                id="description"
                                name="description"
                                value={formState.description}
                                onChange={(e) => handleChange(e)}
                            />
                            
                            {//uncomment later
                            // <p>Select category and subcategory what you want to trade:</p>
                            // <select
                            //     id="giveType"
                            //     name="giveType"
                            //     value={formState.giveType}
                            //     onChange={(e) => handleChange(e)}
                            //     style={{ textTransform: 'capitalize' }}
                            // >
                            //     <option value={0} disabled  >Select the category</option>
                            //     {
                            //         categoryList.map((item) => {
                            //             console.log(item);
                            //             return <option key={item.id} value={item.id} style={{ textTransform: 'capitalize' }}>{item.name}</option>;
                            //         })
                            //     }
                            // </select>
                            // <select
                            //     id="giveTypeId"
                            //     name="giveTypeId"
                            //     value={formState.giveTypeId}
                            //     onChange={(e) => handleChange(e)}
                            //     style={{ textTransform: 'capitalize' }}
                            // >
                            //     <option value={0} disabled  >Select the subcategory</option>
                            //     {/*{
                            //         formState.giveType !== 0
                            //         &&
                            //         categoryList.filter(obj => { return ("" + obj.id) === formState.giveType })[0].subCategories.map((item) => {
                            //             return <option key={item.id} value={item.id} style={{ textTransform: 'capitalize' }}>{item.name}</option>;
                            //         })

                            //     }*/}

                            // </select>

                            // <p>Select category and subcategory what you want in return:</p>
                            // <select
                            //     id="receiveType"
                            //     name="receiveType"
                            //     value={formState.receiveType}
                            //     onChange={(e) => handleChange(e)}
                            //     style={{ textTransform: 'capitalize' }}
                            // >
                            //     <option value={0} disabled  >Select the category</option>
                            //     {
                            //         categoryList.map((item) => {
                            //             return <><option key={item.id} value={item.id} style={{ textTransform: 'capitalize' }}>{item.name}</option></>;
                            //         })
                            //     }
                            // </select>
                            // <select
                            //     id="receiveTypeId"
                            //     name="receiveTypeId"
                            //     value={formState.receiveTypeId}
                            //     onChange={(e) => handleChange(e)}
                            //     style={{ textTransform: 'capitalize' }}
                            // >
                            //     <option value={0} disabled  >Select the subcategory</option>
                            //     {/*{
                            //         formState.receiveType !== 0
                            //         &&

                            //         categoryList.filter(obj => { return ("" + obj.id) === formState.receiveType })[0].subCategories.map((item) => {
                            //             return <option key={item.id} value={item.id} style={{ textTransform: 'capitalize' }}>{item.name}</option>;
                            //         })
                            //     }*/}
                            // </select>
}
                            {error && (
                                <div className={styles.error}>
                                    <MdError /> <span>{error}</span>
                                </div>
                            )}

                            {success && (
                                <div className={styles.success}>
                                    <MdCheckCircle /> <span>{success}</span>
                                </div>
                            )}

                            <button className={styles.editBarterBtn} type="submit">
                                SAVE CHANGES
                            </button>
                        </form>

                    </div>
                </div>
            </div>
        </div>
    );
};

export default EditBarter;