import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Card from '../../components/Card/CardComponents';
import Typography from '../../components/Typography/TypographyComponent';

import { connect } from "react-redux";
import { fetchTrainingSeries } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/TrainingSeries";
import { fetchTrainingListByTrainingSeriesId } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training";


class TrainingSeriesUpdate extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      trainingSeries: {
        createTrainingSeriesModels: [
          {
            name: ''
          }
        ]
      },
      trainingList: [],
      trainingListError: false
    }
  }

  trainingSeriesId = localStorage.getItem("trainingSeriesId");

  componentDidUpdate(prevProps) {
    const {
      isTrainingSeriesLoading,
      trainingSeries,
      errorTrainingSeries,
      isTrainingListByTrainingSeriesIdLoading,
      TrainingListByTrainingSeriesId,
      errorTrainingListByTrainingSeriesId
    } = this.props;

    if (prevProps.isTrainingSeriesLoading && !isTrainingSeriesLoading) {
      this.setState({
        loadingTrainer: true
      })
    }
    if (prevProps.isTrainingSeriesLoading && !isTrainingSeriesLoading && trainingSeries) {
      if (!errorTrainingSeries) {
        this.setState({
          trainingSeries: trainingSeries.items[0]
        })
      }
    }
    if (!prevProps.errorTrainingListByTrainingSeriesId && errorTrainingListByTrainingSeriesId) {
      this.setState({
        trainingListError: true
      })
    }
    if (prevProps.isTrainingListByTrainingSeriesIdLoading && !isTrainingListByTrainingSeriesIdLoading && TrainingListByTrainingSeriesId) {
      this.setState({
        trainingList: TrainingListByTrainingSeriesId.items
      })
    }
  }

  componentDidMount() {
    this.props.fetchTrainingSeries(this.trainingSeriesId);
    this.props.fetchTrainingListByTrainingSeriesId(this.trainingSeriesId);
  }

  handleClick = () => {
    this.props.history.push(`/honoplay/trainingseriesdetail/training`);
  }

  render() {
    const { trainingSeries, trainingList } = this.state;
    const { classes } = this.props;

    return (

      <div className={classes.root}>
        <Grid container spacing={24}>
          <Grid item xs={12} sm={12}>
            <Typography
              pageHeader={trainingSeries.name}
            />
          </Grid>
          <Grid item xs={12} sm={3}>
            <CardButton
              cardName={translate('CreateTraining')}
              cardDescription={translate('YouCanCreateDifferentTrainingsForEachTrainingSeries')}
              onClick={this.handleClick}
              forTraining
              iconName="graduation-cap"
            />
          </Grid>
          <Grid item xs={12} sm={9}>
            <Card
              data={trainingList}
              titleName={translate('Update')}
              id={id => {

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
    isTrainingSeriesLoading,
    trainingSeries,
    errorTrainingSeries
  } = state.trainingSeries;

  const {
    isTrainingListByTrainingSeriesIdLoading,
    TrainingListByTrainingSeriesId,
    errorTrainingListByTrainingSeriesId
  } = state.trainingListByTrainingSeriesId;

  return {
    isTrainingSeriesLoading,
    trainingSeries,
    errorTrainingSeries,
    isTrainingListByTrainingSeriesIdLoading,
    TrainingListByTrainingSeriesId,
    errorTrainingListByTrainingSeriesId
  };
};

const mapDispatchToProps = {
  fetchTrainingSeries,
  fetchTrainingListByTrainingSeriesId
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(TrainingSeriesUpdate));