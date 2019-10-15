import React from "react";
import { HashRouter as Router, Route, Switch } from "react-router-dom";
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
import decoder from "jwt-decode";
import { HONOPLAY_TRAINER_PROJECT_ID } from "./Helpers/Statics";
import { init } from "@omegabigdata/terasu-api-proxy";

const CheckTokenExp = token => {
  if (!token) return false;
  const expireDate = decoder(token).exp * 1000;
  if (expireDate < Date.now()) {
    return true;
  }
  return false;
};

class App extends React.Component {
  componentWillMount() {
    init(HONOPLAY_TRAINER_PROJECT_ID);
  }

  componentDidUpdate() {
    let token = localStorage.getItem("token");
    if (CheckTokenExp(token)) {
      this.props.postTrainerRenewToken(token);
    }
    if (this.props.userTrainerRenewToken) {
      localStorage.setItem("token", token);
    }
  }

  render() {
    return (
      <React.Fragment>
        <Router>
          <Switch>
            <Route exact path="/" component={Login} />
            <Route exact path="/login" component={Login} />
            <Route exact path="/homepage" component={Home} />
            <Route exact path="/classroom" component={Classroom} />
            <Route exact path="/trainingdetail" component={Training} />
            <Route exact path="/joincode" component={JoinCode} />
            <Route exact path="/logout" component={Logout} />
          </Switch>
        </Router>
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  const { userTrainerToken } = state.trainerUserToken;

  const { userTrainerRenewToken } = state.userTraineeRenewToken;

  return { userTrainerToken, userTrainerRenewToken };
};

export default connect(
  mapStateToProps,
  { fethTrainerUserToken, fetchTrainingList, postTrainerRenewToken }
)(App);
