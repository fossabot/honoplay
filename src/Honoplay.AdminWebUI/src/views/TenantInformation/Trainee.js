import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid, Divider} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Table from '../../components/Table/TableComponent';

import { connect } from "react-redux";
import { fetchTraineeList } from "@omegabigdata/honoplay-redux-helper/Src/actions/Trainee";
import { fetchDepartmentList } from "@omegabigdata/honoplay-redux-helper/Src/actions/Department";
import { values } from 'regenerator-runtime';

class Trainee extends React.Component {

constructor(props) {
  super(props);
  this.state = {
    trainees: [],
    departments: [],
    workingStatus: [
        { id: 1, name: 'Çalışan',  },
        { id: 2, name: 'Aday',  },
        { id: 3, name: 'Stajyer',  },
    ],
    gender: [
      { id: 0, name: 'Erkek', },
      { id: 1, name: 'Kadın', }
    ],
    traineeColumns: [
      { title: "Ad", field: "name"},
      { title: "Soyad", field: "surname"},
      { title: "TCKN", field: "nationalIdentityNumber"},
      { title: "Cep Telefonu", field: "phoneNumber"},
      { title: "Cinsiyet", field: "gender"}
    ],
  };
}

traineeModel = {
  name: null,
  surname: null,
  nationalIdentityNumber: null,
  phoneNumber: null,
  gender: null,
  workingStatusId: null,
  departmentId: null
}

componentDidUpdate(prevProps) {
  const { isTraineeListLoading, errorTraineeList, trainees, 
    errorDepartmentList, isDepartmentListLoading, departmentList } = this.props;
  if( prevProps.isTraineeListLoading && !isTraineeListLoading && trainees ) {
    trainees.items.map(trainee => {
      this.setState({
        trainees: this.state.trainees.push(trainee),
      })   
    })
  }
  if( prevProps.isDepartmentListLoading && !isDepartmentListLoading && departmentList ) {
    departmentList.items.map(department => {
      this.setState({
        departments: this.state.departments.push(department)
      })
    })
  }
}

componentDidMount() {
  this.props.fetchTraineeList(0,50);
  this.props.fetchDepartmentList(0,50);
}

render() {
  const { classes, isError } = this.props;
    return (
        <div className={classes.root} id="kisiEkle">
        <Grid container spacing={40}>
          <Grid item xs={12} sm={12}/>
          <Grid item xs={12} sm={12}>
          <div />
          <a href="#kisiler" className={classes.linkStyle}>
           {`${translate('Trainee')} ${translate('Add')}`}
          </a>
          </Grid>
          <Grid item xs={12} sm={12}>
          
            <DropDown 
              error = {isError}
              data={this.state.workingStatus}                        
              labelName={translate('WorkingStatus')}                   
              describable
              onChange = { value => {
                this.traineeModel.workingStatusId = value;
                this.props.basicTraineeModel(this.traineeModel);
              }}
            />
            <Input 
              error = {isError}
              labelName={translate('Name')}                                    
              inputType="text"
              onChange = { value => {
                this.traineeModel.name = value;
                this.props.basicTraineeModel(this.traineeModel);
              }}
            />
            <Input 
              error = {isError}
              labelName={translate('Surname')}                                     
              inputType="text"
              onChange = { value => {
                this.traineeModel.surname = value;
                this.props.basicTraineeModel(this.traineeModel);
              }}
            />
            <DropDown 
              error = {isError}
              data={this.state.departments}                       
              labelName={translate('Department')}   
              onChange = { value => {
                this.traineeModel.departmentId = value;
                this.props.basicTraineeModel(this.traineeModel);
              }}
            />
            <Input 
              error = {isError}
              labelName={translate('NationalIdentityNumber')}                                   
              inputType="text"
              onChange = { value => {
                this.traineeModel.nationalIdentityNumber = value;
                this.props.basicTraineeModel(this.traineeModel);
              }}
            />
            <Input 
              error = {isError}
              labelName={translate('PhoneNumber')}                                        
              inputType="text"
              onChange = { value => {
                this.traineeModel.phoneNumber = value;
                this.props.basicTraineeModel(this.traineeModel);
              }}
            />
            <DropDown 
              error = {isError}
              data={this.state.gender}                     
              labelName={translate('Gender')} 
              onChange = { value => {
                this.traineeModel.gender = value;
                this.props.basicTraineeModel(this.traineeModel);
              }}
            />
          
          </Grid>
          <Grid item xs={12} sm={12}><Divider/></Grid>
          <Grid item xs={12} sm={12}>
          <a href='#kisiEkle' className={classes.linkStyle}>
            {translate('Trainees')}
          </a>
          </Grid>
          <Grid item xs={12} sm={12}>  
            <div id="kisiler">
              <Table 
                columns={this.state.traineeColumns}                    
                data={this.state.trainees}
              />
            </div>
          </Grid>
        </Grid>
        </div>
        );
    }
}

const mapStateToProps = state => {
  const { errorDepartmentList, isDepartmentListLoading, departmentList } = state.departmentList;
  const { isTraineeListLoading, errorTraineeList, trainees } = state.traineeList;
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