import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Typography from '../../components/Typography/TypographyComponent';

import { connect } from "react-redux";
import { fetchTrainingSeries } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/TrainingSeries";


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
    }
  }

  trainingSeriesId = this.props.match.params.trainingseriesId;

  componentDidUpdate(prevProps) {
    const {
      isTrainingSeriesLoading,
      trainingSeries,
      errorTrainingSeries
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
  }

  componentDidMount() {
    this.props.fetchTrainingSeries(this.trainingSeriesId);
  }

  handleClick = () => {
    this.props.history.push(`/honoplay/trainingseries/${this.trainingSeriesId}/training`);
  }

  render() {
    const { trainingSeries } = this.state;
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
          <Grid item xs={12} sm={9}></Grid>
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


  return {
    isTrainingSeriesLoading,
    trainingSeries,
    errorTrainingSeries
  };
};

const mapDispatchToProps = {
  fetchTrainingSeries
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(TrainingSeriesUpdate));