import React, { Component } from "react";
import { hot } from "react-hot-loader";
import { Route } from "react-router-dom";
import Layout from "./components/Layout/LayoutComponent";

import Questions from "./views/Questions/Questions";
import TenantInformation from "./views/TenantInformation/TenantInformation";
import Trainers from "./views/Trainers/Trainers";
import UserManagement from "./views/UserManagement/UserManagement";
import NewQuestion from "./views/Questions/NewQuestion";
import TrainingSeries from './views/TrainingSeries/TrainingSeries';
import TrainingSeriesUpdate from './views/TrainingSeries/TrainingSeriesUpdate';
import TrainingSeriesInformation from './views/TrainingSeries/TrainingSeriesInformation';

import setToken from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/index";


class App extends Component {
  componentDidMount() {
    let token = localStorage.getItem("token");
    if (token) {
      console.log("token var :", token);
      setToken.setToken(token);
    }
  }

  render() {
    return (
      <Layout>
        <Route path="/honoplay/questions" component={Questions} />
        <Route path="/honoplay/tenantinformation" component={TenantInformation} />
        <Route path="/honoplay/trainerUsers" component={Trainers} />
        <Route path="/honoplay/usermanagement" component={UserManagement} />
        <Route path="/honoplay/addquestion" component={NewQuestion} />
        <Route path="/honoplay/trainingseries" exact component={TrainingSeries} />
        <Route path="/honoplay/trainingseries/:trainingseriesId" exact component={TrainingSeriesUpdate} />
        <Route path="/honoplay/trainingseries/:trainingseriesId/training" exact component={TrainingSeriesInformation} />
      </Layout>
    );
  }
}

export default hot(module)(App);
