import React from 'react';
import { withStyles } from '@material-ui/core/styles'; 
import {Grid} from '@material-ui/core';
import Style from '../Sorular/Style';
import Tabs from '../../components/Tabs/FullWidthTabs';


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
            <Tabs/>
          </Grid>
        </Grid>
        </div>
        );
    }
}

export default withStyles(Style)(SirketBilgileri);