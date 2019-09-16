﻿import React, { Component, useState, useEffect } from "react";
import { hot } from "react-hot-loader";
import { Route, Redirect } from "react-router-dom";
import Layout from "./components/Layout/LayoutComponent";

import Questions from "./views/Questions/Questions";
import TenantInformation from "./views/TenantInformation/TenantInformation";
import Trainers from "./views/Trainers/Trainers";
import UserManagement from "./views/UserManagement/UserManagement";
import NewQuestion from "./views/Questions/NewQuestion";
import TrainingSeries from './views/TrainingSeries/TrainingSeries';
import Trainings from './views/TrainingSeries/Trainings';
import TrainingSeriesInformation from './views/TrainingSeries/TrainingSeriesInformation';
import TrainingSeriesUpdate from './views/TrainingSeries/TrainingSeriesUpdate';
import setToken from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/index";

const App = () => {
    const [token] = useState(localStorage.getItem("token"));
    const [loaded, setLoaded] = useState(false);
    useEffect(() => {
            setToken.setToken(token);
            setTimeout(() => {}, 500);
            setLoaded(true);
        },
        []);

    if (token) {
        if (!loaded) {
            return null;
        }
        return (
            <Layout>
                <Route path="/admin/questions" component={Questions}/>
                <Route path="/admin/tenantinformation" component={TenantInformation}/>
                <Route path="/admin/trainers" component={Trainers}/>
                <Route path="/admin/usermanagement" component={UserManagement}/>
                <Route path="/admin/addquestion" component={NewQuestion}/>
                <Route path="/admin/trainingseries" exact component={TrainingSeries}/>
                <Route path="/admin/trainingseriesdetail" exact component={Trainings}/>
                <Route path="/admin/trainingseriesdetail/training" exact component={TrainingSeriesInformation}/>
                <Route path="/admin/trainingseriesupdate" exact component={TrainingSeriesUpdate}/>
            </Layout>
        );
    } else {
        return (
            <Redirect to="/admin/login"/>
        );
    }

};

export default hot(module)(App);
