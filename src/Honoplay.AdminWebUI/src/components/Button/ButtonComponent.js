import React from 'react';
import { withStyles } from '@material-ui/core/styles'; 
import {Button,MuiThemeProvider} from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';  
import { Style,theme } from './Style';

const ButtonComponent = (props) => {
        const { classes, buttonColor, buttonIcon, buttonName, onClick} = props;
        return (
            <div>
                <MuiThemeProvider theme={theme}>
                    <Button variant="contained"
                            color={buttonColor}
                            className={classes.button}
                            onClick={onClick}>
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
 
export default withStyles(Style)(ButtonComponent);