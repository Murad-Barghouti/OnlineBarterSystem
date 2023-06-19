import React, { useState } from "react";
import styles from "./Signup.module.css";
import { NavLink } from "react-router-dom";
import { MdError } from "react-icons/md";
import logo from "../../assets/obslogo.png";

const Signup = () => {
    const [formState, setFormState] = useState({
        fullname: "",
        email: "",
        password: "",
        confirmpassword: "",
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

        if (!formState.email || !formState.password || !formState.fullname || !formState.confirmpassword||!formState.city) {
            setError("All of the fields are required");
            setLoading(false);
        }
    };

    return (
        <div className={styles.container}>
            <div className={styles.overlay}>                
                <div className={styles.main}>
                    <div className={styles.header}>
                        <NavLink to="/"><img className={styles.logo} src={logo} alt="Online barter system" /></NavLink>
                        <h1 className={styles.heading}>Join us today!</h1>
                        <h2 className={styles.subheading}>
                            Enter all of the following values to register.
                        </h2>
                    </div>
                    <div
                        className={styles.formContainer}
                        onSubmit={(e) => handleSubmit(e)}
                    >
                        <form className={styles.signupForm}>
                            <input
                                type="text"
                                placeholder="Full Name"
                                id="fullname"
                                name="fullname"
                                value={formState.fullname}
                                onChange={(e) => handleChange(e)}
                            />
                            <input
                                type="email"
                                placeholder="Email"
                                id="email"
                                name="email"
                                value={formState.email}
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
                            <input
                                type="password"
                                placeholder="Confirm password"
                                id="confirmpassword"
                                name="confirmpassword"
                                value={formState.password}
                                onChange={(e) => handleChange(e)}
                            />
                            <select id="city" name="city" onChange={(e) => handleChange(e)}>
                                <option value="" disabled selected >Select your city location</option>
                                <option value="ankara">Ankara</option>
                                <option value="istanbul">Istanbul</option>
                                <option value="izmir">Izmir</option>
                                <option value="antalya">Antalya</option>
                                <option value="adana">Adana</option>
                                <option value="bursa">Bursa</option>
                                <option value="fethiye">Fethiye</option>
                                <option value="trabzon">Trabzon</option>
                            </select>
                            
                            {error && (
                                <div className={styles.error}>
                                    <MdError /> <span>{error}</span>
                                </div>
                            )}
                            <div className={styles.terms}>
                                <input type="checkbox" />
                                <p>
                                    I agree with the <span>Terms {`&`} conditions</span>
                                </p>
                            </div>
                            <button className={styles.signupBtn} type="submit">
                                SIGN UP
                            </button>
                        </form>
                        <p className={styles.signinMsg}>
                            Already have an account? <NavLink to="/signin">Sign in</NavLink>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Signup;