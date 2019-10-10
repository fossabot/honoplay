import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import classNames from 'classnames';
import { Grid, CircularProgress, Divider } from '@material-ui/core';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';
import Button from '../../../components/Button/ButtonComponent';
import Table from '../../../components/Table/TableComponent';
import BreadCrumbs from '../../../components/BreadCrumbs/BreadCrumbs';
import { genderToString } from '../../../helpers/Converter';

import { connect } from 'react-redux';
import { fetchTrainersList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainer';
import { fetchTraineeList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainee';
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
            name: '',
            trainerUserId: '',
            trainingId: '',
            traineeUsersId: []
          }
        ]
      },
      traineeColumns: [
        { title: translate('Name'), field: 'name' },
        { title: translate('Surname'), field: 'surname' },
        {
          title: translate('NationalIdentityNumber'),
          field: 'nationalIdentityNumber'
        },
        { title: translate('PhoneNumber'), field: 'phoneNumber' },
        { title: translate('Gender'), field: 'gender' }
      ],
      traineeList: []
    };
  }

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
      errorClassroom
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
      if (!errorCreateClassroom) {
        this.setState({
          classroomLoading: false,
          classroomError: false
        });
        this.props.history.push(
          `/trainingseries/training/${this.props.match.params.trainingId}`
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
  }

  componentDidMount() {
    this.props.fetchTrainersList(0, 50);
    this.props.fetchTraineeList(0, 50);
    this.props.fetchClassroom(this.classroomId);
  }

  handleClick = () => {
    this.props.createClassroom(this.state.classroom);
  };

  handleUpdate = () => {
    //update
  };

  render() {
    const {
      classroomLoading,
      trainer,
      classroom,
      classroomError,
      traineeColumns,
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
          {classroom.createClassroomModels.map((classroom, id) => (
            <Grid item xs={12} sm={12} key={id}>
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
                />
              </Grid>
              <Grid item xs={12} sm={12}>
                <Table
                  columns={traineeColumns}
                  data={traineeList}
                  isSelected={selected => {
                    classroom.traineeUsersId = selected;
                  }}
                />
              </Grid>
            </Grid>
          ))}
        </Grid>
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
    errorClassroom
  };
};

const mapDispatchToProps = {
  fetchTrainersList,
  createClassroom,
  fetchClassroomListByTrainingId,
  fetchTraineeList,
  fetchClassroom,
  updateClassroom
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(ClassroomCreate));
