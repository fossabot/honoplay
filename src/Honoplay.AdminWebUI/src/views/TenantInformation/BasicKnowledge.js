import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid} from '@material-ui/core';
import Style from '../Style';
import Input from '../../components/Input/InputTextComponent';
import FileInput from '../../components/Input/FileInputComponent';

class BasicKnowledge extends React.Component {

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
              labelName={translate('TenantName')}              
              inputType="text"
            />
            <FileInput 
              labelName={translate('TenantLogo')}
            />
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(BasicKnowledge);