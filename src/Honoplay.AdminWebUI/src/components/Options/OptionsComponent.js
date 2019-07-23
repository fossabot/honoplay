import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Grid, Typography, Checkbox, MuiThemeProvider } from '@material-ui/core';
import {Style,theme} from './Style';
import Input from '../Input/InputTextComponent';

class OptionsComponent extends React.Component {

    constructor(props) {
        super(props);

    }

    render() {
        const { classes, data } = this.props;

        return (
            <MuiThemeProvider theme={theme} >
                <div className={classes.root}>
                    <Grid item xs={12} sm={12}>
                        <Typography>
                            Doğru Cevap ?
                        </Typography>
                    </Grid>
                    {data.map((data, id) => (
                        <Grid container direction="row" spacing={24}>
                            <Grid item xs={12} sm={12} />
                            <Grid item xs={12} sm={1}>
                                <Checkbox
                                    color='secondary'
                                />
                            </Grid>
                            <Grid item xs={12} sm={1} >
                                <Input
                                    disabled
                                    key={id}
                                    inputType="text"
                                    value={data.order}
                                />
                            </Grid>
                            <Grid item xs={12} sm={10} >
                                <Input
                                    disabled
                                    key={id}
                                    inputType="text"
                                    value={data.answer}
                                />
                            </Grid>
                        </Grid>
                    ))}
                </div>
            </MuiThemeProvider>
        );
    }
}

export default withStyles(Style)(OptionsComponent);