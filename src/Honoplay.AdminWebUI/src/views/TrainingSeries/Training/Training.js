import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import moment from 'moment';
import classNames from 'classnames';
import { withStyles } from '@material-ui/core/styles';
import { Grid, Divider, CircularProgress } from '@material-ui/core';
import BreadCrumbs from '../../../components/BreadCrumbs/BreadCrumbs';
import Button from '../../../components/Button/ButtonComponent';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';

import { connect } from 'react-redux';
import {
  createTraining,
  fetchTraining,
  updateTraining
} from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training';

class Training extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      success: false,
      trainingLoading: false,
      trainingError: false,
      trainingCategory: [{ id: 1, name: 'Yazılım' }],
      createTrainingModels: [
        {
          trainingSeriesId: '',
          trainingCategoryId: '',
          name: '',
          description: '',
          beginDateTime: '',
          endDateTime: ''
        }
      ]
    };
  }

  trainingSeriesId = localStorage.getItem('trainingSeriesId');
  trainingId = localStorage.getItem('trainingId');

  trainingModel = {
    createTrainingModels: []
  };

  componentDidUpdate(prevProps) {
    const {
      isCreateTrainingLoading,
      newCreateTraining,
      errorCreateTraining,
      isTrainingLoading,
      training,
      errorTraining,
      isUpdateTrainingLoading,
      updateTraining,
      errorUpdateTraining
    } = this.props;

    if (prevProps.isTrainingLoading && !isTrainingLoading && training) {
      if (!errorTraining) {
        this.setState({
          createTrainingModels: training.items
        });
      }
    }

    if (!prevProps.isCreateTrainingLoading && isCreateTrainingLoading) {
      this.setState({
        trainingLoading: true
      });
    }
    if (!prevProps.errorCreateTraining && errorCreateTraining) {
      this.setState({
        trainingError: true,
        trainingLoading: false
      });
    }
    if (
      prevProps.isCreateTrainingLoading &&
      !isCreateTrainingLoading &&
      newCreateTraining
    ) {
      if (!errorCreateTraining) {
        this.setState({
          trainingLoading: false,
          trainingError: false
        });
        this.props.history.push(
          `/admin/trainingseries/${this.props.match.params.trainingSeriesName}`
        );
      }
    }

    if (!prevProps.isUpdateTrainingLoading && isUpdateTrainingLoading) {
      this.setState({
        trainingLoading: true
      });
    }
    if (!prevProps.errorUpdateTraining && errorUpdateTraining) {
      this.setState({
        trainingError: true,
        trainingLoading: false,
        success: false
      });
    }
    if (
      prevProps.isUpdateTrainingLoading &&
      !isUpdateTrainingLoading &&
      updateTraining
    ) {
      if (!errorUpdateTraining) {
        this.setState({
          trainingError: false,
          trainingLoading: false,
          success: true
        });
        setTimeout(() => {
          this.setState({ success: false });
        }, 1000);
      }
    }
  }

  componentDidMount() {
    this.props.fetchTraining(this.trainingId);
  }

  handleClick = () => {
    this.trainingModel.createTrainingModels = this.state.createTrainingModels;
    this.props.createTraining(this.trainingModel);
  };

  handleUpdate = () => {
    this.state.createTrainingModels.map((training, id) =>
      this.props.updateTraining(training)
    );
  };

  render() {
    const {
      createTrainingModels,
      trainingCategory,
      trainingError,
      trainingLoading,
      success
    } = this.state;
    const { classes, update } = this.props;

    this.state.createTrainingModels.map((training, id) => {
      training.trainingSeriesId = this.trainingSeriesId;
    });

    const buttonClassname = classNames({
      [classes.buttonSuccess]: success
    });

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          {update ? (
            <Grid item xs={12} sm={11} />
          ) : (
            <Grid item xs={12} sm={11}>
              <BreadCrumbs />
            </Grid>
          )}
          <Grid item xs={12} sm={1}>
            <Button
              buttonColor="primary"
              className={buttonClassname}
              buttonName={update ? translate('Update') : translate('Save')}
              onClick={update ? this.handleUpdate : this.handleClick}
              disabled={trainingLoading}
            />
            {trainingLoading && (
              <CircularProgress
                size={24}
                disableShrink={true}
                className={classes.buttonProgressSave}
              />
            )}
          </Grid>
          {!update && (
            <Grid item xs={12} sm={12}>
              <Divider />
            </Grid>
          )}
          <Grid item xs={12} sm={12} />
          <Grid item xs={12} sm={12}>
            {createTrainingModels.map((training, id) => (
              <Grid item xs={12} sm={12} key={id}>
                <Input
                  error={trainingError}
                  labelName={translate('TrainingName')}
                  inputType="text"
                  onChange={e => {
                    training.name = e.target.value;
                    this.setState({
                      trainingError: false
                    });
                  }}
                  value={training.name}
                />
                <DropDown
                  error={trainingError}
                  labelName={translate('TrainingCategory')}
                  data={trainingCategory}
                  onChange={e => {
                    training.trainingCategoryId = e.target.value;
                    this.setState({
                      trainingError: false
                    });
                  }}
                  value={training.trainingCategoryId}
                />
                <Input
                  error={trainingError}
                  labelName={translate('BeginDate')}
                  inputType="date"
                  onChange={e => {
                    training.beginDateTime = e.target.value;
                    this.setState({
                      trainingError: false
                    });
                  }}
                  value={moment(training.beginDateTime).format('YYYY-MM-DD')}
                />
                <Input
                  error={trainingError}
                  labelName={translate('EndDate')}
                  inputType="date"
                  onChange={e => {
                    training.endDateTime = e.target.value;
                    this.setState({
                      trainingError: false
                    });
                  }}
                  value={moment(training.endDateTime).format('YYYY-MM-DD')}
                />
                <Input
                  error={trainingError}
                  multiline
                  labelName={translate('Description')}
                  inputType="text"
                  onChange={e => {
                    training.description = e.target.value;
                    this.setState({
                      trainingError: false
                    });
                  }}
                  value={training.description}
                />
              </Grid>
            ))}
          </Grid>
        </Grid>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isCreateTrainingLoading,
    createTraining,
    errorCreateTraining
  } = state.createTraining;

  let newCreateTraining = createTraining;

  const { isTrainingLoading, training, errorTraining } = state.training;

  const {
    isUpdateTrainingLoading,
    updateTraining,
    errorUpdateTraining
  } = state.updateTraining;

  return {
    isCreateTrainingLoading,
    newCreateTraining,
    errorCreateTraining,
    isTrainingLoading,
    training,
    errorTraining,
    isUpdateTrainingLoading,
    updateTraining,
    errorUpdateTraining
  };
};

const mapDispatchToProps = {
  createTraining,
  fetchTraining,
  updateTraining
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Training));
