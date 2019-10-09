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
import Modal from '../../../components/Modal/ModalComponent';
import TrainingseriesCreate from './TrainingSeriesCreate';
import Card from '../../../components/Card/Card';
import Button from '../../../components/Button/ButtonComponent';
import SearchIcon from '@material-ui/icons/Search';
import BreadCrumbs from '../../../components/BreadCrumbs/BreadCrumbs';

import { connect } from 'react-redux';
import { fetchTrainingSeriesList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/TrainingSeries';

class TrainingSeries extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      search: [],
      openDialog: false,
      deneme: [],
      trainingSerieses: [],
      trainingSeriesesError: false,
      checked: false
    };
  }

  componentDidUpdate(prevProps) {
    const {
      isTrainingSeriesListLoading,
      TrainingSeriesList,
      errorTrainingSeriesList
    } = this.props;

    if (!prevProps.errorTrainingSeriesList && errorTrainingSeriesList) {
      this.setState({
        trainingSeriesesError: true
      });
    }
    if (
      prevProps.isTrainingSeriesListLoading &&
      !isTrainingSeriesListLoading &&
      TrainingSeriesList
    ) {
      this.setState({
        trainingSerieses: TrainingSeriesList.items,
        openDialog: false
      });
    }
  }
  componentDidMount() {
    const { fetchTrainingSeriesList } = this.props;
    fetchTrainingSeriesList(0, 50);
  }

  handleClickOpenDialog = () => {
    this.setState({ openDialog: true });
  };

  handleCloseDialog = () => {
    this.setState({ openDialog: false });
  };

  onSearchInputChange = event => {
    let searched = [];
    this.state.trainingSerieses.map((trainingSeries, index) => {
      if (trainingSeries.name.includes(event.target.value)) {
        searched = searched.concat(trainingSeries);
      }
    });
    this.setState({ search: searched });
  };

  render() {
    const { openDialog, trainingSerieses, search } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={9}>
            <BreadCrumbs />
          </Grid>
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
              onClick={this.handleClickOpenDialog}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Divider />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Card
              add
              data={search.length === 0 ? trainingSerieses : search}
              id={id => {
                if (id) {
                  localStorage.setItem('trainingSeriesId', id);
                }
              }}
            />
          </Grid>
        </Grid>
        <Modal
          titleName={translate('CreateATrainingSeries')}
          open={openDialog}
          handleClose={this.handleCloseDialog}
        >
          <TrainingseriesCreate />
        </Modal>
      </div>
    );
  }
}

const mapStateToProps = state => {
  const {
    isTrainingSeriesListLoading,
    TrainingSeriesList,
    errorTrainingSeriesList
  } = state.trainingSeriesList;

  return {
    isTrainingSeriesListLoading,
    TrainingSeriesList,
    errorTrainingSeriesList
  };
};

const mapDispatchToProps = {
  fetchTrainingSeriesList
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(TrainingSeries));
