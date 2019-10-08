import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import {
  Grid,
  Divider,
  CircularProgress,
  TextField,
  InputAdornment,
  IconButton,
  InputLabel
} from '@material-ui/core';
import Style from '../Style';
import Visibility from '@material-ui/icons/Visibility';
import VisibilityOff from '@material-ui/icons/VisibilityOff';
import Typography from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Table from '../../components/Table/TableComponent';

import { connect } from 'react-redux';
import {
  createTrainer,
  fetchTrainersList
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainer';
import { fetchDepartmentList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Department';
import { fetchProfessionList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Profession';

import { departmentToString } from '../../helpers/Converter';
import TrainersUpdate from './TrainersUpdate';
import Profession from './Profession';

class Trainers extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      departments: [],
      trainer: [],
      professions: [],
      departmentListError: false,
      trainerError: false,
      loadingTrainer: false,
      trainerColumns: [
        { title: translate('Name'), field: 'name' },
        { title: translate('Surname'), field: 'surname' },
        { title: translate('NationalIdentityNumber'), field: 'email' },
        { title: translate('PhoneNumber'), field: 'phoneNumber' },
        { title: translate('Department'), field: 'departmentId' }
      ]
    };
  }

  trainerModel = {
    name: '',
    surname: '',
    email: '',
    phoneNumber: '',
    departmentId: '',
    professionId: '',
    password: ''
  };

  componentDidUpdate(prevProps) {
    const {
      departmentList,
      isDepartmentListLoading,
      errorDepartmentList,
      isCreateTrainerLoading,
      createTrainer,
      errorCreateTrainer,
      isTrainerListLoading,
      errorTrainerList,
      trainersList,
      isProfessionListLoading,
      professionList
    } = this.props;

    if (!prevProps.errorDepartmentList && errorDepartmentList) {
      this.setState({
        departmentListError: true
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

    if (
      prevProps.isProfessionListLoading &&
      !isProfessionListLoading &&
      professionList
    ) {
      this.setState({
        professions: professionList.items
      });
    }
    if (!prevProps.isCreateTrainerLoading && isCreateTrainerLoading) {
      this.setState({
        loadingTrainer: true
      });
    }
    if (!prevProps.errorCreateTrainer && errorCreateTrainer) {
      this.setState({
        trainerError: true,
        loadingTrainer: false
      });
    }
    if (
      prevProps.isCreateTrainerLoading &&
      !isCreateTrainerLoading &&
      createTrainer
    ) {
      this.props.fetchTrainersList(0, 50);
      if (!errorCreateTrainer) {
        this.setState({
          trainerError: false,
          loadingTrainer: false
        });
      }
    }
    if (
      prevProps.isTrainerListLoading &&
      !isTrainerListLoading &&
      trainersList
    ) {
      if (!errorTrainerList) {
        if (departmentList && trainersList) {
          departmentToString(departmentList.items, trainersList.items);
        }
        this.setState({
          trainer: trainersList.items
        });
      }
    }
  }

  componentDidMount() {
    this.props.fetchTrainersList(0, 50);
    this.props.fetchDepartmentList(0, 50);
    this.props.fetchProfessionList(0, 50);
  }

  handleChange = e => {
    const { name, value } = e.target;
    this.trainerModel[name] = value;
    this.setState({
      departmentListError: false,
      trainerError: false
    });
  };

  handleClick = () => {
    const { createTrainer } = this.props;
    createTrainer(this.trainerModel);
  };

  handleClickShowPassword = () => {
    this.setState(state => ({
      showPassword: !state.showPassword
    }));
  };

  render() {
    const {
      departments,
      loadingTrainer,
      trainerError,
      trainer,
      trainerColumns,
      professions
    } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Typography pageHeader={translate('Trainers')} />
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            <Input
              error={trainerError}
              onChange={this.handleChange}
              labelName={translate('Name')}
              inputType="text"
              name="name"
              value={this.trainerModel.name}
            />
            <Input
              error={trainerError}
              onChange={this.handleChange}
              labelName={translate('Surname')}
              inputType="text"
              name="surname"
              value={this.trainerModel.surname}
            />
            <DropDown
              error={trainerError}
              onChange={this.handleChange}
              data={departments}
              labelName={translate('Department')}
              name="departmentId"
              value={this.trainerModel.departmentId}
            />
            <DropDown
              error={trainerError}
              onChange={this.handleChange}
              data={professions}
              labelName={translate('TrainerExpertise')}
              name="professionId"
              value={this.trainerModel.professionId}
              describable
            >
              <Profession />
            </DropDown>
            <Input
              error={trainerError}
              onChange={this.handleChange}
              labelName={translate('EmailAddress')}
              inputType="text"
              name="email"
              value={this.trainerModel.email}
            />
            <Input
              error={trainerError}
              onChange={this.handleChange}
              labelName={translate('PhoneNumber')}
              inputType="text"
              name="phoneNumber"
              value={this.trainerModel.phoneNumber}
            />
          </Grid>
          <Grid item xs={12} sm={3}>
            <InputLabel className={classes.bootstrapFormLabel}>
              {translate('Password')}
            </InputLabel>
          </Grid>
          <Grid item xs={12} sm={9}>
            <TextField
              margin="dense"
              variant="outlined"
              error={trainerError && true}
              className={classes.passwordInput}
              name="password"
              type={this.state.showPassword ? 'text' : 'password'}
              onChange={this.handleChange}
              value={this.trainerModel.password}
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
          <Grid item xs={12} sm={11} />
          <Grid item xs={12} sm={1}>
            <Button
              buttonColor="primary"
              buttonName={translate('Save')}
              disabled={loadingTrainer}
              onClick={this.handleClick}
            />
            {loadingTrainer && (
              <CircularProgress
                size={24}
                disableShrink={true}
                className={classes.buttonProgress}
              />
            )}
          </Grid>
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Table
              columns={trainerColumns}
              data={trainer}
              isSelected={selected => {}}
              remove
              update
            >
              <TrainersUpdate />
            </Table>
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isCreateTrainerLoading,
    createTrainer,
    errorCreateTrainer
  } = state.createTrainer;

  const {
    errorDepartmentList,
    isDepartmentListLoading,
    departmentList
  } = state.departmentList;

  const {
    isTrainerListLoading,
    errorTrainerList,
    trainersList
  } = state.trainersList;

  const {
    isProfessionListLoading,
    professionList,
    errorProfessionList
  } = state.professionList;

  return {
    isCreateTrainerLoading,
    createTrainer,
    errorCreateTrainer,
    errorDepartmentList,
    isDepartmentListLoading,
    departmentList,
    isTrainerListLoading,
    errorTrainerList,
    trainersList,
    isProfessionListLoading,
    professionList,
    errorProfessionList
  };
};

const mapDispatchToProps = {
  createTrainer,
  fetchTrainersList,
  fetchDepartmentList,
  fetchProfessionList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Trainers));
