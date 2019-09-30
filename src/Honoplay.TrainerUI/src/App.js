import React, { useState, useEffect } from "react";
import { Router as BrowserRouter, Route, Switch } from "react-router-dom";
import History from "./Helpers/History";
import Login from "./Pages/Login";
import Home from "./Pages/Home/index";
import Training from "./Pages/Training";
import JoinCode from "./Pages/JoinCode";
import { connect } from "react-redux";
import {
  fethTrainerUserToken,
  fetchTrainingList,
  postTrainerRenewToken
} from "@omegabigdata/honoplay-redux-helper/Src/actions/TrainerUser";

import Classroom from "./Pages/Classroom";
import Logout from "./Helpers/Logout";

const CheckTokenExp = token => {
  if (!token) return false;
  const expireDate = decoder(token).exp * 1000;

  if (expireDate < Date.now()) {
    return true;
  }
  return false;
};

class App extends React.Component {
  render() {
    return (
      <React.Fragment>
        <BrowserRouter history={History}>
          <Switch>
            <Route exact path="/trainer/" component={Login} />
            <Route exact path="/" component={Login} />
            <Route path="/trainer/login" component={Login} />
            <Route path="/trainer/homepage" component={Home} />
            <Route path="/trainer/classroom" component={Classroom} />
            <Route path="/trainer/trainingdetail" component={Training} />
            <Route path="/trainer/joincode" component={JoinCode} />
            <Route path="/trainer/logout" component={Logout} />
          </Switch>
        </BrowserRouter>
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  const { userTrainerToken } = state.trainerUserToken;
  return { userTrainerToken };
};

export default connect(
  mapStateToProps,
  { fethTrainerUserToken, fetchTrainingList, postTrainerRenewToken }
)(App);
