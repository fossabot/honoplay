import React from 'react';
import Icons from '../Icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { withStyles } from '@material-ui/core/styles';
import {List, ListItem, ListItemText, ListItemIcon, 
        MuiThemeProvider, Typography, Collapse} from '@material-ui/core';
import {NavLink} from 'react-router-dom';
import { Style, theme } from './Style';

class ListItemComponent extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        open: true,
      };
      this.handleClick=this.handleClick.bind(this);
    }
    
    handleClick()
    {
      this.setState(state => ({ open: !state.open }));
    };

    render() {
      const { classes,pageLink,pageIcon,pageName} = this.props;
      return (
        <MuiThemeProvider theme={theme}>
              <ListItem className={classes.listItemLink} 
                        component={NavLink} to={pageLink} 
                        activeClassName={classes.active}
                        button onClick={this.handleClick}>
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

}
export default withStyles(Style)(ListItemComponent);