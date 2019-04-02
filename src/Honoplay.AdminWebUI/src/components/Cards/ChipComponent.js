import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import {Paper, Chip, Typography, Grid} from '@material-ui/core';
import Style from './ChipStyle';


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
    const { classes, CardName } = this.props;

    return (
      <Grid container spacing={24}>
        <Grid item xs={12}>
          <Paper className={classes.root}>
            <Grid item xs={12}>
              <Typography variant="h5" className={classes.typography} >
              {CardName}
              </Typography>
            </Grid>
             {this.state.chipData.map(data => {
             return (
              <Chip
                key={data.key}
                label={data.label}
                onDelete={this.handleDelete.bind(this,data)}
                className={classes.chip}
              />
            );
            })}
          </Paper>
        </Grid>
      </Grid>

    );
  }
}

ChipComponent.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(Style)(ChipComponent);
