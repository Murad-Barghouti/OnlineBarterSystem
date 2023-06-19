import React, { useState, useEffect } from "react";
import styles from "./Search.module.css";
import { NavLink } from "react-router-dom";
import { MdError } from "react-icons/md";
import { FaSearch, FaUserCircle } from "react-icons/fa";
import arrows from "../../assets/arrows.jpg";
import Navbar from "../../components/Navbar/Navbar";
import { cities, categoryList } from "../../data";

const Search = () => {
  const [cities, setCityState] = useState([]);
  const [formState, setFormState] = useState({
    searchTerm: "",
    categoryId: "",
    subCategoryId: "",
    cityId: "",
  });
  const [allBarters, setAllBarters] = useState([]);
  const [categories, setCategories] = useState([]);
  const [subCategories, setSubCategories] = useState([]);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);

  const user = JSON.parse(localStorage.getItem("currentUserInfo"));

  const baseURL = "https://localhost:7073/api";
  useEffect(() => {
    fetch(baseURL + "/City")
      .then((response) => response.json())
      .then((cities) => {
        setCityState(cities);
      })
      .catch((err) => {
        console.log(err.message);
      });
  }, []);

  useEffect(() => {
    fetch(baseURL + "/Category")
      .then((response) => response.json())
      .then((categories) => {
        setCategories(categories);
      })
      .catch((err) => {
        console.log(err.message);
      });
  }, []);

  useEffect(() => {
    fetch(baseURL + "/Barter")
      .then((response) => response.json())
      .then((barters) => {
        console.log(barters);
        setAllBarters(barters);
      })
      .catch((err) => {
        console.log(err.message);
      });
  }, []);

  const handleChange = (e) => {
    if (e.target.name == "categoryId") {
      let category = categories.find((val) => val.id == e.target.value);
      setSubCategories(category.subCategories);
    }
    setFormState({
      ...formState,
      [e.target.name]: e.target.value,
    });
    console.log(formState);
  };

  const handleSubmit = (e) => {
    setLoading(true);
    setError("");
    e.preventDefault();

    if (
      formState.searchTerm === "" &&
      formState.categoryId === "" &&
      formState.subcategoryId === "" &&
      formState.cityId === ""
    ) {
      setError("Fill atleast one of the fields above.");
      setLoading(false);
    }
  };

  const getBarters = async (id) => {
    try {
      const response = await fetch(baseURL + "/Barter", {
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

      setAllBarters(result);
    } catch (err) {
      console.log(err.message);
    } finally {
    }
  };

  const handleClickSearch = async () => {
    try {
      const response = await fetch(
        baseURL + "/Barter/bartersInCity/" + formState.cityId,
        {
          method: "GET",
          headers: {
            Accept: "application/json",
          },
        }
      );

      if (!response.ok) {
        throw new Error(`Error! status: ${response.status}`);
      }

      const result = await response.json();

      setAllBarters(result);
    } catch (err) {
      console.log(err.message);
    } finally {
    }
  };

  const handleClickJoinBarter = async (id) => {
    try {
      const response = await fetch(
        baseURL + "/Barter/joinBarter/" + id + "?userName=" + user.userName,
        {
          method: "GET",
          headers: {
            Accept: "application/json",
          },
        }
      );

      if (!response.ok) {
        throw new Error(
          `Error! status: ${response.status} message ${response.message}`
        );
      }

      const result = await response.json();

      getBarters();
    } catch (err) {
      console.log(err.message);
    } finally {
    }
  };

  const handleClickLeaveBarter = async (id) => {
    try {
      const response = await fetch(baseURL + "/Barter/leaveBarter/" + id, {
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

      getBarters();
    } catch (err) {
      console.log(err.message);
    } finally {
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
                id="categoryId"
                name="categoryId"
                value={formState.categoryId}
                onChange={(e) => handleChange(e)}
                style={{ textTransform: "capitalize" }}
              >
                <option value="" disabled>
                  Select the category
                </option>
                {categories.map((item) => {
                  return (
                    <option
                      value={item.id}
                      key={item.id}
                      style={{ textTransform: "capitalize" }}
                    >
                      {item.name}
                    </option>
                  );
                })}
              </select>
              <select
                id="subCategoryId"
                name="subCategoryId"
                value={formState.subCategoryId}
                onChange={(e) => handleChange(e)}
                style={{ textTransform: "capitalize" }}
              >
                <option value="" disabled>
                  Select the subcategory
                </option>
                {
                  ///formState.category != '' && categoryList.filter(obj => { return obj.category === formState.category })[0].subcategories.length != 0
                  //&& categoryList.filter(obj => { return obj.category === formState.category })[0].subcategories.map((item) => {
                  //return <><option value={item} style={{ textTransform: 'capitalize' }}>{item}</option></>;
                  subCategories?.map((item) => {
                    return (
                      <option
                        value={item.id}
                        key={item.id}
                        style={{ textTransform: "capitalize" }}
                      >
                        {item.name}
                      </option>
                    );
                  })
                }
              </select>

              <select
                id="cityId"
                name="cityId"
                value={formState.cityId}
                onChange={(e) => handleChange(e)}
                style={{ textTransform: "capitalize" }}
              >
                <option value="" disabled>
                  Select your city location
                </option>
                {cities.map((item) => {
                  return (
                    <option
                      value={item.id}
                      key={item.id}
                      style={{ textTransform: "capitalize" }}
                    >
                      {item.name}
                    </option>
                  );
                })}
              </select>
              <button className={styles.headerItem} type="submit">
                <FaSearch
                  onClick={handleClickSearch}
                  style={{
                    fontSize: 25,
                    position: "relative",
                    bottom: 7,
                    opacity: 0.8,
                  }}
                />
              </button>
            </form>
            {error && (
              <div className={styles.error}>
                <MdError /> <span>{error}</span>
              </div>
            )}
          </div>
          <div className={styles.list}>
            {allBarters.length != 0 ? (
              allBarters.map((item) => {
                const { username, barter } = item;
                return (
                  <div key={item.id} className={styles.item}>
                    <div className={styles.infoContainer}>
                      <div className={styles.info}>
                        <div className={styles.userInfo}>
                          Posted by{" "}
                          <FaUserCircle
                            style={{
                              fontSize: 20,
                              position: "relative",
                              top: 4,
                            }}
                          />{" "}
                          <b className={styles.fullname}>
                            {item.initiator.firstName} {item.initiator.lastName}
                          </b>
                        </div>
                        <div className={styles.details}>
                          <p>
                            <b>Want to trade: </b> {item.giveType.name}
                          </p>
                          <img
                            className={styles.arrows}
                            src={arrows}
                            alt="arrows"
                          />
                          <p>
                            <b>For: </b> {item.receiveType.name}
                          </p>
                        </div>
                      </div>
                    </div>
                    {item.initiatorId != user.id &&
                      item.joinerId != user.id &&
                      item.joinerId == null && (
                        <div className={styles.buttons}>
                          <button
                            onClick={(e) => handleClickJoinBarter(item.id)}
                          >
                            Join Barter
                          </button>
                        </div>
                      )}
                    {item.joinerId == user.id && (
                      <div className={styles.buttons}>
                        <button
                          onClick={(e) => handleClickLeaveBarter(item.id)}
                        >
                          Leave Barter
                        </button>
                      </div>
                    )}
                  </div>
                );
              })
            ) : (
              <div className={styles.searchResultsHeading}>
                no search results
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Search;
