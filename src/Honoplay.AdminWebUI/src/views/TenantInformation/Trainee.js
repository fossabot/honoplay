import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
  Grid,
  Divider,
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
import Table from '../../components/Table/TableComponent';

import TraineesUpdate from './TraineesUpdate';
import WorkingStatuses from './WorkingStatus';

import { connect } from "react-redux";
import { fetchTraineeList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainee";
import { fetchDepartmentList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Department";
import { fetchWorkingStatusList } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/WorkingStatus";
import { genderToString } from '../../helpers/Converter';

class Trainee extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      traineeList: [],
      departments: [],
      workingStatuses: [],
      gender: [
        { id: 0, name: 'Erkek', },
        { id: 1, name: 'Kadın', }
      ],
      traineeColumns: [
        { title: translate('Name'), field: "name" },
        { title: translate('Surname'), field: "surname" },
        { title: translate('NationalIdentityNumber'), field: "nationalIdentityNumber" },
        { title: translate('PhoneNumber'), field: "phoneNumber" },
        { title: translate('Gender'), field: "gender" }
      ],
    };
  }

  traineeModel = {
    name: '',
    surname: '',
    nationalIdentityNumber: '',
    phoneNumber: '',
    gender: '',
    workingStatusId: '',
    departmentId: '',
    password: '',
    email:''
  }

  componentDidUpdate(prevProps) {
    const {
      isTraineeListLoading,
      errorTraineeList,
      trainees,
      isDepartmentListLoading,
      departmentList,
      isWorkingStatusListLoading,
      workingStatusList

    } = this.props;

    if (prevProps.isTraineeListLoading && !isTraineeListLoading && trainees) {
      if (!errorTraineeList) {
        genderToString(trainees.items);
        this.setState({
          traineeList: trainees.items
        })
      }
    }
    if (prevProps.isDepartmentListLoading && !isDepartmentListLoading && departmentList) {
      this.setState({
        departments: departmentList.items
      })
    }
    if (prevProps.isWorkingStatusListLoading && !isWorkingStatusListLoading && workingStatusList) {
      this.setState({
        workingStatuses: workingStatusList.items
      })
    }
  }

  componentDidMount() {
    this.props.fetchTraineeList(0, 50);
    this.props.fetchWorkingStatusList(0, 50);
  }

  handleChange = (e) => {
    const { name, value } = e.target;
    this.traineeModel[name] = value;
    this.props.basicTraineeModel(this.traineeModel);
  }

  render() {
    const {
      classes,
      isErrorTrainee } = this.props;
    const {
      departments,
      workingStatuses,
      gender,
      traineeColumns,
      traineeList } = this.state;

    return (
      <div className={classes.root} id="addTrainee">
        <Grid container spacing={3}>
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
              value={this.traineeModel.workingStatusId}
            >
              <WorkingStatuses data={workingStatuses} />
            </DropDown>
            <Input
              error={isErrorTrainee}
              labelName={translate('Name')}
              inputType="text"
              onChange={this.handleChange}
              name="name"
              value={this.traineeModel.name}
            />
            <Input
              error={isErrorTrainee}
              labelName={translate('Surname')}
              inputType="text"
              onChange={this.handleChange}
              name="surname"
              value={this.traineeModel.surname}
            />
            <DropDown
              error={isErrorTrainee}
              data={departments}
              labelName={translate('Department')}
              onChange={this.handleChange}
              name="departmentId"
              value={this.traineeModel.departmentId}
            />
            <Input
              error={isErrorTrainee}
              labelName={translate('NationalIdentityNumber')}
              inputType="text"
              onChange={this.handleChange}
              name="nationalIdentityNumber"
              value={this.traineeModel.nationalIdentityNumber}
            />
            <Input
              error={isErrorTrainee}
              labelName={translate('PhoneNumber')}
              inputType="text"
              onChange={this.handleChange}
              name="phoneNumber"
              value={this.traineeModel.phoneNumber}
            />
            <DropDown
              error={isErrorTrainee}
              data={gender}
              labelName={translate('Gender')}
              onChange={this.handleChange}
              name="gender"
              value={this.traineeModel.gender}
            />
            <Input
              error={isErrorTrainee}
              labelName={translate('EmailAddress')}
              inputType="text"
              name="email"
              value={this.traineeModel.email}
              onChange={this.handleChange}
            />
          </Grid>
          <Grid item xs={12} sm={3}>
            <InputLabel className={classes.bootstrapFormLabel}>
              {translate('Password')}
            </InputLabel>
          </Grid>
          <Grid item xs={12} sm={9}>
            <TextField
              error={isErrorTrainee && true}
              className={classes.passwordInput}
              name="password"
              type={this.state.showPassword ? 'text' : 'password'}
              onChange={this.handleChange}
              value={this.traineeModel.password}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton
                      onClick={this.handleClickShowPassword}
                    >
                      {this.state.showPassword ? <VisibilityOff /> : <Visibility />}
                    </IconButton>
                  </InputAdornment>
                ),
              }}
            />
          </Grid>
          <Grid item xs={12} sm={12}><Divider /></Grid>
          <Grid item xs={12} sm={12}>
            <a href='#addTrainee'
              className={classes.linkStyle}>
              {translate('Trainees')}
            </a>
          </Grid>
          <Grid item xs={12} sm={12}>
            <div id="trainees">
              <Table
                columns={traineeColumns}
                data={traineeList}
                isSelected={selected => { }}
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

  return {
    isTraineeListLoading,
    errorTraineeList,
    trainees,
    errorDepartmentList,
    isDepartmentListLoading,
    departmentList,
    isWorkingStatusListLoading,
    workingStatusList,
    errorWorkingStatusList
  };
};

const mapDispatchToProps = {
  fetchTraineeList,
  fetchDepartmentList,
  fetchWorkingStatusList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Trainee));