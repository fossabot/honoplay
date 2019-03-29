import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import {Grid, InputLabel, NativeSelect, FormControl} from '@material-ui/core';
import Style from './Style';
import BootstrapInput from './DropDownInputStyle';

class DropDownInputComponent extends React.Component {

  constructor(props) {
        super(props);
        this.state={Data: props.data};
       
  }

  render() {
    const { classes, LabelName } = this.props;

    return (
        <div className={classes.root}>
        <Grid container spacing={24}>
          <Grid item xs={3} className={classes.center}>
            <InputLabel  htmlFor="bootstrap-input" 
                         className={classes.bootstrapFormLabel}>
                         {LabelName}
            </InputLabel>                                   
          </Grid>
          <Grid item xs={9}>
         
            <NativeSelect
                style={{width: '90%'}}
                value={this.state.age}
                onChange={this.handleChange}
                input={<BootstrapInput fullWidth/>}
            >
                <option>Seçiniz</option>
                {this.state.Data.map((data,i) =>
                    
                    <option value={data.key} key={i}>{data.label}</option>
                )}                   
            </NativeSelect>              
          </Grid>
        </Grid>
      </div>
    );
  }
}

DropDownInputComponent.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(Style)(DropDownInputComponent);
