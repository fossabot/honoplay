import React, { useState, useEffect } from 'react';
import { Route, Redirect, Switch } from 'react-router-dom';
import Layout from './components/Layout/LayoutComponent';

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
      <Switch>
        <Layout>
          <Route path="/admin/profile" component={Profile} />
          <Route path="/admin/dashboard" component={Dashboard} />
          <Route path="/admin/reports" component={Reports} />
          <Route path="/admin/questions" component={Questions} />
          <Route path="/admin/trainees" component={Trainees} />
          <Route path="/admin/trainers" component={Trainers} />
          <Route path="/admin/usermanagement" component={UserManagement} />
          <Route path="/admin/addquestion" component={NewQuestion} />
          <Route path="/admin/trainingseries" component={TrainingSeries} />
          <Route
            exact
            path="/admin/trainingseriesdetail"
            component={Trainings}
          />
          <Route
            exact
            path="/admin/trainingseriesdetail/training"
            component={TrainingSeriesInformation}
          />
          <Route
            path="/admin/trainingseriesupdate"
            component={TrainingSeriesUpdate}
          />
        </Layout>
      </Switch>
    );
  } else {
    return <Redirect to="/admin/login" />;
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
