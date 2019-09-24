import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Chip, Grid } from '@material-ui/core';
import Style from './Style';

class ChipComponent extends React.Component {
  render() {
    const { classes, data } = this.props;

    return (
      <Grid container spacing={10}>
        <Grid item xs={12} sm={12}>
          {data.map(data => {
            return (
              <Chip
                variant="outlined"
                key={data.id}
                label={data.name}
                className={classes.chip}
              />
            );
          })}
        </Grid>
      </Grid>
    );
  }
}

export default withStyles(Style)(ChipComponent);
