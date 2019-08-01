import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid } from '@material-ui/core';
import Style from '../Style';
import CardButton from '../../components/Card/CardButton';
import Typography from '../../components/Typography/TypographyComponent';

class TrainingSeries extends React.Component {

  constructor(props) {
    super(props);
  }

  handleClick = () => {
    this.props.history.push("/home/trainingseries/trainingseriescreate");
  }

  render() {
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
              onClick= {this.handleClick}
            />
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default withStyles(Style)(TrainingSeries);