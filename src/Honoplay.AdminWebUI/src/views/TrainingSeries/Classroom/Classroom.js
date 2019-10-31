import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import classNames from 'classnames';
import { Grid, CircularProgress, Divider } from '@material-ui/core';
import Style from '../../Style';
import DropDown from '../../../components/Input/DropDownInputComponent';
import Button from '../../../components/Button/ButtonComponent';
import Input from '../../../components/Input/InputTextComponent';
import BreadCrumbs from '../../../components/BreadCrumbs/BreadCrumbs';
import TransferList from '../../../components/TransferList/TransferList';
import { genderToString } from '../../../helpers/Converter';
import differenceBy from 'lodash/differenceBy';
import moment from 'moment';
import { connect } from 'react-redux';
import { fetchTrainersList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainer';
import {
  fetchTraineeList,
  fetchTraineeUserByClassroomId
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainee';
import {
  createClassroom,
  fetchClassroomListByTrainingId,
  fetchClassroom,
  updateClassroom
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Classroom';

class ClassroomCreate extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      success: false,
      classroomLoading: false,
      classroomError: false,
      trainer: null,
      classroom: {
        createClassroomModels: [
          {
            trainerUserId: '',
            trainingId: '',
            name: '',
            traineeUsersIdList: [],
            beginDatetime: '',
            endDatetime: '',
            location: ''
          }
        ]
      },
      traineeList: [],
      traineeUsers: []
    };
  }

  trainingSeriesId = localStorage.getItem('trainingSeriesId');
  trainingId = localStorage.getItem('trainingId');
  classroomId = localStorage.getItem('classroomId');

  componentDidUpdate(prevProps) {
    const {
      isTrainerListLoading,
      errorTrainerList,
      trainersList,
      isCreateClassroomLoading,
      newClassroom,
      errorCreateClassroom,
      isTraineeListLoading,
      errorTraineeList,
      trainees,
      isClassroomLoading,
      classroom,
      errorClassroom,
      isTraineeUserByClassroomIdLoading,
      traineeUserByClassroomId,
      errorTraineeUserByClassroomId,
      isUpdateClassroomLoading,
      updateClassroom,
      errorUpdateClassroom
    } = this.props;

    if (prevProps.isClassroomLoading && !isClassroomLoading && classroom) {
      if (!errorClassroom) {
        this.setState({
          classroom: {
            createClassroomModels: classroom.items
          }
        });
      }
    }

    if (
      prevProps.isTrainerListLoading &&
      !isTrainerListLoading &&
      trainersList
    ) {
      if (!errorTrainerList) {
        this.setState({
          trainer: trainersList.items
        });
      }
    }

    if (!prevProps.isCreateClassroomLoading && isCreateClassroomLoading) {
      this.setState({
        classroomLoading: true
      });
    }
    if (!prevProps.errorCreateClassroom && errorCreateClassroom) {
      this.setState({
        classroomError: true,
        classroomLoading: false
      });
    }
    if (
      prevProps.isCreateClassroomLoading &&
      !isCreateClassroomLoading &&
      newClassroom
    ) {
      this.props.fetchClassroomListByTrainingId(this.trainingId);
      this.props.fetchTraineeUserByClassroomId(this.classroomId);
      if (!errorCreateClassroom) {
        this.setState({
          classroomLoading: false,
          classroomError: false
        });
        this.props.history.push(
          `/trainingseries/${this.trainingSeriesId}/training/${this.props.match.params.trainingId}`
        );
      }
    }
    if (prevProps.isTraineeListLoading && !isTraineeListLoading && trainees) {
      if (!errorTraineeList) {
        genderToString(trainees.items);
        this.setState({
          traineeList: trainees.items
        });
      }
    }
    if (
      prevProps.isTraineeUserByClassroomIdLoading &&
      !isTraineeUserByClassroomIdLoading &&
      traineeUserByClassroomId
    ) {
      if (!errorTraineeUserByClassroomId) {
        this.setState({
          traineeUsers: traineeUserByClassroomId.items
        });
      }
    }
    if (!prevProps.isUpdateClassroomLoading && isUpdateClassroomLoading) {
      this.setState({
        classroomLoading: true
      });
    }
    if (!prevProps.errorUpdateClassroom && errorUpdateClassroom) {
      this.setState({
        classroomError: true,
        classroomLoading: false,
        success: false
      });
    }
    if (
      prevProps.isUpdateClassroomLoading &&
      !isUpdateClassroomLoading &&
      updateClassroom
    ) {
      if (!errorUpdateClassroom) {
        this.setState({
          classroomError: false,
          classroomLoading: false,
          success: true
        });
        setTimeout(() => {
          this.setState({ success: false });
        }, 1000);
      }
    }
  }

  componentDidMount() {
    this.props.fetchTrainersList(0, 50);
    this.props.fetchTraineeList(0, 50);
    this.props.fetchClassroom(this.classroomId);
    this.props.fetchTraineeUserByClassroomId(this.classroomId);
  }

  handleClick = () => {
    this.props.createClassroom(this.state.classroom);
  };

  handleUpdate = () => {
    this.state.classroom.createClassroomModels.map((classroom, id) =>
      this.props.updateClassroom(classroom)
    );
  };

  render() {
    const {
      classroomLoading,
      trainer,
      classroom,
      classroomError,
      traineeUsers,
      traineeList,
      success
    } = this.state;
    const { classes, update } = this.props;

    this.state.classroom.createClassroomModels.map(classroom => {
      classroom.trainingId = this.trainingId;
    });
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });

    return (
      <div className={classes.root}>
        {classroom.createClassroomModels.map((classroom, id) => (
          <Grid container spacing={3} key={id}>
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
                disabled={classroomLoading}
              />
              {classroomLoading && (
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
            <Grid item xs={12} sm={12}>
              <Grid item xs={12} sm={12}>
                <Input
                  error={classroomError}
                  labelName={translate('ClassroomName')}
                  inputType="text"
                  onChange={e => {
                    classroom.name = e.target.value;
                    this.setState({ classroomError: false });
                  }}
                  value={update && classroom.name}
                  htmlFor="classroomName"
                  id="classroomName"
                />
                <DropDown
                  error={classroomError}
                  data={trainer}
                  labelName={translate('Trainer')}
                  onChange={e => {
                    classroom.trainerUserId = e.target.value;
                    this.setState({ classroomError: false });
                  }}
                  value={update && classroom.trainerUserId}
                  htmlFor="trainer"
                  id="trainer"
                />
                <Input
                  error={classroomError}
                  labelName={translate('BeginDate')}
                  inputType="datetime-local"
                  onChange={e => {
                    classroom.beginDatetime = e.target.value;
                    this.setState({
                      classroomError: false
                    });
                  }}
                  value={
                    update &&
                    moment(classroom.beginDatetime).format('YYYY-MM-DDTHH:mm')
                  }
                  htmlFor="beginDate"
                  id="beginDate"
                />
                <Input
                  error={classroomError}
                  labelName={translate('EndDate')}
                  inputType="datetime-local"
                  onChange={e => {
                    classroom.endDatetime = e.target.value;
                    this.setState({
                      classroomError: false
                    });
                  }}
                  value={
                    update &&
                    moment(classroom.endDatetime).format('YYYY-MM-DDTHH:mm')
                  }
                  htmlFor="endDate"
                  id="endDate"
                />
                <Input
                  error={classroomError}
                  multiline
                  labelName={translate('Location')}
                  inputType="text"
                  onChange={e => {
                    classroom.location = e.target.value;
                    this.setState({
                      classroomError: false
                    });
                  }}
                  value={update && classroom.location}
                  htmlFor="location"
                  id="location"
                />
              </Grid>
            </Grid>
            {traineeUsers && traineeUsers.length !== 0 && update ? (
              <Grid item xs={12} sm={12}>
                <TransferList
                  leftData={differenceBy(traineeList, traineeUsers, 'id')}
                  rightData={traineeUsers}
                  isSelected={selected => {
                    classroom.traineeUsersIdList = selected;
                  }}
                />
              </Grid>
            ) : (
              <Grid item xs={12} sm={12}>
                <TransferList
                  leftData={traineeList}
                  rightData={[]}
                  isSelected={selected => {
                    classroom.traineeUsersIdList = selected;
                  }}
                />
              </Grid>
            )}
          </Grid>
        ))}
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isTrainerListLoading,
    errorTrainerList,
    trainersList
  } = state.trainersList;

  const {
    isCreateClassroomLoading,
    createClassroom,
    errorCreateClassroom
  } = state.createClassroom;

  let newClassroom = createClassroom;

  const {
    isTraineeListLoading,
    errorTraineeList,
    trainees
  } = state.traineeList;

  const { isClassroomLoading, classroom, errorClassroom } = state.classroom;

  const {
    isTraineeUserByClassroomIdLoading,
    traineeUserByClassroomId,
    errorTraineeUserByClassroomId
  } = state.traineeUserByClassroomId;

  const {
    isUpdateClassroomLoading,
    updateClassroom,
    errorUpdateClassroom
  } = state.updateClassroom;

  return {
    isTrainerListLoading,
    errorTrainerList,
    trainersList,
    isCreateClassroomLoading,
    newClassroom,
    errorCreateClassroom,
    isTraineeListLoading,
    errorTraineeList,
    trainees,
    isClassroomLoading,
    classroom,
    errorClassroom,
    isTraineeUserByClassroomIdLoading,
    traineeUserByClassroomId,
    errorTraineeUserByClassroomId,
    isUpdateClassroomLoading,
    updateClassroom,
    errorUpdateClassroom
  };
};

const mapDispatchToProps = {
  fetchTrainersList,
  createClassroom,
  fetchClassroomListByTrainingId,
  fetchTraineeList,
  fetchClassroom,
  updateClassroom,
  fetchTraineeUserByClassroomId
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(ClassroomCreate));
