import React from 'react';
import { withStyles } from '@material-ui/core/styles'; 
import {Button,MuiThemeProvider} from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';  
import theme from '../../TypographyTheme';
import Style from './Style';

class ButtonComponent extends React.Component {
    render() {
        const { classes, buttonColor, buttonIcon, buttonName} = this.props;
 
        return (
            <div>
                <MuiThemeProvider theme={theme}>
                    <Button variant="contained"
                            color={buttonColor}
                            className={classes.button}>
                            {(buttonIcon &&
                                <div className={classes.div}>
                                    <FontAwesomeIcon icon={buttonIcon}/>
                                </div>
                            )}
                            {buttonName} 
                    </Button>
                </MuiThemeProvider>
            </div>   
        );
    }
}  
 
export default withStyles(Style, { withTheme: true })(ButtonComponent);