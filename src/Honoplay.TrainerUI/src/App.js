import React from "react";
import { Router as BrowserRouter, Route } from "react-router-dom";
import History from "./Helpers/History";
import Login from "./Pages/Login";
import Home from "./Pages/Home/index";
import Training from "./Pages/Training";
import JoinCode from "./Pages/JoinCode";
import { connect } from "react-redux";
import {
  fethTrainerUserToken,
  fetchTrainingList
} from "@omegabigdata/honoplay-redux-helper/Src/actions/TrainerUser";
import Classroom from "./Pages/Classroom";
import Logout from "./Helpers/Logout";

class App extends React.Component {
  render() {
    return (
      <>
        <BrowserRouter history={History}>
          <Route exact path="/" component={Login} />
          <Route path="/login" component={Login} />
          <Route path="/homepage" component={Home} />
          <Route path="/classroom" component={Classroom} />
          <Route path="/trainingdetail" component={Training} />
          <Route path="/joincode" component={JoinCode} />
          <Route path="/logout" component={Logout} />
        </BrowserRouter>
      </>
    );
  }
}

const mapStateToProps = state => {
  const { userTrainerToken } = state.trainerUserToken;
  return { userTrainerToken };
};

export default connect(
  mapStateToProps,
  { fethTrainerUserToken, fetchTrainingList }
)(App);
