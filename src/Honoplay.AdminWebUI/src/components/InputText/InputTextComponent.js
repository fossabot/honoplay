import React from 'react';
import {Grid, InputLabel, InputBase} from '@material-ui/core';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import Style from './Style';

  
class InputTextComponent extends React.Component {
  render() {
      const { classes,InputName,InputType,PlaceHolderName} = this.props;
  
    return (
      <div className={classes.root}>
        <Grid container spacing={24}>
          <Grid item sm={3} className={classes.center}>
            <InputLabel  htmlFor="bootstrap-input" 
                         className={classes.bootstrapFormLabel}>
                         {InputName}
            </InputLabel>                                   
          </Grid>
          <Grid item sm={9}>
            <InputBase id="bootstrap-input"
                       defaultValue={PlaceHolderName}
                       type={InputType}
                       fullWidth
                       classes={{
                         root: classes.bootstrapRoot,
                         input: classes.bootstrapInput,
                       }}
              />          
          </Grid>
        </Grid>
      </div>
    );
  }
}

InputTextComponent.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(Style)(InputTextComponent);