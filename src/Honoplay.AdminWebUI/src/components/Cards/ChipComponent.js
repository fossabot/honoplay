import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import {Paper, Chip, Grid, Typography} from '@material-ui/core';
import Style from './Style';


class ChipComponent extends React.Component {
  
  constructor(props) {
    super(props);
    this.state={chipData: props.data};
  }

  handleDelete  (data) {
    this.setState(state => {
      const chipData = [...state.chipData];
      const chipToDelete = chipData.indexOf(data);
      chipData.splice(chipToDelete, 1);
      return { chipData };
    });
  };

  render() {
    const { classes, cardName } = this.props;

    return (
      <Grid container spacing={24}>
        <Grid item xs={12}>
          <Paper className={classes.chipRoot}>
            <Grid item xs={12}>
              <Typography className={classes.chipTypography}>
                {cardName}
              </Typography>
            </Grid>
            {this.state.chipData.map(data => {
             return (
              <Chip
                key={data.key}
                label={data.label}
                onDelete={this.handleDelete.bind(this,data)}
                className={classes.chip}
                variant="outlined"
              />
            );})}
          </Paper>
        </Grid>
      </Grid>
    );
  }
}

export default withStyles(Style)(ChipComponent);
