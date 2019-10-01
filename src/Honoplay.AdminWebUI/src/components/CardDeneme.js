import React from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import {
  Paper,
  Typography,
  Grid,
  CardActions,
  Button,
  IconButton,
  TablePagination
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
    padding: theme.spacing(3),
    textAlign: 'left',
    color: theme.palette.text.secondary,
    '&:hover': {
      boxShadow: '0 .5rem 1rem rgba(0,0,0,.25)'
      //position: 'absolute'
    },
    '&:hover $div': {
      visibility: 'visible'
      //display: 'flex'
    },
    '&:hover $date': {
      color: deepPurple[700]
    }
  },
  box: {
    marginBottom: 40,
    height: 10
  },
  div: {
    display: 'flex',
    justifyContent: 'flex-end',
    visibility: 'hidden'
  },
  actionButtom: {
    textTransform: 'uppercase',
    margin: theme.spacing(1),
    width: 152
  }
}));

export default function CarDeneme(props) {
  const { data } = props;
  const classes = useStyles();

  return (
    <React.Fragment>
      <CssBaseline />
      <div className={classes.root}>
        <Grid container justify="flex-start">
          <Grid
            spacing={4}
            alignItems="flex-start"
            justify="flex-start"
            container
          >
            {data &&
              data.map((data, index) => {
                const dateToFormat = data.createdAt;
                return (
                  <Grid item xs={12} sm={3} key={index}>
                    <Paper className={classes.paper}>
                      <div className={classes.box}>
                        <Typography
                          style={{ textTransform: 'uppercase' }}
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
                      <CardActions disableSpacing className={classes.div}>
                        <IconButton>
                          <Edit />
                        </IconButton>
                      </CardActions>
                    </Paper>
                  </Grid>
                );
              })}
          </Grid>
        </Grid>
      </div>
    </React.Fragment>
  );
}
