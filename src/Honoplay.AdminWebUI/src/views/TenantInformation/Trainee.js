import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
  Grid,
  Divider
} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Table from '../../components/Table/TableComponent';

import { connect } from "react-redux";
import { fetchTraineeList } from "@omegabigdata/honoplay-redux-helper/Src/actions/Trainee";
import { fetchDepartmentList } from "@omegabigdata/honoplay-redux-helper/Src/actions/Department";

class Trainee extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      traineeList: [],
      departments: [],
      workingStatus: [
        { id: 1, name: 'Çalışan', },
        { id: 2, name: 'Aday', },
        { id: 3, name: 'Stajyer', },
      ],
      gender: [
        { id: 0, name: 'Erkek', },
        { id: 1, name: 'Kadın', }
      ],
      traineeColumns: [
        { title: "Ad", field: "name" },
        { title: "Soyad", field: "surname" },
        { title: "TCKN", field: "nationalIdentityNumber" },
        { title: "Cep Telefonu", field: "phoneNumber" },
        { title: "Cinsiyet", field: "gender" }
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
    departmentId: ''
  }

  componentDidUpdate(prevProps) {
    const {
      isTraineeListLoading,
      errorTraineeList,
      trainees,
      errorDepartmentList,
      isDepartmentListLoading,
      departmentList } = this.props;

    if (prevProps.isTraineeListLoading && !isTraineeListLoading && trainees) {
      this.setState({
        traineeList: trainees.items
      })
    }
    if (prevProps.isDepartmentListLoading && !isDepartmentListLoading && departmentList) {
      this.setState({
        departments: departmentList.items
      })
    }
  }

  componentDidMount() {
    this.props.fetchTraineeList(0, 50);
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
      workingStatus,
      gender,
      traineeColumns,
      traineeList } = this.state;
    return (
      <div className={classes.root} id="kisiEkle">
        <Grid container spacing={40}>
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <div />
            <a href="#kisiler" className={classes.linkStyle}>
              {`${translate('Trainee')} ${translate('Add')}`}
            </a>
          </Grid>
          <Grid item xs={12} sm={12}>
            <DropDown
              error={isErrorTrainee}
              data={workingStatus}
              labelName={translate('WorkingStatus')}
              describable
              onChange={this.handleChange}
              name="workingStatusId"
              value={this.traineeModel.workingStatusId}
            />
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
          </Grid>
          <Grid item xs={12} sm={12}><Divider /></Grid>
          <Grid item xs={12} sm={12}>
            <a href='#kisiEkle'
              className={classes.linkStyle}>
              {translate('Trainees')}
            </a>
          </Grid>
          <Grid item xs={12} sm={12}>
            <div id="kisiler">
              <Table
                columns={traineeColumns}
                data={traineeList}
              />
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

  return {
    isTraineeListLoading,
    errorTraineeList,
    trainees,
    errorDepartmentList,
    isDepartmentListLoading,
    departmentList
  };
};

const mapDispatchToProps = {
  fetchTraineeList,
  fetchDepartmentList,
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Trainee));