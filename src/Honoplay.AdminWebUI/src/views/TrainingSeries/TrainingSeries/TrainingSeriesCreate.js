import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import classNames from 'classnames';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid, CircularProgress } from '@material-ui/core';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import Button from '../../../components/Button/ButtonComponent';

import { connect } from 'react-redux';
import {
  createTrainingSeries,
  fetchTrainingSeriesList,
  fetchTrainingSeries,
  updateTrainingSeries
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/TrainingSeries';

class TrainingSeriesCreate extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      success: false,
      trainingSeriesError: false,
      loading: false,
      trainingSeries: {
        createTrainingSeriesModels: [
          {
            name: ''
          }
        ]
      }
    };
  }

  trainingSeriesId = localStorage.getItem('trainingSeriesId');

  componentDidUpdate(prevProps) {
    const {
      isCreateTrainingSeriesLoading,
      createTrainingSeries,
      errorCreateTrainingSeries,
      isTrainingSeriesLoading,
      trainingSeries,
      errorTrainingSeries,
      isUpdateTrainingSeriesLoading,
      updateTrainingSeries,
      errorUpdateTrainingSeries
    } = this.props;

    if (
      prevProps.isTrainingSeriesLoading &&
      !isTrainingSeriesLoading &&
      trainingSeries
    ) {
      if (!errorTrainingSeries) {
        this.setState({
          trainingSeries: {
            createTrainingSeriesModels: trainingSeries.items
          }
        });
      }
    }

    if (
      !prevProps.isCreateTrainingSeriesLoading &&
      isCreateTrainingSeriesLoading
    ) {
      this.setState({
        loading: true
      });
    }
    if (!prevProps.errorCreateTrainingSeries && errorCreateTrainingSeries) {
      this.setState({
        trainingSeriesError: true,
        loading: false
      });
    }
    if (
      prevProps.isCreateTrainingSeriesLoading &&
      !isCreateTrainingSeriesLoading &&
      createTrainingSeries
    ) {
      if (!errorCreateTrainingSeries) {
        this.props.fetchTrainingSeriesList(0, 50);
        this.setState({
          loading: false,
          trainingSeriesError: false
        });
      }
    }
    if (
      !prevProps.isUpdateTrainingSeriesLoading &&
      isUpdateTrainingSeriesLoading
    ) {
      this.setState({
        loading: true
      });
    }
    if (!prevProps.errorUpdateTrainingSeries && errorUpdateTrainingSeries) {
      this.setState({
        trainingSeriesError: true,
        loading: false,
        success: false
      });
    }
    if (
      prevProps.isUpdateTrainingSeriesLoading &&
      !isUpdateTrainingSeriesLoading &&
      updateTrainingSeries
    ) {
      if (!errorUpdateTrainingSeries) {
        this.setState({
          trainingSeriesError: false,
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
    this.props.fetchTrainingSeries(this.trainingSeriesId);
  }

  handleClick = () => {
    this.props.createTrainingSeries(this.state.trainingSeries);
  };

  handleUpdate = () => {
    this.state.trainingSeries.createTrainingSeriesModels.map(
      (trainingSeries, id) => this.props.updateTrainingSeries(trainingSeries)
    );
  };

  render() {
    const {
      trainingSeries,
      trainingSeriesError,
      loading,
      success
    } = this.state;
    const { classes, update } = this.props;
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          {update && (
            <>
              <Grid item xs={12} sm={11} />
              <Grid item xs={12} sm={1}>
                <Button
                  buttonColor="primary"
                  buttonName={translate('Update')}
                  onClick={this.handleUpdate}
                  className={buttonClassname}
                  disabled={loading}
                />
                {loading && (
                  <CircularProgress
                    size={24}
                    disableShrink={true}
                    className={classes.buttonProgressUpdate}
                  />
                )}
              </Grid>
              <Grid item xs={12} sm={12} />
            </>
          )}
          {trainingSeries.createTrainingSeriesModels.map(
            (trainingSeries, id) => (
              <Grid item xs={12} sm={12} key={id}>
                <Input
                  error={trainingSeriesError}
                  labelName={translate('TrainingSeriesName')}
                  inputType="text"
                  onChange={e => {
                    trainingSeries.name = e.target.value;
                    this.setState({ trainingSeriesError: false });
                  }}
                  value={update && trainingSeries.name}
                  htmlFor="trainingSeriesName"
                  id="trainingSeriesName"
                />
              </Grid>
            )
          )}
          {!update && (
            <>
              <Grid item xs={12} sm={11} />
              <Grid item xs={12} sm={1}>
                <Button
                  buttonColor="primary"
                  buttonName={translate('Save')}
                  onClick={this.handleClick}
                  disabled={loading}
                />
                {loading && (
                  <CircularProgress
                    size={24}
                    disableShrink={true}
                    className={classes.buttonProgressSave}
                  />
                )}
              </Grid>
            </>
          )}
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isCreateTrainingSeriesLoading,
    createTrainingSeries,
    errorCreateTrainingSeries
  } = state.createTrainingSeries;

  const {
    isTrainingSeriesLoading,
    trainingSeries,
    errorTrainingSeries
  } = state.trainingSeries;

  const {
    isUpdateTrainingSeriesLoading,
    updateTrainingSeries,
    errorUpdateTrainingSeries
  } = state.updateTrainingSeries;

  return {
    isCreateTrainingSeriesLoading,
    createTrainingSeries,
    errorCreateTrainingSeries,
    isTrainingSeriesLoading,
    trainingSeries,
    errorTrainingSeries,
    isUpdateTrainingSeriesLoading,
    updateTrainingSeries,
    errorUpdateTrainingSeries
  };
};

const mapDispatchToProps = {
  createTrainingSeries,
  fetchTrainingSeriesList,
  fetchTrainingSeries,
  updateTrainingSeries
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(TrainingSeriesCreate));
