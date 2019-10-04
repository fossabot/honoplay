import React, { useState, useEffect } from 'react';
import { Route, Redirect, Switch } from 'react-router-dom';
import Layout from './components/Layout/LayoutComponent';
import Login from './views/Login/Login';

import Questions from './views/Questions/Questions';
import Trainees from './views/Trainees/Trainee';
import Trainers from './views/Trainers/Trainers';
import UserManagement from './views/UserManagement/UserManagement';
import NewQuestion from './views/Questions/NewQuestion';
import TrainingSeries from './views/TrainingSeries/TrainingSeries';
import Trainings from './views/TrainingSeries/Trainings';
import TrainingSeriesInformation from './views/TrainingSeries/TrainingSeriesInformation';
import TrainingSeriesUpdate from './views/TrainingSeries/TrainingSeriesUpdate';
import Dashboard from './views/Home/Dashboard';
import Reports from './views/Reports/Reports';
import Profile from './views/Profile/Profile';
import setToken from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/index';
import { connect } from 'react-redux';
import { renewToken } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/AdminUser';
import decoder from 'jwt-decode';
import { StylesProvider, createGenerateClassName } from '@material-ui/styles';

const generateClassName = createGenerateClassName({
  productionPrefix: 'c',
  disableGlobal: true
});
const CheckTokenExp = token => {
  if (!token) return false;
  const expireDate = decoder(token).exp * 1000;

  if (expireDate < Date.now()) {
    return true;
  }
  return false;
};

const App = ({ renewToken, newToken }) => {
  const [token] = useState(localStorage.getItem('token'));
  const [isCheckedToken, setIsCheckedToken] = useState(false);

  useEffect(() => {
    if (token) {
      if (CheckTokenExp(token)) {
        renewToken(token);
      } else {
        setToken.setToken(token);
        setIsCheckedToken(true);
      }
    }
  }, []);

  useEffect(() => {
    if (newToken) {
      setToken.setToken(newToken);
      localStorage.setItem('token', newToken);
      setIsCheckedToken(true);
    }
  }, [newToken]);

  if (token) {
    if (!isCheckedToken) {
      return null;
    }

    return (
      <StylesProvider generateClassName={generateClassName}>
        <Switch>
          <Route exact path="/" component={Login} />
          <Layout>
            <Route path="/profile" component={Profile} />
            <Route path="/dashboard" component={Dashboard} />
            <Route path="/reports" component={Reports} />
            <Route path="/questions" component={Questions} />
            <Route path="/trainees" component={Trainees} />
            <Route path="/trainers" component={Trainers} />
            <Route path="/usermanagement" component={UserManagement} />
            <Route path="/addquestion" component={NewQuestion} />
            <Route exact path="/trainingseries" component={TrainingSeries} />
            <Route
              exact
              path="/trainingseries/:trainingName"
              component={props => <Trainings {...props} />}
            />
            <Route
              exact
              path="/trainingseriesdetail/training"
              component={TrainingSeriesInformation}
            />
            <Route
              path="/trainingseriesupdate"
              component={TrainingSeriesUpdate}
            />
          </Layout>
        </Switch>
      </StylesProvider>
    );
  } else {
    return <Redirect to="/login" />;
  }
};

const mapStateToProps = state => {
  const { isRenewTokenLoading, renewToken, errorRenewToken } = state.renewToken;

  return {
    isRenewTokenLoading,
    newToken: renewToken,
    errorRenewToken
  };
};

const mapDispatchToProps = {
  renewToken
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(App);
