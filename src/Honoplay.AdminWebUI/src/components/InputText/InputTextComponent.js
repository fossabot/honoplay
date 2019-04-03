import React from 'react';
import {Grid, InputLabel, InputBase} from '@material-ui/core';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import Style from './Style';
  
class InputTextComponent extends React.Component {
  render() {
      const { classes,LabelName,InputId,InputType} = this.props;
  
    return (
      <div className={classes.root}>
        <Grid container spacing={24}>
          {(LabelName ? 
              <Grid item xs={3} className={classes.center}>
                  <InputLabel  className={classes.bootstrapFormLabel}>
                               {LabelName}
                  </InputLabel>                                   
              </Grid> : ""

          )}
          <Grid item xs={9}>
            <InputBase id={InputId}
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