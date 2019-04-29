import React from 'react';
import Icons from '../Icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { withStyles } from '@material-ui/core/styles';
import {ListItem, ListItemText, ListItemIcon, 
        MuiThemeProvider, Typography} from '@material-ui/core';
import {NavLink} from 'react-router-dom';
import { Style, theme } from './Style';

const ListItemComponent = (props) => {

    const { classes,pageLink,pageIcon,pageName} = props;
    return (
      <MuiThemeProvider theme={theme}>
            <ListItem className={classes.listItemLink} 
                      component={NavLink} to={pageLink} 
                      activeClassName={classes.active}>
            <ListItemIcon>
            <FontAwesomeIcon className={classes.fontawesome} 
                             icon={pageIcon}>
            </FontAwesomeIcon>
            </ListItemIcon>
            <ListItemText
                primary= {
                  <Typography className={classes.typography}>
                    {pageName}  
                  </Typography>
                }              
            />                       
            </ListItem>
      </MuiThemeProvider>
    );
}
export default withStyles(Style)(ListItemComponent);