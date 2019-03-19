import React from 'react';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faQuestionCircle, faListOl, faGraduationCap, 
         faUsers, faCog, faChartPie } from '@fortawesome/free-solid-svg-icons';
library.add(faQuestionCircle, faListOl, faGraduationCap, faUsers, faCog, faChartPie );
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import {ListItem, ListItemText,Typography, Divider} from '@material-ui/core';
import {NavLink} from 'react-router-dom';
import Style from './Style';

function ListItemLink(props) {
  return <ListItem button component="a" {...props} />;
}

class MenuItem extends React.Component {
render() {
    const { classes,pageLink,pageIcon,pageName} = this.props;

    return (
        <div>
            <ListItemLink className={classes.ListItemLink} component={NavLink} to={pageLink}>
            <FontAwesomeIcon className={classes.fontawesome} icon={pageIcon} />
            <ListItemText
                primary={<Typography className={classes.Typography}> {pageName} </Typography>}
            />             
            </ListItemLink>
            <Divider className={classes.Divider}/>
        </div>
    );
  }
}
MenuItem.propTypes = {
  classes: PropTypes.object.isRequired,
  theme: PropTypes.object.isRequired,
};

export default withStyles(Style, { withTheme: true })(MenuItem);