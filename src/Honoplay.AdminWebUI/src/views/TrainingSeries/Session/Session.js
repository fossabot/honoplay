import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import classNames from 'classnames';
import { Grid, CircularProgress, Divider } from '@material-ui/core';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';
import Button from '../../../components/Button/ButtonComponent';
import BreadCrumbs from '../../../components/BreadCrumbs/BreadCrumbs';

import { connect } from 'react-redux';
import {
  createSession,
  fetchSessionListByClassroomId,
  fetchSession,
  updateSession
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Session';

class SessionCreate extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      success: false,
      sessionLoading: false,
      game: [{ id: 1, name: 'Kelime Oyunu' }, { id: 2, name: 'Puzzle' }],
      session: {
        createSessionModels: [
          {
            gameId: '',
            classroomId: '',
            name: ''
          }
        ]
      },
      open: false,
      sessionLoading: false,
      sessionError: false
    };
  }

  classroomId = localStorage.getItem('classroomId');
  sessionId = localStorage.getItem('sessionId');

  componentDidUpdate(prevProps) {
    const {
      isCreateSessionLoading,
      newSession,
      errorCreateSession,
      isSessionLoading,
      session,
      errorSession,
      isUpdateSessionLoading,
      updateSession,
      errorUpdateSession
    } = this.props;

    if (prevProps.isSessionLoading && !isSessionLoading && session) {
      if (!errorSession) {
        this.setState({
          session: {
            createSessionModels: session.items
          }
        });
      }
    }

    if (!prevProps.isCreateSessionLoading && isCreateSessionLoading) {
      this.setState({
        sessionLoading: true
      });
    }
    if (!prevProps.errorCreateSession && errorCreateSession) {
      this.setState({
        sessionError: true,
        sessionLoading: false
      });
    }
    if (
      prevProps.isCreateSessionLoading &&
      !isCreateSessionLoading &&
      newSession
    ) {
      this.props.fetchSessionListByClassroomId(this.classroomId);
      if (!errorCreateSession) {
        this.setState({
          sessionLoading: false,
          sessionError: false
        });
        this.props.history.push(
          `/trainingseries/training/classroom/${this.props.match.params.trainingSeriesName}`
        );
      }
    }

    if (!prevProps.isUpdateSessionLoading && isUpdateSessionLoading) {
      this.setState({
        sessionLoading: true
      });
    }
    if (!prevProps.errorUpdateSession && errorUpdateSession) {
      this.setState({
        sessionError: true,
        sessionLoading: false,
        success: false
      });
    }
    if (
      prevProps.isUpdateSessionLoading &&
      !isUpdateSessionLoading &&
      updateSession
    ) {
      if (!errorUpdateSession) {
        this.setState({
          sessionError: false,
          sessionLoading: false,
          success: true
        });
        setTimeout(() => {
          this.setState({ success: false });
        }, 1000);
      }
    }
  }

  componentDidMount() {
    this.props.fetchSession(this.sessionId);
  }

  handleClick = () => {
    this.props.createSession(this.state.session);
  };

  handleUpdate = () => {
    this.state.session.createSessionModels.map((session, id) =>
      this.props.updateSession(session)
    );
  };

  render() {
    const { sessionLoading, game, session, sessionError, success } = this.state;
    const { classes, update } = this.props;

    this.state.session.createSessionModels.map(session => {
      session.classroomId = this.classroomId;
    });
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          {update ? (
            <Grid item xs={12} sm={11} />
          ) : (
            <Grid item xs={12} sm={11}>
              <BreadCrumbs />
            </Grid>
          )}
          <Grid item xs={12} sm={1}>
            <Button
              buttonColor="primary"
              className={buttonClassname}
              buttonName={update ? translate('Update') : translate('Save')}
              onClick={update ? this.handleUpdate : this.handleClick}
              disabled={sessionLoading}
            />
            {sessionLoading && (
              <CircularProgress
                size={24}
                disableShrink={true}
                className={classes.buttonProgressSave}
              />
            )}
          </Grid>
          {!update && (
            <Grid item xs={12} sm={12}>
              <Divider />
            </Grid>
          )}
          <Grid item xs={12} sm={12} />
          {session.createSessionModels.map((session, id) => (
            <Grid item xs={12} sm={12} key={id}>
              <Input
                error={sessionError}
                labelName={translate('SessionName')}
                inputType="text"
                onChange={e => {
                  session.name = e.target.value;
                  this.setState({
                    sessionError: false
                  });
                }}
                value={update && session.name}
              />
              <DropDown
                error={sessionError}
                data={game}
                labelName={translate('Game')}
                onChange={e => {
                  session.gameId = e.target.value;
                  this.setState({
                    sessionError: false
                  });
                }}
                value={update && session.gameId}
              />
            </Grid>
          ))}
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isCreateSessionLoading,
    createSession,
    errorCreateSession
  } = state.createSession;

  let newSession = createSession;

  const { isSessionLoading, session, errorSession } = state.session;

  const {
    isUpdateSessionLoading,
    updateSession,
    errorUpdateSession
  } = state.updateSession;

  return {
    isCreateSessionLoading,
    newSession,
    errorCreateSession,
    isSessionLoading,
    session,
    errorSession,
    isUpdateSessionLoading,
    updateSession,
    errorUpdateSession
  };
};

const mapDispatchToProps = {
  createSession,
  fetchSessionListByClassroomId,
  fetchSession,
  updateSession
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(SessionCreate));
