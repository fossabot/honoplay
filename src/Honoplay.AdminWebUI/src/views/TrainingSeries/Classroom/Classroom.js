import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
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
  fetchClassroomListByTrainingId
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Classroom';

class ClassroomCreate extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
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
      trainees
    } = this.props;

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
        // this.props.history.push(
        //   `/admin/trainingseries/training/${this.props.match.params.trainingName}`
        // );
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
  }

  handleClick = () => {
    this.props.createClassroom(this.state.classroom);
  };

  render() {
    const {
      classroomLoading,
      trainer,
      classroom,
      classroomError,
      traineeColumns,
      traineeList
    } = this.state;
    const { classes } = this.props;

    this.state.classroom.createClassroomModels.map(classroom => {
      classroom.trainingId = this.trainingId;
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
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
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
                />
                <DropDown
                  error={classroomError}
                  data={trainer}
                  labelName={translate('Trainer')}
                  onChange={e => {
                    classroom.trainerUserId = e.target.value;
                    this.setState({ classroomError: false });
                  }}
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

  return {
    isTrainerListLoading,
    errorTrainerList,
    trainersList,
    isCreateClassroomLoading,
    newClassroom,
    errorCreateClassroom,
    isTraineeListLoading,
    errorTraineeList,
    trainees
  };
};

const mapDispatchToProps = {
  fetchTrainersList,
  createClassroom,
  fetchClassroomListByTrainingId,
  fetchTraineeList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(ClassroomCreate));
