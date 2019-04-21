import React from 'react';
import { withStyles } from '@material-ui/core/styles'; 
import {Typography} from '@material-ui/core';
import Style from './Style';

function TypographyComponent(props) {
    const { classes, pageHeader } = props;
    return (
        <Typography className={classes.typography}>
            {pageHeader}
        </Typography>
    );
}
  
export default withStyles(Style)(TypographyComponent);