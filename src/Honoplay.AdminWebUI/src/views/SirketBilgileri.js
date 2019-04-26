import React from 'react';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid} from '@material-ui/core';
import Style from './Sorular/Style';
import Typography from '../components/Typography/TypographyComponent';
import Tabs from '../components/Tabs/Tabs';

class SirketBilgileri extends React.Component {

constructor(props) {
  super(props);
}

render() {
  const { classes } = this.props;
  
    return (
        <div className={classes.root}>
        <Grid container spacing={24}>
          <Grid item xs={12} sm={12}>
            <Typography pageHeader='Şirket Bilgileri'/>
          </Grid>
          <Grid item xs={12} sm={12}>
            <Tabs/>
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(SirketBilgileri);