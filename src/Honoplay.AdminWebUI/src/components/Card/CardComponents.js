import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import { Card, CardContent, Typography, Grid, IconButton, MuiThemeProvider } from '@material-ui/core';
import Button from '../Button/ButtonComponent';
import {Style, theme} from './Style';
import DeleteOutlinedIcon from '@material-ui/icons/DeleteOutlined';


class CardComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = { chipData: props.data };
        this.handleDelete = this.handleDelete.bind(this);
    }

    handleDelete(data) {
        this.setState(state => {
            const chipData = [...state.chipData];
            const chipToDelete = chipData.indexOf(data);
            chipData.splice(chipToDelete, 1);
            return { chipData };
        });

    };

    render() {
        const { classes } = this.props;
        return (
            <MuiThemeProvider theme={theme}>
                <Grid container spacing={24}>
                    <Grid item sm={12}>
                        <div className={classes.cardRoot}>
                            {this.state.chipData.map((data, i) => {
                                return (
                                    <Grid item xs={6} sm={3}>
                                        <Card className={classes.card} key={i}>
                                            <div className={classes.cardIcon}>
                                                <IconButton
                                                    key={data.key}
                                                    onClick={this.handleDelete.bind(this, data)}>
                                                    <DeleteOutlinedIcon />
                                                </IconButton>

                                            </div>
                                            <CardContent>
                                                <Typography variant="h6" className={classes.cardLabel}>
                                                    {data.label}
                                                </Typography>
                                                <Typography variant="h6" className={classes.cardDate}>
                                                    {data.date}
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

CardComponent.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(Style)(CardComponent);