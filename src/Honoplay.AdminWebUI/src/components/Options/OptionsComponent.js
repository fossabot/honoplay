import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid, Checkbox, MuiThemeProvider, IconButton, TextField } from '@material-ui/core';
import { Style, theme } from './Style';
import Input from '../Input/InputTextComponent';

class OptionsComponent extends React.Component {

    render() {
        const { classes, key } = this.props;
        return (

            <MuiThemeProvider theme={theme} >
                <Grid container key={key} >
                    <Grid item xs={12} sm={1}>
                        <Checkbox 
                            color='secondary'
                        />
                    </Grid>
                    <Grid item xs={12} sm={1}>
                        <Input
                            inputType="text"
                            placeholder={translate('Order')}
                        />
                    </Grid>
                    <Grid item xs={12} sm={5} >
                        <Input
                            inputType="text"
                            placeholder={translate('Answer')}
                        />
                    </Grid>
                </Grid>
            </MuiThemeProvider>
        );
    }
}

export default withStyles(Style)(OptionsComponent);