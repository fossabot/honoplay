import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid } from '@material-ui/core';
import { Link } from "react-router-dom";
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Typography from '../../components/Typography/TypographyComponent';
import Modal from '../../components/Modal/ModalComponent';
import Card from '../../components/Card/CardComponents';
import TrainingseriesCreate from './TrainingSeriesCreate';
import TrainingSeriesUpdate from './TrainingSeriesUpdate';
import { connect } from "react-redux";
import { fetchTrainingSeriesList } from '@omegabigdata/honoplay-redux-helper/dist/Src/actions/TrainingSeries';

class TrainingSeries extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      openDialog: false,
      trainingSerieses: [],
      trainingSeriesesError: false,
    }
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
      })
    }
    if (prevProps.isTrainingSeriesListLoading && !isTrainingSeriesListLoading && TrainingSeriesList) {
      this.setState({
        trainingSerieses: TrainingSeriesList.items
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
        <Grid container spacing={24}>
          <Grid item xs={12} sm={12}>
            <Typography
              pageHeader={translate('TrainingSeries')}
            />
          </Grid>
          <Grid item xs={12} sm={3}>
            <CardButton
              cardName={translate('CreateATrainingSeries')}
              cardDescription={translate('YouCanCreateTrainingSetsAndCollectDifferentTrainingsInOneField')}
              onClick={this.handleClickOpenDialog}
            />
          </Grid>
          <Grid item xs={12} sm={9}>
            <Card
              data={trainingSerieses}
              url="trainingseries"
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