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
import Style from '../../Style';
import Card from '../../../components/Card/Card';
import Button from '../../../components/Button/ButtonComponent';
import SearchIcon from '@material-ui/icons/Search';

import { connect } from 'react-redux';
import { fetchTrainingSeries } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/TrainingSeries';
import { fetchTrainingListByTrainingSeriesId } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/Training';

class Trainings extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      search: [],
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

  onSearchInputChange = event => {
    let searched = [];
    this.state.trainingList.map((training, index) => {
      if (training.name.includes(event.target.value)) {
        searched = searched.concat(training);
      }
    });
    this.setState({ search: searched });
  };

  render() {
    const { trainingList, search } = this.state;
    const { classes, onClick } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={9} />
          <Grid item xs={8} sm={2}>
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
          <Grid item xs={4} sm={1}>
            <Button
              buttonColor="primary"
              buttonName={translate('Add')}
              onClick={onClick}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Card
              data={search.length === 0 ? trainingList : search}
              url={`${this.trainingSeriesId}/training`}
              id={id => {
                if (id) {
                  localStorage.setItem('trainingId', id);
                }
              }}
              name={name => {
                if (name) {
                  localStorage.setItem('trainingName', name);
                }
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
)(withStyles(Style)(Trainings));
