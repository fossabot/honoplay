import React from 'react';
import moment from 'moment';
import { withStyles } from '@material-ui/core/styles';
import { Card, CardContent, Typography, Grid, IconButton, MuiThemeProvider } from '@material-ui/core';
import Button from '../Button/ButtonComponent';
import { Style, theme } from './Style';
import DeleteOutlinedIcon from '@material-ui/icons/DeleteOutlined';


class CardComponent extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { classes, data } = this.props;
        return (
            <MuiThemeProvider theme={theme}>
                <Grid container spacing={24}>
                    <Grid item sm={12}>
                        <div className={classes.cardRoot}>
                            {data.map((data, i) => {
                                const dateToFormat = data.createdAt;
                                return (
                                    <Grid item xs={6} sm={3} key={data.id}>
                                        <Card className={classes.card} key={i}>
                                            <div className={classes.cardIcon}>
                                                <IconButton
                                                    key={data.id}>
                                                    <DeleteOutlinedIcon />
                                                </IconButton>
                                            </div>
                                            <CardContent>
                                                <Typography
                                                    variant="h6"
                                                    className={classes.cardLabel}>
                                                    {data.name}
                                                </Typography>
                                                <Typography
                                                    variant="h6"
                                                    className={classes.cardDate}>
                                                    {moment(dateToFormat).format("DD/MM/YYYY")}

                                                </Typography>
                                                <div className={classes.center}>
                                                    <Button
                                                        buttonColor="secondary"
                                                        buttonName="Düzenle"
                                                    />
                                                </div>
                                            </CardContent>
                                        </Card>
                                    </Grid>
                                );
                            })}
                        </div>
                    </Grid>
                </Grid>
            </MuiThemeProvider>
        );
    }
}

export default withStyles(Style)(CardComponent);