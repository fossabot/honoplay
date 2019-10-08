import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, CircularProgress, Divider, IconButton } from '@material-ui/core';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';
import Button from '../../../components/Button/ButtonComponent';
import BreadCrumbs from '../../../components/BreadCrumbs/BreadCrumbs';

import { connect } from 'react-redux';
import {
  createSession,
  fetchSessionListByClassroomId
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Session';

class SessionCreate extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
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

  componentDidUpdate(prevProps) {
    const {
      isCreateSessionLoading,
      newSession,
      errorCreateSession
    } = this.props;

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
          `/admin/trainingseries/training/classroom/${this.props.match.params.trainingSeriesName}`
        );
      }
    }
  }

  handleClick = () => {
    this.props.createSession(this.state.session);
  };

  render() {
    const { sessionLoading, game, session, sessionError } = this.state;
    const { classes } = this.props;

    this.state.session.createSessionModels.map(session => {
      session.classroomId = this.classroomId;
    });

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={11}>
            <BreadCrumbs />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              buttonColor="primary"
              buttonName={translate('Save')}
              onClick={this.handleClick}
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
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
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

  return {
    isCreateSessionLoading,
    newSession,
    errorCreateSession
  };
};

const mapDispatchToProps = {
  createSession,
  fetchSessionListByClassroomId
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(SessionCreate));
