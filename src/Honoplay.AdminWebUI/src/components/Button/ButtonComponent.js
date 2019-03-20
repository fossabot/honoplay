import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import {Button,MuiThemeProvider,createMuiTheme } from '@material-ui/core';
import green from '@material-ui/core/colors/green';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';  

const styles = theme => ({
    button: {
        margin: theme.spacing.unit,
        color: 'white'
    },
});

const theme = createMuiTheme({
    palette: {
      primary: green,
    },
});

class ButtonComponent extends React.Component {
    render() {
        const { classes,ButtonIcon,ButtonName} = this.props;
 
        return (
            <div>
                <MuiThemeProvider theme={theme}>
                    <Button variant="contained" 
                            color="primary"
                            className={classes.button}>
                    <FontAwesomeIcon icon={ButtonIcon}/>
                        &nbsp; {ButtonName} &nbsp;
                    </Button>
                </MuiThemeProvider>
            </div>   
        );
    }
}  
ButtonComponent.propTypes = {
    classes: PropTypes.object.isRequired,
};
 
export default withStyles(styles, { withTheme: true })(ButtonComponent);