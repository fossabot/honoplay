import React, { useState, useEffect } from 'react';
import { Route, Redirect, Switch } from 'react-router-dom';
import Layout from './components/Layout/LayoutComponent';

import Questions from './views/Questions/Questions';
import Trainees from './views/Trainees/Trainee';
import Trainers from './views/Trainers/Trainers';
import UserManagement from './views/UserManagement/UserManagement';
import NewQuestion from './views/Questions/NewQuestion';
import TrainingSeries from './views/TrainingSeries/TrainingSeries/TrainingSeries';
import TrainingSeriesTab from './views/TrainingSeries/TrainingSeries/TrainingSeriesTab';
import Classrooms from './views/TrainingSeries/Classroom/Classrooms';
import Classroom from './views/TrainingSeries/Classroom/Classroom';
import Sessions from './views/TrainingSeries/Session/Sessions';
import Session from './views/TrainingSeries/Session/Session';

import Dashboard from './views/Home/Dashboard';
import Reports from './views/Reports/Reports';
import Profile from './views/Profile/Profile';
import setToken from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/index';
import { connect } from 'react-redux';
import { renewToken } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/AdminUser';
import decoder from 'jwt-decode';
import { StylesProvider, createGenerateClassName } from '@material-ui/styles';
import Training from './views/TrainingSeries/Training/Training';

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
          <Layout>
            <Route path="/admin/profile" component={Profile} />
            <Route path="/admin/dashboard" component={Dashboard} />
            <Route path="/admin/reports" component={Reports} />
            <Route path="/admin/questions" component={Questions} />
            <Route path="/admin/trainees" component={Trainees} />
            <Route path="/admin/trainers" component={Trainers} />
            <Route path="/admin/usermanagement" component={UserManagement} />
            <Route path="/admin/addquestion" component={NewQuestion} />
            <Route
              exact
              path="/admin/trainingseries"
              component={TrainingSeries}
            />
            <Route
              exact
              path="/admin/trainingseries/:trainingSeriesName/"
              component={TrainingSeriesTab}
            />
            <Route
              exact
              path="/admin/trainingseries/:trainingSeriesName/training/"
              component={Training}
            />
            <Route
              exact
              path="/admin/trainingseries/training/:trainingName/"
              component={Classrooms}
            />
            <Route
              exact
              path="/admin/trainingseries/training/:trainingName/classroom"
              component={Classroom}
            />
            <Route
              exact
              path="/admin/trainingseries/training/classroom/:classroomName"
              component={Sessions}
            />
            <Route
              exact
              path="/admin/trainingseries/training/classroom/:classroomName/session"
              component={Session}
            />
          </Layout>
        </Switch>
      </StylesProvider>
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
