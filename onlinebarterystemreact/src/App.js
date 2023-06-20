import React, { Component } from 'react';
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Signup from './pages/Signup/Signup';
import Signin from './pages/Signin/Signin';
import Homepage from './pages/Homepage/Homepage';
import ContactUs from './pages/ContactUs/ContactUs';
import ParticipationgBarters from './pages/ParticipatingBarters/ParticipatingBarters';
import MyBarters from './pages/MyBarters/MyBarters';
import AddABarter from './pages/AddABarter/AddABarters';
import BarterRequests from './pages/BarterRequests/BarterRequests';
import EditBarter from './pages/EditBarter/EditBarter';
import Search from './pages/Search/Search';
import AccountInfo from './pages/AccountInfo/AccountInfo';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true };
    }


    static renderForecastsTable(forecasts) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map(forecast =>
                        <tr key={forecast.date}>
                            <td>{forecast.date}</td>
                            <td>{forecast.temperatureC}</td>
                            <td>{forecast.temperatureF}</td>
                            <td>{forecast.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
       
        return (
            <div>
                <Router>
                    <Routes>
                        <Route exact path='/' element={<Homepage />} />
                        <Route exact path='/signup' element={<Signup />} />
                        <Route exact path='/signin' element={<Signin />} />
                        <Route exact path='/contactus' element={<ContactUs />} />
                        <Route exact path='/addbarter' element={<AddABarter />} />
                        <Route exact path='/editbarter/:barterId' element={<EditBarter />} />
                        <Route exact path='/profile/participatingbarters' element={<ParticipationgBarters />} />
                        <Route exact path='/profile/mybarters' element={<MyBarters />} />
                        <Route exact path='/profile/requests' element={<BarterRequests />} />
                        <Route exact path='/search' element={<Search />} />
                        <Route exact path='/account' element={<AccountInfo />} />
                    </Routes>
                </Router>
            </div>
        );
    }

  
}





/*import React, { Component } from 'react';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    static renderForecastsTable(forecasts) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map(forecast =>
                        <tr key={forecast.date}>
                            <td>{forecast.date}</td>
                            <td>{forecast.temperatureC}</td>
                            <td>{forecast.temperatureF}</td>
                            <td>{forecast.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>lllLoading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : App.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel" >hshshs Wealalalther forecast</h1>
                <p>This component demonstrates fetching minalaakakakkadata from the server.</p>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
}*/
