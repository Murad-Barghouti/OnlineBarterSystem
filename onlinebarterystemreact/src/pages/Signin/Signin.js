import React, { useState } from "react";
import styles from "./Signin.module.css";
import { NavLink, useNavigate } from "react-router-dom";
import { MdError } from "react-icons/md";
import logo from "../../assets/obslogo.png";

const Signin = () => {
    const [formState, setFormState] = useState({
           userName: "",
        password: "",
    });
    const navigate = useNavigate();
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);

    const baseURL = "https://localhost:7073/api";

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

        if (!formState.userName || !formState.password) {
            setError("All of the fields are required");
            setLoading(false);
        } else {
            //console.log(formState);
            //console.log(JSON.stringify(formState));
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formState)
            };
            console.log(requestOptions);
            fetch(baseURL + "/Account/signin", requestOptions)
                .then(response => response.json())
                .then(data => console.log(data))
                .catch((err) => {
                    console.log(err.message);
                    localStorage.setItem('currentUsername', formState.userName);
                    navigate("/profile/mybarters");
                });
        }
    };

    return (
        <div className={styles.container}>
            <div className={styles.overlay}>
                <div className={styles.main}>
                    <div className={styles.header}>
                        <NavLink to="/"><img className={styles.logo} src={logo} alt="Online barter system" /></NavLink>
                        <h1 className={styles.heading}>Sign in!</h1>
                        <h2 className={styles.subheading}>
                            Enter your username and password.
                        </h2>
                    </div>
                    <div
                        className={styles.formContainer}
                        onSubmit={(e) => handleSubmit(e)}
                    >
                        <form className={styles.signinForm}>
                            <input
                                type="text"
                                placeholder="Username"
                                id="userName"
                                name="userName"
                                value={formState.userName}
                                onChange={(e) => handleChange(e)}
                            />
                            <input
                                type="password"
                                placeholder="Password"
                                id="password"
                                name="password"
                                value={formState.password}
                                onChange={(e) => handleChange(e)}
                            />
                           

                            {error && (
                                <div className={styles.error}>
                                    <MdError /> <span>{error}</span>
                                </div>
                            )}
                            <button className={styles.signinBtn} type="submit">
                                SIGN IN
                            </button>
                        </form>
                        <p className={styles.signupMsg}>
                            Don't have an account yet? <NavLink to="/signup">Sign up</NavLink>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Signin;