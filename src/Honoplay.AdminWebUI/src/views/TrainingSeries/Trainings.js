import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import {
  Grid,
  Divider,
  TextField,
  InputAdornment,
  IconButton
} from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Card from '../../components/Card/CardComponents';
import Typography from '../../components/Typography/TypographyComponent';
import BreadCrumbs from '../../components/BreadCrumbs/BreadCrumbs';
import Button from '../../components/Button/ButtonComponent';
import SearchIcon from '@material-ui/icons/Search';

import { connect } from 'react-redux';
import { fetchTrainingSeries } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/TrainingSeries';
import { fetchTrainingListByTrainingSeriesId } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training';

class Trainings extends React.Component {
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
    };
  }

  trainingSeriesId = localStorage.getItem('trainingSeriesId');

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
      });
    }
    if (
      prevProps.isTrainingSeriesLoading &&
      !isTrainingSeriesLoading &&
      trainingSeries
    ) {
      if (!errorTrainingSeries) {
        this.setState({
          trainingSeries: trainingSeries.items[0]
        });
      }
    }
    if (
      !prevProps.errorTrainingListByTrainingSeriesId &&
      errorTrainingListByTrainingSeriesId
    ) {
      this.setState({
        trainingListError: true
      });
    }
    if (
      prevProps.isTrainingListByTrainingSeriesIdLoading &&
      !isTrainingListByTrainingSeriesIdLoading &&
      TrainingListByTrainingSeriesId
    ) {
      this.setState({
        trainingList: TrainingListByTrainingSeriesId.items
      });
    }
  }

  componentDidMount() {
    this.props.fetchTrainingSeries(this.trainingSeriesId);
    this.props.fetchTrainingListByTrainingSeriesId(this.trainingSeriesId);
  }

  handleClick = () => {
    this.props.history.push(`/trainingseriesdetail/training`);
  };

  render() {
    const { classes, match } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={9}>
            <BreadCrumbs />
          </Grid>
          <Grid item xs={12} sm={2}>
            <TextField
              placeholder={translate('Search')}
              margin="none"
              onChange={this.onSearchInputChange}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="start">
                    <IconButton>
                      <SearchIcon />
                    </IconButton>
                  </InputAdornment>
                )
              }}
            />
          </Grid>
          <Grid item xs={12} sm={1}>
            <Button
              buttonColor="primary"
              buttonName={translate('Add')}
              onClick={this.handleClickOpenDialog}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={9}>
            <Card
              data={trainingList}
              url="trainingseriesupdate"
              id={id => {
                if (id) {
                  localStorage.setItem('trainingId', id);
                }
              }}
            ></Card>
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
)(withStyles(Style)(Trainings));
