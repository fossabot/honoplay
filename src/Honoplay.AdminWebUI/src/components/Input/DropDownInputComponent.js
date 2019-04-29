import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import {Grid, InputLabel, NativeSelect} from '@material-ui/core';
import {Style, BootstrapInput} from './Style';

class DropDownInputComponent extends React.Component {
  constructor(props) {
        super(props);
        this.state={data: props.data};       
  }
  render() {

    const { classes, labelName } = this.props;
    return (
        <div className={classes.inputRoot}>
        <Grid container spacing={24}>
          <Grid item xs={12} sm={3}
                className={classes.labelCenter}>
            <InputLabel  htmlFor="bootstrap-input" 
                         className={classes.bootstrapFormLabel}>
                         {labelName}
            </InputLabel>                                   
          </Grid>
          <Grid item xs={12} sm={9}>
            <NativeSelect
                className={classes.nativeWidth}
                input={<BootstrapInput fullWidth/>}
            >
                <option>Seçiniz</option>
                {this.state.data.map(n => 
                    <option value={n.key} key={n.key}>
                      {n.label}
                    </option>
                )}                   
            </NativeSelect>              
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default withStyles(Style)(DropDownInputComponent);
