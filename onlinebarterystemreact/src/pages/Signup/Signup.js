import React, { useState, useEffect } from "react";
import styles from "./Signup.module.css";
import { NavLink , useNavigate} from "react-router-dom";
import { MdError } from "react-icons/md";
import logo from "../../assets/obslogo.png";

const Signup = () => {
    const [formState, setFormState] = useState({
        firstName: "",
        lastName: "",
        userName:"",
        email: "",
        password: "",
        confirmPassword: "",
        phoneNumber: "",
        cityId: ""
    });
    const navigate = useNavigate();
    const [cities, setCityState] = useState([]);
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);

    const baseURL = "https://localhost:7073/api";
    useEffect(() => {
        fetch(baseURL+"/City")
           .then((response) => response.json())
           .then((cities) => {
              console.log(cities);
              setCityState(cities);
           })
           .catch((err) => {
              console.log(err.message);
           });
     }, []);
    

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

        if (!formState.email || !formState.password || !formState.firstName || !formState.lastName || !formState.confirmPassword
            || !formState.phoneNumber || !formState.cityId || !formState.userName) {
            setError("All of the fields are required");
            setLoading(false);
        } else {
            signUp();
        }
    };

    const signUp = async () => {
        try {
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formState)
            };
          const response = await fetch(baseURL+"/Account/signup", requestOptions, requestOptions);
    
          if (!response?.ok) {
            throw new Error(
              `Error! status: ${response.status} message ${response.message}`
            );
          }    

          navigate("/signin");

        } catch (err) {
          console.log(err.message);
        } finally {
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
                                placeholder="First Name"
                                id="firstName"
                                name="firstName"
                                value={formState.firstName}
                                onChange={(e) => handleChange(e)}
                            />
                            <input
                                type="text"
                                placeholder="Last Name"
                                id="lastName"
                                name="lastName"
                                value={formState.lastName}
                                onChange={(e) => handleChange(e)}
                            />
                            <input
                                type="text"
                                placeholder="User Name"
                                id="userName"
                                name="userName"
                                value={formState.userName}
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
                                type="text"
                                placeholder="Phone number"
                                id="phoneNumber"
                                name="phoneNumber"
                                value={formState.phoneNumber}
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
                                id="confirmPassword"
                                name="confirmPassword"
                                value={formState.confirmPassword}
                                onChange={(e) => handleChange(e)}
                            />
                            <select
                                id="cityId"
                                name="cityId"
                                value={formState.cityId}
                                onChange={(e) => handleChange(e)}
                                style={{ textTransform: 'capitalize' }}
                            >
                                <option value="" disabled>Select your city location</option>
                                {
                                    cities.map((item) => {
                                        return <option value={item.id} key={item.id} style={{ textTransform: 'capitalize' }}>{item.name}</option>;
                                    })
                                }
                                
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