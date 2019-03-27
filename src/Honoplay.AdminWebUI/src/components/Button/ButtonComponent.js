import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import {Button,MuiThemeProvider,createMuiTheme,IconButton } from '@material-ui/core';
import {green, red, blue} from '@material-ui/core/colors';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';  
import Style from './Style';

const theme = createMuiTheme({
    palette: {
      primary: green,
      secondary: red,
      accent: blue
    },
});


class ButtonComponent extends React.Component {
    render() {
        const { classes,ButtonColor,ButtonIcon,ButtonName} = this.props;
 
        return (
            <div>
                <MuiThemeProvider theme={theme}>
                    <Button variant="contained"
                            color={ButtonColor}
                            className={classes.button}>
                            {(ButtonIcon ? 
                                <div className={classes.div}>
                                    <FontAwesomeIcon icon={ButtonIcon}/>
                                </div>
                             : "" )}
                            {ButtonName} 
                    </Button>
                </MuiThemeProvider>
            </div>   
        );
    }
}  
ButtonComponent.propTypes = {
    classes: PropTypes.object.isRequired,
};
 
export default withStyles(Style, { withTheme: true })(ButtonComponent);