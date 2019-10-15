import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import classNames from 'classnames';
import {
  Grid,
  Divider,
  TextField,
  InputAdornment,
  IconButton,
  InputLabel,
  CircularProgress
} from '@material-ui/core';
import Style from '../Style';
import Visibility from '@material-ui/icons/Visibility';
import VisibilityOff from '@material-ui/icons/VisibilityOff';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Table from '../../components/Table/TableComponent';
import Header from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';

import TraineesUpdate from './TraineesUpdate';
import WorkingStatuses from './WorkingStatus';

import { connect } from 'react-redux';
import {
  fetchTraineeList,
  createTrainee,
  fetchTrainee,
  updateTrainee
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainee';
import { fetchDepartmentList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Department';
import { fetchWorkingStatusList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/WorkingStatus';
import { genderToString } from '../../helpers/Converter';

class Trainee extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      success: false,
      traineeList: [],
      departments: [],
      workingStatuses: [],
      gender: [{ id: 0, name: 'Erkek' }, { id: 1, name: 'Kadın' }],
      traineeColumns: [
        { title: translate('Name'), field: 'name' },
        { title: translate('Surname'), field: 'surname' },
        {
          title: translate('NationalIdentityNumber'),
          field: 'nationalIdentityNumber'
        },
        { title: translate('EmailAddress'), field: 'email' },
        { title: translate('PhoneNumber'), field: 'phoneNumber' },
        { title: translate('Gender'), field: 'gender' }
      ],
      loading: false,
      isErrorTrainee: false,
      traineeModel: {
        name: '',
        surname: '',
        nationalIdentityNumber: '',
        phoneNumber: '',
        gender: '',
        workingStatusId: '',
        departmentId: '',
        password: '',
        email: ''
      }
    };
  }

  componentDidUpdate(prevProps) {
    const {
      isTraineeListLoading,
      errorTraineeList,
      trainees,
      isDepartmentListLoading,
      departmentList,
      isWorkingStatusListLoading,
      workingStatusList,
      errorCreateTrainee,
      isCreateTraineeLoading,
      newTrainee,
      isTraineeLoading,
      errorTrainee,
      trainee,
      isUpdateTraineeLoading,
      errorUpdateTrainee,
      updatedTrainee
    } = this.props;

    if (prevProps.isTraineeListLoading && !isTraineeListLoading && trainees) {
      if (!errorTraineeList) {
        genderToString(trainees.items);
        this.setState({
          traineeList: trainees.items
        });
      }
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
    if (
      prevProps.isWorkingStatusListLoading &&
      !isWorkingStatusListLoading &&
      workingStatusList
    ) {
      this.setState({
        workingStatuses: workingStatusList.items
      });
    }

    if (!prevProps.isCreateTraineeLoading && isCreateTraineeLoading) {
      this.setState({
        loading: true
      });
    }
    if (!prevProps.errorCreateTrainee && errorCreateTrainee) {
      this.setState({
        isErrorTrainee: true,
        loading: false
      });
    }
    if (
      prevProps.isCreateTraineeLoading &&
      !isCreateTraineeLoading &&
      newTrainee
    ) {
      this.props.fetchTraineeList(0, 50);
      if (!errorCreateTrainee) {
        this.setState({
          loading: false,
          isErrorTrainee: false
        });
      }
    }
    if (prevProps.isTraineeLoading && !isTraineeLoading && trainee) {
      if (!errorTrainee) {
        this.setState({
          traineeModel: trainee.items[0]
        });
      }
    }
    if (!prevProps.isUpdateTraineeLoading && isUpdateTraineeLoading) {
      this.setState({
        loading: true
      });
    }
    if (!prevProps.errorUpdateTrainee && errorUpdateTrainee) {
      this.setState({
        isErrorTrainee: true,
        loading: false,
        success: false
      });
    }
    if (
      prevProps.isUpdateTraineeLoading &&
      !isUpdateTraineeLoading &&
      updatedTrainee
    ) {
      if (!errorUpdateTrainee) {
        this.props.fetchTraineeList(0, 50);
        this.setState({
          isErrorTrainee: false,
          loading: false,
          success: true
        });
        setTimeout(() => {
          this.setState({ success: false });
        }, 1000);
      }
    }
  }

  componentDidMount() {
    this.props.fetchTraineeList(0, 50);
    this.props.fetchWorkingStatusList(0, 50);
    this.props.fetchDepartmentList(0, 50);
    if (this.props.match.params.id) {
      this.props.fetchTrainee(parseInt(this.props.match.params.id));
    }
  }

  handleChange = e => {
    const { name, value } = e.target;
    this.setState(prevState => ({
      traineeModel: {
        ...prevState.traineeModel,
        [name]: value
      },
      isErrorTrainee: false
    }));
  };

  handleClickShowPassword = () => {
    this.setState(state => ({
      showPassword: !state.showPassword
    }));
  };

  handleClick = () => {
    this.props.createTrainee(this.state.traineeModel);
  };

  handleClickUpdate = () => {
    this.props.updateTrainee(this.state.traineeModel);
  };

  handleChangeTrainee = dataId => {
    this.props.fetchTrainee(dataId);
  };

  render() {
    const { classes } = this.props;
    const {
      departments,
      workingStatuses,
      gender,
      traineeColumns,
      traineeList,
      loading,
      isErrorTrainee,
      traineeModel,
      success
    } = this.state;
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });

    return (
      <div className={classes.root} id="addTrainee">
        <Grid container spacing={3}>
          <Header pageHeader={translate('Trainees')} />
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <div />
            <a href="#trainees" className={classes.linkStyle}>
              {`${translate('Trainee')} ${translate('Add')}`}
            </a>
          </Grid>
          <Grid item xs={12} sm={12}>
            <DropDown
              error={isErrorTrainee}
              data={workingStatuses}
              labelName={translate('WorkingStatus')}
              describable
              onChange={this.handleChange}
              name="workingStatusId"
              value={traineeModel.workingStatusId}
            >
              <WorkingStatuses data={workingStatuses} />
            </DropDown>
            <Input
              error={isErrorTrainee}
              labelName={translate('Name')}
              inputType="text"
              onChange={this.handleChange}
              name="name"
              value={traineeModel.name}
            />
            <Input
              error={isErrorTrainee}
              labelName={translate('Surname')}
              inputType="text"
              onChange={this.handleChange}
              name="surname"
              value={traineeModel.surname}
            />
            <DropDown
              error={isErrorTrainee}
              data={departments}
              labelName={translate('Department')}
              onChange={this.handleChange}
              name="departmentId"
              value={traineeModel.departmentId}
            />
            <Input
              error={isErrorTrainee}
              labelName={translate('NationalIdentityNumber')}
              inputType="text"
              onChange={this.handleChange}
              name="nationalIdentityNumber"
              value={traineeModel.nationalIdentityNumber}
            />
            <Input
              error={isErrorTrainee}
              labelName={translate('PhoneNumber')}
              inputType="text"
              onChange={this.handleChange}
              name="phoneNumber"
              value={traineeModel.phoneNumber}
            />
            <DropDown
              error={isErrorTrainee}
              data={gender}
              labelName={translate('Gender')}
              onChange={this.handleChange}
              name="gender"
              value={traineeModel.gender}
            />
            <Input
              error={isErrorTrainee}
              labelName={translate('EmailAddress')}
              inputType="text"
              name="email"
              value={traineeModel.email}
              onChange={this.handleChange}
            />
          </Grid>
          <Grid item xs={12} sm={2}>
            <InputLabel className={classes.bootstrapFormLabel}>
              {translate('Password')}
            </InputLabel>
          </Grid>
          <Grid item xs={12} sm={9}>
            <TextField
              margin="dense"
              variant="outlined"
              error={isErrorTrainee && true}
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
          <Grid item xs={12} sm={1} />
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={11} />
          <Grid item xs={12} sm={1}>
            <Button
              className={buttonClassname}
              buttonColor="primary"
              buttonName={
                this.props.match.params.id
                  ? translate('Update')
                  : translate('Save')
              }
              disabled={loading}
              onClick={
                this.props.match.params.id
                  ? this.handleClickUpdate
                  : this.handleClick
              }
            />
            {loading && (
              <CircularProgress
                size={24}
                disableShrink={true}
                className={
                  this.props.match.params.id
                    ? classes.buttonProgressUpdate
                    : classes.buttonProgressSave
                }
              />
            )}
          </Grid>
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12}>
            <a href="#addTrainee" className={classes.linkStyle}>
              {translate('Trainees')}
            </a>
          </Grid>
          <Grid item xs={12} sm={12}>
            <div id="trainees">
              <Table
                columns={traineeColumns}
                data={traineeList}
                isSelected={selected => {}}
                url="trainees"
                dataId={dataId => this.handleChangeTrainee(dataId)}
                remove
                update
              >
                <TraineesUpdate />
              </Table>
            </div>
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    errorDepartmentList,
    isDepartmentListLoading,
    departmentList
  } = state.departmentList;

  const {
    isTraineeListLoading,
    errorTraineeList,
    trainees
  } = state.traineeList;

  const {
    isWorkingStatusListLoading,
    workingStatusList,
    errorWorkingStatusList
  } = state.workingStatusList;

  const {
    errorCreateTrainee,
    isCreateTraineeLoading,
    newTrainee
  } = state.createTrainee;

  const { isTraineeLoading, errorTrainee, trainee } = state.trainee;

  const {
    isUpdateTraineeLoading,
    errorUpdateTrainee,
    updatedTrainee
  } = state.updateTrainee;

  return {
    isTraineeListLoading,
    errorTraineeList,
    trainees,
    errorDepartmentList,
    isDepartmentListLoading,
    departmentList,
    isWorkingStatusListLoading,
    workingStatusList,
    errorWorkingStatusList,
    errorCreateTrainee,
    isCreateTraineeLoading,
    newTrainee,
    isTraineeLoading,
    errorTrainee,
    trainee,
    isUpdateTraineeLoading,
    errorUpdateTrainee,
    updatedTrainee
  };
};

const mapDispatchToProps = {
  fetchTraineeList,
  fetchDepartmentList,
  fetchWorkingStatusList,
  createTrainee,
  fetchTrainee,
  updateTrainee
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Trainee));
