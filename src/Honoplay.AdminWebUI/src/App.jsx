import React, { useState, useEffect } from 'react';
import {
  HashRouter as Router,
  Route,
  Redirect,
  Switch
} from 'react-router-dom';
import Layout from './components/Layout/LayoutComponent';

import Questions from './views/Questions/Questions';
import Trainees from './views/Trainees/Trainee';
import Trainers from './views/Trainers/Trainers';
import UserManagement from './views/UserManagement/UserManagement';
import NewQuestion from './views/Questions/NewQuestion';
import TrainingSeries from './views/TrainingSeries/TrainingSeries/TrainingSeries';
import TrainingSeriesTab from './views/TrainingSeries/TrainingSeries/TrainingSeriesTab';
import Training from './views/TrainingSeries/Training/Training';
import TrainingsTab from './views/TrainingSeries/Training/TrainingsTab';
import Classroom from './views/TrainingSeries/Classroom/Classroom';
import Session from './views/TrainingSeries/Session/Session';
import SessionsTab from './views/TrainingSeries/Session/SessionsTab';
import ClassroomTab from './views/TrainingSeries/Classroom/ClassroomTab';
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
        <Router>
          <Switch>
            <Layout>
              <Route exact path="/profile" component={Profile} />
              <Route exact path="/dashboard" component={Dashboard} />
              <Route exact path="/reports" component={Reports} />
              <Route exact path="/questions" component={Questions} />
              <Route exact path="/trainees" component={Trainees} />
              <Route exact path="/trainers" component={Trainers} />
              <Route exact path="/usermanagement" component={UserManagement} />
              <Route exact path="/addquestion" component={NewQuestion} />
              <Route exact path="/trainingseries" component={TrainingSeries} />
              <Route
                exact
                path="/trainingseries/:trainingSeriesId/"
                component={TrainingSeriesTab}
              />
              <Route
                exact
                path="/trainingseries/:trainingSeriesId/training/"
                component={Training}
              />
              <Route
                exact
                path="/trainingseries/training/:trainingId/"
                component={TrainingsTab}
              />
              <Route
                exact
                path="/trainingseries/training/:trainingId/classroom"
                component={Classroom}
              />
              <Route
                exact
                path="/trainingseries/training/classroom/:classroomId"
                component={ClassroomTab}
              />
              <Route
                exact
                path="/trainingseries/training/classroom/:classroomId/session"
                component={Session}
              />
              <Route
                exact
                path="/trainingseries/training/classroom/session/:sessionId"
                component={SessionsTab}
              />
            </Layout>
          </Switch>
        </Router>
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
