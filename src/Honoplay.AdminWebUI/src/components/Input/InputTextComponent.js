import React from 'react';
import {Grid, InputLabel, InputBase} from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles'; 
import {Style} from './Style';
  
const InputTextComponent = (props) => {

    const { classes,labelName,inputType} = props;
    return (
      <div className={classes.inputRoot}>
        <Grid container spacing={24}>
          {(labelName &&
              <Grid item xs={12} sm={3}
                    className={classes.labelCenter}>
                  <InputLabel  className={classes.bootstrapFormLabel}>
                               {labelName}
                  </InputLabel>                                   
              </Grid> 
          )}
          <Grid item xs={12} sm={9}>
            <InputBase 
              type={inputType}
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

export default withStyles(Style)(InputTextComponent);