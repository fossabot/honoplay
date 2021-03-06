import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid, CircularProgress, Divider } from '@material-ui/core';
import { Style, theme } from './Style';
import Input from '../../components/Input/InputTextComponent';
import Button from '../../components/Button/ButtonComponent';
import Chip from '../../components/Chip/ChipComponent';

import { connect } from 'react-redux';
import {
  fetchWorkingStatusList,
  postWorkingStatus
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/WorkingStatus';

class WorkingStatus extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      loadingCreate: false,
      createError: false,
      workingStatuses: []
    };
  }

  workingStatusModel = {
    name: ''
  };

  componentDidUpdate(prevProps) {
    const {
      isWorkingStatusCreateLoading,
      newWorkingStatus,
      errorWorkingStatusCreate,
      isWorkingStatusListLoading,
      newWorkingStatusList
    } = this.props;

    if (
      prevProps.isWorkingStatusListLoading &&
      !isWorkingStatusListLoading &&
      newWorkingStatusList
    ) {
      this.setState({
        workingStatuses: newWorkingStatusList.items
      });
    }

    if (
      !prevProps.isWorkingStatusCreateLoading &&
      isWorkingStatusCreateLoading
    ) {
      this.setState({
        loadingCreate: true
      });
    }
    if (!prevProps.errorWorkingStatusCreate && errorWorkingStatusCreate) {
      this.setState({
        createError: true,
        loadingCreate: false
      });
    }
    if (
      prevProps.isWorkingStatusCreateLoading &&
      !isWorkingStatusCreateLoading &&
      newWorkingStatus
    ) {
      this.props.fetchWorkingStatusList(0, 50);
      if (!errorWorkingStatusCreate) {
        this.setState({
          loadingCreate: false,
          createError: false
        });
      }
    }
  }

  handleChange = e => {
    const { name, value } = e.target;
    this.workingStatusModel[name] = value;
    this.setState({
      createError: false
    });
  };

  handleClick = () => {
    this.props.postWorkingStatus(this.workingStatusModel);
  };

  componentDidMount() {
    this.props.fetchWorkingStatusList(0, 50);
  }

  render() {
    const { classes } = this.props;
    const { loadingCreate, createError, workingStatuses } = this.state;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={11}>
            <Input
              error={createError}
              onChange={this.handleChange}
              labelName={translate('WorkingStatus')}
              inputType="text"
              name="name"
              value={this.workingStatusModel.name}
              htmlFor="workingStatusModal"
              id="workingStatusModal"
            />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              buttonColor="secondary"
              buttonName={translate('Add')}
              onClick={this.handleClick}
              disabled={loadingCreate}
            />
            {loadingCreate && (
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
            <Chip data={workingStatuses}></Chip>
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isWorkingStatusCreateLoading,
    workingStatusCreate,
    errorWorkingStatusCreate
  } = state.workingStatusCreate;

  let newWorkingStatus = workingStatusCreate;

  const {
    isWorkingStatusListLoading,
    workingStatusList,
    errorWorkingStatusList
  } = state.workingStatusList;

  let newWorkingStatusList = workingStatusList;

  return {
    isWorkingStatusCreateLoading,
    newWorkingStatus,
    errorWorkingStatusCreate,
    isWorkingStatusListLoading,
    newWorkingStatusList,
    errorWorkingStatusList
  };
};

const mapDispatchToProps = {
  postWorkingStatus,
  fetchWorkingStatusList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(WorkingStatus));
