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
import TrainingSeriesCreate from './views/TrainingSeries/TrainingSeriesCreate';
// import TrainingCreate from './views/TrainingSeries/TrainingCreate';
// import TrainingInformation from './views/TrainingSeries/TrainingInformation';

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
        <Route path="/home/questions" component={Questions} />
        <Route path="/home/tenantinformation" component={TenantInformation} />
        <Route path="/home/trainers" component={Trainers}/>
        <Route path="/home/usermanagement" component={UserManagement}/>
        <Route path="/home/addquestion" component={NewQuestion}/>
        <Route path="/home/trainingseries" exact component={TrainingSeries}/>
        <Route path="/home/trainingseries/trainingseriescreate" exact component={TrainingSeriesCreate}/>
        {/* <Route path="/home/trainingseries/trainingcreate" exact component={TrainingCreate}/>
        <Route path="/home/trainingseries/traininginformation" exact component={TrainingInformation}/> */}
      </Layout>
    );
  }
}

export default hot(module)(App);
