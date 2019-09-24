import React from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import Grid from '@material-ui/core/Grid';
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import { hidden } from 'ansi-colors';
import { borderLeft } from '@material-ui/system';

const useStyles = makeStyles(theme => ({
  root: {
    flexGrow: 1,
    padding: 8 * 4
  },
  paper: {
    padding: theme.spacing(3),
    textAlign: 'left',
    color: theme.palette.text.secondary,
    '&:hover': {
      boxShadow: '0 .5rem 1rem rgba(0,0,0,.25)'
    },
    '&:hover $actionButtom': {
      visibility: 'visible'
    }
  },
  box: {
    marginBottom: 40,
    height: 65
  },
  div: {
    display: 'flex',
    justifyContent: 'flex-end'
  },
  actionButtom: {
    textTransform: 'uppercase',
    margin: theme.spacing(1),
    width: 152,
    visibility: 'hidden'
  }
}));

export default function CarDeneme() {
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
            //className={classes.grid}
          >
            <Grid item xs={12} sm={3}>
              <Paper className={classes.paper}>
                <div className={classes.box}>
                  <Typography
                    style={{ textTransform: 'uppercase' }}
                    color="primary"
                    gutterBottom
                  >
                    Eğitim Serisi
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    23.09.2019
                  </Typography>
                </div>
                <div className={classes.div}>
                  <Button
                    color="primary"
                    variant="outlined"
                    className={classes.actionButtom}
                  >
                    Düzenle
                  </Button>
                </div>
              </Paper>
            </Grid>

            <Grid item xs={12} sm={3}>
              <Paper className={classes.paper}>
                <div className={classes.box}>
                  <Typography
                    style={{ textTransform: 'uppercase' }}
                    color="primary"
                    gutterBottom
                  >
                    Eğitim Serisi
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    23.09.2019
                  </Typography>
                </div>
                <div className={classes.div}>
                  <Button
                    color="primary"
                    variant="outlined"
                    className={classes.actionButtom}
                  >
                    Düzenle
                  </Button>
                </div>
              </Paper>
            </Grid>
            <Grid item xs={12} sm={3}>
              <Paper className={classes.paper}>
                <div className={classes.box}>
                  <Typography
                    style={{ textTransform: 'uppercase' }}
                    color="primary"
                    gutterBottom
                  >
                    Eğitim Serisi
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    23.09.2019
                  </Typography>
                </div>
                <div className={classes.div}>
                  <Button
                    color="primary"
                    variant="outlined"
                    className={classes.actionButtom}
                  >
                    Düzenle
                  </Button>
                </div>
              </Paper>
            </Grid>
            <Grid item xs={12} sm={3}>
              <Paper className={classes.paper}>
                <div className={classes.box}>
                  <Typography
                    style={{ textTransform: 'uppercase' }}
                    color="primary"
                    gutterBottom
                  >
                    Eğitim Serisi
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    23.09.2019
                  </Typography>
                </div>
                <div className={classes.div}>
                  <Button
                    color="primary"
                    variant="outlined"
                    className={classes.actionButtom}
                  >
                    Düzenle
                  </Button>
                </div>
              </Paper>
            </Grid>
            <Grid item xs={12} sm={3}>
              <Paper className={classes.paper}>
                <div className={classes.box}>
                  <Typography
                    style={{ textTransform: 'uppercase' }}
                    color="primary"
                    gutterBottom
                  >
                    Eğitim Serisi
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    23.09.2019
                  </Typography>
                </div>
                <div className={classes.div}>
                  <Button
                    color="primary"
                    variant="outlined"
                    className={classes.actionButtom}
                  >
                    Düzenle
                  </Button>
                </div>
              </Paper>
            </Grid>
            <Grid item xs={12} sm={3}>
              <Paper className={classes.paper}>
                <div className={classes.box}>
                  <Typography
                    style={{ textTransform: 'uppercase' }}
                    color="primary"
                    gutterBottom
                  >
                    Eğitim Serisi
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    23.09.2019
                  </Typography>
                </div>
                <div className={classes.div}>
                  <Button
                    color="primary"
                    variant="outlined"
                    className={classes.actionButtom}
                  >
                    Düzenle
                  </Button>
                </div>
              </Paper>
            </Grid>
            <Grid item xs={12} sm={3}>
              <Paper className={classes.paper}>
                <div className={classes.box}>
                  <Typography
                    style={{ textTransform: 'uppercase' }}
                    color="primary"
                    gutterBottom
                  >
                    Eğitim Serisi
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    23.09.2019
                  </Typography>
                </div>
                <div className={classes.div}>
                  <Button
                    color="primary"
                    variant="outlined"
                    className={classes.actionButtom}
                  >
                    Düzenle
                  </Button>
                </div>
              </Paper>
            </Grid>
            <Grid item xs={12} sm={3}>
              <Paper className={classes.paper}>
                <div className={classes.box}>
                  <Typography
                    style={{ textTransform: 'uppercase' }}
                    color="primary"
                    gutterBottom
                  >
                    Eğitim Serisi
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    23.09.2019
                  </Typography>
                </div>
                <div className={classes.div}>
                  <Button
                    color="primary"
                    variant="outlined"
                    className={classes.actionButtom}
                  >
                    Düzenle
                  </Button>
                </div>
              </Paper>
            </Grid>
            <Grid item xs={12} sm={3}>
              <Paper className={classes.paper}>
                <div className={classes.box}>
                  <Typography
                    style={{ textTransform: 'uppercase' }}
                    color="primary"
                    gutterBottom
                  >
                    Eğitim Serisi
                  </Typography>
                  <Typography variant="body2" gutterBottom>
                    23.09.2019
                  </Typography>
                </div>
                <div className={classes.div}>
                  <Button
                    color="primary"
                    variant="outlined"
                    className={classes.actionButtom}
                  >
                    Düzenle
                  </Button>
                </div>
              </Paper>
            </Grid>
          </Grid>
        </Grid>
      </div>
    </React.Fragment>
  );
}
