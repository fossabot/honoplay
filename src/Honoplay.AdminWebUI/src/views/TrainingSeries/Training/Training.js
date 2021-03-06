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
import { fetchTrainingCategoryList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/TrainingCategory';

class Training extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      success: false,
      trainingLoading: false,
      trainingError: false,
      trainingCategory: [],
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
      errorUpdateTraining,
      isTrainingCategoryListLoading,
      trainingCategories,
      errorTrainingCategoryList
    } = this.props;

    if (
      prevProps.isTrainingCategoryListLoading &&
      !isTrainingCategoryListLoading &&
      trainingCategories
    ) {
      if (!errorTrainingCategoryList) {
        this.setState({
          trainingCategory: trainingCategories.items
        });
      }
    }
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
          `/trainingseries/${this.props.match.params.trainingSeriesId}`
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
    this.props.fetchTrainingCategoryList(0, 50);
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
                  value={update && training.name}
                  htmlFor="trainingName"
                  id="trainingName"
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
                  value={update && training.trainingCategoryId}
                  htmlFor="trainingCategory"
                  id="trainingCategory"
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
                  value={
                    update &&
                    moment(training.beginDateTime).format('YYYY-MM-DD')
                  }
                  htmlFor="beginDate"
                  id="beginDate"
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
                  value={
                    update && moment(training.endDateTime).format('YYYY-MM-DD')
                  }
                  htmlFor="endDate"
                  id="endDate"
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
                  value={update && training.description}
                  htmlFor="description"
                  id="description"
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

  const {
    isTrainingCategoryListLoading,
    trainingCategories,
    errorTrainingCategoryList
  } = state.trainingCategoryList;

  return {
    isCreateTrainingLoading,
    newCreateTraining,
    errorCreateTraining,
    isTrainingLoading,
    training,
    errorTraining,
    isUpdateTrainingLoading,
    updateTraining,
    errorUpdateTraining,
    isTrainingCategoryListLoading,
    trainingCategories,
    errorTrainingCategoryList
  };
};

const mapDispatchToProps = {
  createTraining,
  fetchTraining,
  updateTraining,
  fetchTrainingCategoryList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Training));
