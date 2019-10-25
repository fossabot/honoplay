import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import classNames from 'classnames';
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
import Header from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import Input from '../../components/Input/InputTextComponent';
import DropDown from '../../components/Input/DropDownInputComponent';
import Department from '../Profile/Department';
import Table from 'material-table';

import { connect } from 'react-redux';
import {
  createTrainer,
  fetchTrainersList,
  fetchTrainer,
  updateTrainer
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Trainer';
import { fetchDepartmentList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Department';
import { fetchProfessionList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Profession';

import { departmentToString } from '../../helpers/Converter';
import Profession from './Profession';

class Trainers extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      trainerId: null,
      departments: [],
      trainer: [],
      professions: [],
      departmentListError: false,
      trainerError: false,
      loadingTrainer: false,
      trainerColumns: [
        { title: translate('Name'), field: 'name' },
        { title: translate('Surname'), field: 'surname' },
        { title: translate('Department'), field: 'departmentId' },
        { title: translate('PhoneNumber'), field: 'phoneNumber' },
        { title: translate('EmailAddress'), field: 'email' }
      ],
      success: false
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
      professionList,
      isTrainerLoading,
      errorTrainer,
      trainer,
      isUpdateTrainerLoading,
      errorUpdateTrainer,
      updateTrainer
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
        this.setState({
          trainer: trainersList.items
        });
      }
    }

    if (prevProps.isTrainerLoading && !isTrainerLoading) {
      this.setState({
        loading: true
      });
    }
    if (prevProps.isTrainerLoading && !isTrainerLoading && trainer) {
      if (!errorTrainer) {
        this.trainerModel = trainer.items[0];
      }
    }
    if (!prevProps.isUpdateTrainerLoading && isUpdateTrainerLoading) {
      this.setState({
        loadingTrainer: true
      });
    }
    if (!prevProps.errorUpdateTrainer && errorUpdateTrainer) {
      this.setState({
        trainerError: true,
        loadingTrainer: false,
        success: false
      });
    }
    if (
      prevProps.isUpdateTrainerLoading &&
      !isUpdateTrainerLoading &&
      updateTrainer
    ) {
      if (!errorUpdateTrainer) {
        this.props.fetchTrainersList(0, 50);
        this.setState({
          trainerError: false,
          loadingTrainer: false,
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
    this.props.fetchDepartmentList(0, 50);
    this.props.fetchProfessionList(0, 50);
    if (this.props.match.params.id) {
      this.props.fetchTrainer(this.props.match.params.id);
    }
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
    this.props.createTrainer(this.trainerModel);
  };

  handleClickUpdate = () => {
    this.props.updateTrainer(this.trainerModel);
  };

  handleClickShowPassword = () => {
    this.setState(state => ({
      showPassword: !state.showPassword
    }));
  };

  handleChangeTrainer = id => {
    this.props.fetchTrainer(id);
    this.props.history.push(`/trainers/${id}`);
  };

  render() {
    const {
      departments,
      loadingTrainer,
      trainerError,
      trainer,
      trainerColumns,
      professions,
      success
    } = this.state;
    const { classes } = this.props;
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });

    departmentToString(departments, trainer);

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={11}>
            <Header pageHeader={translate('Trainers')} />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              className={buttonClassname}
              buttonColor="primary"
              buttonName={
                this.props.match.params.id
                  ? translate('Update')
                  : translate('Save')
              }
              disabled={loadingTrainer}
              onClick={
                this.props.match.params.id
                  ? this.handleClickUpdate
                  : this.handleClick
              }
            />
            {loadingTrainer && (
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
              htmlFor="name"
              id="name"
            />
            <Input
              error={trainerError}
              onChange={this.handleChange}
              labelName={translate('Surname')}
              inputType="text"
              name="surname"
              value={this.trainerModel.surname}
              htmlFor="surname"
              id="surname"
            />
            <DropDown
              error={trainerError}
              onChange={this.handleChange}
              data={departments}
              labelName={translate('Department')}
              name="departmentId"
              value={this.trainerModel.departmentId}
              describable
              htmlFor="department"
              id="department"
            >
              <Department describable />
            </DropDown>
            <DropDown
              error={trainerError}
              onChange={this.handleChange}
              data={professions}
              labelName={translate('TrainerExpertise')}
              name="professionId"
              value={this.trainerModel.professionId}
              describable
              htmlFor="trainerExpertise"
              id="trainerExpertise"
            >
              <Profession />
            </DropDown>
            <Input
              error={trainerError}
              onChange={this.handleChange}
              labelName={translate('PhoneNumber')}
              inputType="text"
              name="phoneNumber"
              value={this.trainerModel.phoneNumber}
              htmlFor="phoneNumber"
              id="phoneNumber"
            />
            <Input
              error={trainerError}
              onChange={this.handleChange}
              labelName={translate('EmailAddress')}
              inputType="text"
              name="email"
              value={this.trainerModel.email}
              htmlFor="emailAddress"
              id="emailAddress"
            />
          </Grid>
          <Grid item xs={12} sm={2} className={classes.center}>
            <InputLabel
              htmlFor="password"
              className={classes.bootstrapFormLabel}
            >
              {translate('Password')}
            </InputLabel>
          </Grid>
          <Grid item xs={12} sm={9}>
            <TextField
              id="password"
              margin="dense"
              variant="outlined"
              error={trainerError && true}
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
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Table
              title={translate('Trainers')}
              columns={trainerColumns}
              data={trainer}
              actions={[
                {
                  icon: 'edit',
                  onClick: (event, rowData) =>
                    this.handleChangeTrainer(rowData.id)
                }
              ]}
              options={{
                actionsColumnIndex: -1
              }}
              localization={{
                pagination: {
                  labelDisplayedRows: '{from}-{to} / {count}',
                  labelRowsSelect: translate('NumberOfRows')
                },
                toolbar: {
                  searchPlaceholder: translate('Search')
                },
                header: {
                  actions: translate('Edit')
                },
                body: {
                  emptyDataSourceMessage: translate('NoRecordsToDisplay')
                }
              }}
              options={{
                actionsColumnIndex: -1
              }}
            />
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

  const { isTrainerLoading, errorTrainer, trainer } = state.trainer;

  const {
    isUpdateTrainerLoading,
    errorUpdateTrainer,
    updateTrainer
  } = state.updateTrainer;

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
    errorProfessionList,
    isTrainerLoading,
    errorTrainer,
    trainer,
    isUpdateTrainerLoading,
    errorUpdateTrainer,
    updateTrainer
  };
};

const mapDispatchToProps = {
  createTrainer,
  fetchTrainersList,
  fetchDepartmentList,
  fetchProfessionList,
  fetchTrainer,
  updateTrainer
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Trainers));
