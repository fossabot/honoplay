import React from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import {
    Paper,
    Typography,
    Grid,
    CardActions,
    Button,
    IconButton,
    TablePagination,
    Divider
} from '@material-ui/core';
import { deepPurple, deepOrange } from '@material-ui/core/colors';
import { translate } from '@omegabigdata/terasu-api-proxy';
import moment from 'moment';
import { makeStyles } from '@material-ui/core/styles';
import Edit from '@material-ui/icons/Edit';
const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1
    },
    paper: {
        textAlign: 'left',
        width: '220px',
        height: '100px',
        color: theme.palette.text.secondary,
    },
    box: {
        postion: "absolute",
        left: "50px",
        top: "50px",
    },
    actionButtom: {
        textTransform: 'uppercase',
        margin: theme.spacing(1),
        width: 152,
    },

    pHover: {
        "width": "200px",
        "height": "90px",
        "left": "0",
        "padding": 8,
        overflow:"hidden",
        "transitionProperty": "transform, height, width, left,padding",
        "transitionDuration": "0.15s,  0.15s,  0.15s, 0.15s, 0.15s",
        "transitionTimingFunction": "cubic-bezier(.26,.58,0,.9)",
        "zIndex": "0",
        "position": "absolute",
        '&:hover': { "height": "130px", "padding-left": 16, "padding-top": 8, "padding-right": 16, "padding-bottom": 8, "width": "216px", "left": "-8px", "zIndex": "1" },
    }
}));

export default function CarDeneme(props) {
    const { data } = props;
    const classes = useStyles();

    return (
        <React.Fragment>
            <CssBaseline />
            <div className={classes.root}>
                <div style={{ "display": "flex", "flexWrap": "wrap" }}>
                  
                        {data &&
                            data.map((data, index) => {
                                const dateToFormat = data.createdAt;
                                return (
                                    <div style={{ "display": "flex", "flexWrap": "wrap" }}>
                                        <div className={classes.paper}>
                                            <div style={{ position: "relative" }}>
                                                <Paper className={classes.pHover}>
                                                    <div style={{
                                                        position: "relative",
                                                        }}>
                                                        <div className={classes.box}>
                                                            <Typography
                                                                style={{ left: 0, textTransform: 'uppercase' }}
                                                                color="primary"
                                                                gutterBottom
                                                            >
                                                                {data.name}
                                                            </Typography>
                                                            <Typography
                                                                variant="body2"
                                                                gutterBottom
                                                                className={classes.date}
                                                            >
                                                                {moment(dateToFormat).format('DD/MM/YYYY')}
                                                            </Typography>
                                                        </div>
                                                        <Divider />
                                                        <div style={{ position: "absolute", right:0, top:0 }}>
                                                            <IconButton>
                                                                <Edit />
                                                            </IconButton>
                                                        </div>
                                                    </div>
                                                  
                                                </Paper>
                                            </div>
                                        </div>
                                    </div>
                                );
                            })}
                </div>
            </div>
        </React.Fragment>
    );
}
