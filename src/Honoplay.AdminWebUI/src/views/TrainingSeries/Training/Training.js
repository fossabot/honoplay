import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import { Grid, Divider, CircularProgress } from '@material-ui/core';
import BreadCrumbs from '../../../components/BreadCrumbs/BreadCrumbs';
import Button from '../../../components/Button/ButtonComponent';
import Style from '../../Style';
import Input from '../../../components/Input/InputTextComponent';
import DropDown from '../../../components/Input/DropDownInputComponent';

import { connect } from 'react-redux';
import { createTraining } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training';

class Training extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
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

  trainingModel = {
    createTrainingModels: []
  };

  componentDidUpdate(prevProps) {
    const {
      isCreateTrainingLoading,
      newCreateTraining,
      errorCreateTraining
    } = this.props;

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
  }

  handleClick = () => {
    this.trainingModel.createTrainingModels = this.state.createTrainingModels;
    this.props.createTraining(this.trainingModel);
  };

  render() {
    const {
      createTrainingModels,
      trainingCategory,
      trainingError,
      trainingLoading
    } = this.state;
    const { classes } = this.props;

    this.state.createTrainingModels.map((training, id) => {
      training.trainingSeriesId = this.trainingSeriesId;
    });

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={11}>
            <BreadCrumbs />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              buttonColor="primary"
              buttonName={translate('Save')}
              onClick={this.handleClick}
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
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
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

  return {
    isCreateTrainingLoading,
    newCreateTraining,
    errorCreateTraining
  };
};

const mapDispatchToProps = {
  createTraining
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Training));
