import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Typography from '../../components/Typography/TypographyComponent';
import Modal from '../../components/Modal/ModalComponent';
import TrainingseriesCreate from './TrainingSeriesCreate';

class TrainingSeries extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      openDialog: false
    }
  }

  handleClickOpenDialog = () => {
    this.setState({ openDialog: true });

  };

  handleCloseDialog = () => {
    this.setState({ openDialog: false });
  };

  render() {
    const { openDialog } = this.state;
    const { classes } = this.props;
    return (
      <div className={classes.root}>
        <Grid container spacing={24}>
          <Grid item xs={12}>
            <Typography
              pageHeader={translate('TrainingSeries')}
            />
          </Grid>
          <Grid item xs={12}>
            <CardButton
              cardName={translate('CreateATrainingSeries')}
              cardDescription={translate('YouCanCreateTrainingSetsAndCollectDifferentTrainingsInOneField')}
              onClick={this.handleClickOpenDialog}
            />
          </Grid>
        </Grid>
        <Modal
          titleName={translate('TrainingSeriesName')}
          open={openDialog}
          handleClose={this.handleCloseDialog}
        > 
          <TrainingseriesCreate />
        </Modal>
      </div>
    );
  }
}

export default withStyles(Style)(TrainingSeries);