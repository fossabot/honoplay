import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import classNames from 'classnames';
import {
  Grid,
  CircularProgress,
  TextField,
  InputAdornment,
  IconButton,
  InputLabel
} from '@material-ui/core';
import Style from '../Style';
import Visibility from '@material-ui/icons/Visibility';
import VisibilityOff from '@material-ui/icons/VisibilityOff';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Button from '../../components/Button/ButtonComponent';

import { connect } from 'react-redux';
import {
  fetchTrainee,
  updateTrainee,
  fetchTraineeList
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainee';
import { fetchWorkingStatusList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/WorkingStatus';
import { fetchDepartmentList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Department';

class TraineesUpdate extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      workingStatuses: [],
      departments: [],
      gender: [
        { id: 0, name: translate('Male') },
        { id: 1, name: translate('Female') }
      ],
      traineeModel: {
        name: '',
        surname: '',
        nationalIdentityNumber: '',
        phoneNumber: '',
        gender: '',
        workingStatusId: '',
        departmentId: '',
        password: ''
      },
      loadingTrainee: false,
      loadingUpdate: false,
      success: false,
      updateError: false,
      confirmPassword: null
    };
  }

  dataId = localStorage.getItem('dataid');

  componentDidUpdate(prevProps) {
    const {
      isWorkingStatusListLoading,
      workingStatusList,
      isTraineeLoading,
      errorTrainee,
      trainee,
      isDepartmentListLoading,
      departmentList,
      isUpdateTraineeLoading,
      errorUpdateTrainee,
      updatedTrainee
    } = this.props;

    if (prevProps.isTraineeLoading && !isTraineeLoading) {
      this.setState({
        loadingTrainee: true
      });
    }
    if (prevProps.isTraineeLoading && !isTraineeLoading && trainee) {
      if (!errorTrainee) {
        this.setState({
          traineeModel: trainee.items[0]
        });
      }
    }
    if (
      prevProps.isWorkingStatusListLoading &&
      !isWorkingStatusListLoading &&
      workingStatusList
    ) {
      this.setState({
        workingStatuses: workingStatusList.items
      });
    }
    if (
      prevProps.isDepartmentListLoading &&
      !isDepartmentListLoading &&
      departmentList
    ) {
      this.setState({
        departments: departmentList.items
      });
    }
    if (!prevProps.isUpdateTraineeLoading && isUpdateTraineeLoading) {
      this.setState({
        loadingUpdate: true
      });
    }
    if (!prevProps.errorUpdateTrainee && errorUpdateTrainee) {
      this.setState({
        updateError: true,
        loadingUpdate: false,
        success: false
      });
    }
    if (
      prevProps.isUpdateTraineeLoading &&
      !isUpdateTraineeLoading &&
      updatedTrainee
    ) {
      if (!errorUpdateTrainee) {
        if (this.state.confirmPassword === this.state.traineeModel.password) {
          this.props.fetchTraineeList(0, 50);
          this.setState({
            updateError: false,
            loadingUpdate: false,
            success: true
          });
          setTimeout(() => {
            this.setState({ success: false });
          }, 1000);
        } else {
          this.setState({
            updateError: true,
            success: false,
            loadingUpdate: false
          });
        }
      }
    }
  }

  componentDidMount() {
    this.props.fetchWorkingStatusList(0, 50);
    this.props.fetchDepartmentList(0, 50);
    this.props.fetchTrainee(parseInt(this.dataId));
  }

  handleChange = e => {
    const { name, value } = e.target;
    this.setState(prevState => ({
      traineeModel: {
        ...prevState.traineeModel,
        [name]: value
      },
      updateError: false,
      [name]: value
    }));
  };

  handleClick = () => {
    this.props.updateTrainee(this.state.traineeModel);
  };

  handleClickShowPassword = () => {
    this.setState(state => ({
      showPassword: !state.showPassword
    }));
  };

  render() {
    const { classes } = this.props;
    const {
      loadingTrainee,
      traineeModel,
      workingStatuses,
      departments,
      gender,
      success,
      loadingUpdate,
      updateError
    } = this.state;
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });
    return (
      <div className={classes.root}>
        {loadingTrainee === false ? (
          <CircularProgress
            size={50}
            disableShrink={true}
            className={classes.progressModal}
          />
        ) : (
          <Grid container spacing={3}>
            <Grid item xs={12} sm={12}>
              <DropDown
                error={updateError}
                data={workingStatuses}
                labelName={translate('WorkingStatus')}
                onChange={this.handleChange}
                name="workingStatusId"
                value={traineeModel.workingStatusId}
              />
              <Input
                error={updateError}
                labelName={translate('Name')}
                inputType="text"
                onChange={this.handleChange}
                name="name"
                value={traineeModel.name}
              />
              <Input
                error={updateError}
                labelName={translate('Surname')}
                inputType="text"
                onChange={this.handleChange}
                name="surname"
                value={traineeModel.surname}
              />
              <DropDown
                error={updateError}
                data={departments}
                labelName={translate('Department')}
                onChange={this.handleChange}
                name="departmentId"
                value={traineeModel.departmentId}
              />
              <Input
                error={updateError}
                labelName={translate('NationalIdentityNumber')}
                inputType="text"
                onChange={this.handleChange}
                name="nationalIdentityNumber"
                value={traineeModel.nationalIdentityNumber}
              />
              <Input
                error={updateError}
                labelName={translate('PhoneNumber')}
                inputType="text"
                onChange={this.handleChange}
                name="phoneNumber"
                value={traineeModel.phoneNumber}
              />
              <DropDown
                error={updateError}
                data={gender}
                labelName={translate('Gender')}
                onChange={this.handleChange}
                name="gender"
                value={traineeModel.gender}
              />
            </Grid>
            <Grid item xs={12} sm={3}>
              <InputLabel className={classes.bootstrapFormLabel}>
                {translate('Password')}
              </InputLabel>
            </Grid>
            <Grid item xs={12} sm={9}>
              <TextField
                error={updateError && true}
                className={classes.passwordInput}
                name="password"
                type={this.state.showPassword ? 'text' : 'password'}
                onChange={this.handleChange}
                InputProps={{
                  endAdornment: (
                    <InputAdornment position="end">
                      <IconButton onClick={this.handleClickShowPassword}>
                        {this.state.showPassword ? (
                          <VisibilityOff />
                        ) : (
                          <Visibility />
                        )}
                      </IconButton>
                    </InputAdornment>
                  )
                }}
              />
            </Grid>
            <Grid item xs={12} sm={3}>
              <InputLabel className={classes.bootstrapFormLabel}>
                {translate('Confirm')}
              </InputLabel>
            </Grid>
            <Grid item xs={12} sm={9}>
              <TextField
                error={updateError && true}
                className={classes.passwordInput}
                name="confirmPassword"
                type={this.state.showPassword ? 'text' : 'password'}
                onChange={this.handleChange}
                InputProps={{
                  endAdornment: (
                    <InputAdornment position="end">
                      <IconButton onClick={this.handleClickShowPassword}>
                        {this.state.showPassword ? (
                          <VisibilityOff />
                        ) : (
                          <Visibility />
                        )}
                      </IconButton>
                    </InputAdornment>
                  )
                }}
              />
            </Grid>
            <Grid item xs={12} sm={12} />
            <Grid item xs={12} sm={11} />
            <Grid item xs={12} sm={1}>
              <Button
                className={buttonClassname}
                buttonColor="primary"
                buttonName={translate('Update')}
                onClick={this.handleClick}
                disabled={loadingUpdate}
              />
              {loadingUpdate && (
                <CircularProgress
                  size={24}
                  disableShrink={true}
                  className={classes.buttonProgressUpdate}
                />
              )}
            </Grid>
          </Grid>
        )}
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isUpdateTraineeLoading,
    errorUpdateTrainee,
    updatedTrainee
  } = state.updateTrainee;

  const { isTraineeLoading, errorTrainee, trainee } = state.trainee;

  const {
    errorDepartmentList,
    isDepartmentListLoading,
    departmentList
  } = state.departmentList;

  const {
    isWorkingStatusListLoading,
    workingStatusList,
    errorWorkingStatusList
  } = state.workingStatusList;

  return {
    errorDepartmentList,
    isDepartmentListLoading,
    departmentList,
    isTraineeLoading,
    errorTrainee,
    trainee,
    isWorkingStatusListLoading,
    workingStatusList,
    errorWorkingStatusList,
    isUpdateTraineeLoading,
    errorUpdateTrainee,
    updatedTrainee
  };
};

const mapDispatchToProps = {
  fetchWorkingStatusList,
  fetchTrainee,
  fetchDepartmentList,
  updateTrainee,
  fetchTraineeList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(TraineesUpdate));
