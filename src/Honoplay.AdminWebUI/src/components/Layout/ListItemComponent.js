import React from 'react';
import Icons from '../Icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import {ListItem, ListItemText, ListItemIcon, Typography, Divider} from '@material-ui/core';
import {NavLink} from 'react-router-dom';
import Style from './Style';

class ListItemComponent extends React.Component {
render() {
    const { classes,pageLink,pageIcon,pageName} = this.props;

    return (
        <div>
            <ListItem className={classes.ListItemLink} 
                      component={NavLink} to={pageLink} 
                      activeClassName={classes.active}>
            <ListItemIcon>
            <FontAwesomeIcon className={classes.fontawesome} 
                             icon={pageIcon}>
            </FontAwesomeIcon>
            </ListItemIcon>
            <ListItemText
                primary={<Typography className={classes.Typography}> 
                {pageName} </Typography>}
            />                       
            </ListItem>
            <Divider className={classes.Divider}/>
        </div>
    );
  }
}
ListItemComponent.propTypes = {
  classes: PropTypes.object.isRequired,
  theme: PropTypes.object.isRequired,
};

export default withStyles(Style, { withTheme: true })(ListItemComponent);