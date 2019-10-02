import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid, Divider } from '@material-ui/core';
import Style from '../Style';
import Header from '../../components/Typography/TypographyComponent';
import Modal from '../../components/Modal/ModalComponent';
import TrainingseriesCreate from './TrainingSeriesCreate';
import CardDeneme from '../../components/Card/Card';
import Button from '../../components/Button/ButtonComponent';

import { connect } from 'react-redux';
import { fetchTrainingSeriesList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/TrainingSeries';

class TrainingSeries extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      openDialog: false,
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

  render() {
    const { openDialog, trainingSerieses } = this.state;
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={11}>
            <Header pageHeader={translate('TrainingSeries')} />
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
          <Grid item xs={12} sm={12}>
            <CardDeneme
              elevation={1}
              data={trainingSerieses}
              url="trainingseriesdetail"
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
