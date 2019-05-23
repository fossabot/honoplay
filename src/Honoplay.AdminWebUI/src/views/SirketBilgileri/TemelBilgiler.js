import React from 'react';
import terasuProxy from '@omegabigdata/terasu-api-proxy';
import { tenantName, tenantLogo } from '../../helpers/TerasuKey';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import FileInput from '../../components/Input/FileInputComponent';

class TemelBilgiler extends React.Component {

constructor(props) {
  super(props);
}

render() {
  const { classes } = this.props;
  
    return (
        <div className={classes.root}>
        <Grid container spacing={40}>
          <Grid item xs={12} sm={12}/>
          <Grid item xs={12} sm={12}>
            <Input 
              labelName={terasuProxy.translate(tenantName)}              
              inputType="text"
            />
            <FileInput 
              labelName={terasuProxy.translate(tenantLogo)}
            />
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(TemelBilgiler);