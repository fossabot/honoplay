import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Typography from '../../components/Typography/TypographyComponent';
import Modal from '../../components/Modal/ModalComponent';
import Card from '../../components/Card/CardComponents';
import TrainingseriesCreate from './TrainingSeriesCreate';


class TrainingSeries extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      openDialog: false,
      data: [
        { key: 1, label: 'Eğitim Serisi 1', date: '01.02.2019' },
        { key: 2, label: 'Eğitim Serisi 2', date: '03.02.2019' },
        { key: 3, label: 'Eğitim Serisi 3', date: '01.05.2019' },
      ]
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
          <Grid item xs={3}>
            <CardButton
              cardName={translate('CreateATrainingSeries')}
              cardDescription={translate('YouCanCreateTrainingSetsAndCollectDifferentTrainingsInOneField')}
              onClick={this.handleClickOpenDialog}
            />
          </Grid>
          <Grid item xs={12} sm={9}>
            <Card
              data={this.state.data}
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


export default withStyles(Style)(TrainingSeries);