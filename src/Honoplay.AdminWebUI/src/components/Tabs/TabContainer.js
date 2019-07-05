import React from 'react';
import { Typography } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import { Style } from "./Style";


const TabContainer = (props) => {

  const { children, dir, classes } = props;

  return (
    <Typography
      component="div"
      dir={dir}
      className={classes.tabContainer}>
      {children}
    </Typography>
  );
}

export default withStyles(Style)(TabContainer);