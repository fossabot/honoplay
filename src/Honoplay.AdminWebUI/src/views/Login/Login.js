import React from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import {Paper, Checkbox, 
        FormControlLabel, FormControl, Button, 
        Avatar, MuiThemeProvider,TextField, 
        Typography} from '@material-ui/core';
import withStyles from '@material-ui/core/styles/withStyles';
import { Style, theme} from './Style';

function Login(props) {
  const { classes } = props;
  return (
    <MuiThemeProvider theme={theme}>
      <main className={classes.main}>
        <CssBaseline />
        <Paper className={classes.paper}>
          <Avatar className={classes.avatar}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography variant="h5" className={classes.typography}>
            Login
          </Typography>
          <form className={classes.form}>
            <FormControl margin="normal"  fullWidth>
                <TextField
                  label="Email Adres"
                  id="email" 
                  name="email" 
                  autoComplete="email" 
                  autoFocus
                />
            </FormControl>
            <FormControl margin="normal"  fullWidth>
                <TextField
                  className={classes.margin}
                  label="Parola"
                  name="password" 
                  type="text" 
                  id="password" 
                  autoComplete="current-password"
                />
            </FormControl>
            <FormControlLabel
              control={<Checkbox value="remember" 
                                 color="primary"/>}
              label="Beni Hatırla"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="secondary"
              className={classes.submit}
            >
              Login
            </Button>

          </form>
        </Paper>
      </main>
    </MuiThemeProvider>
  );
}
export default withStyles(Style)(Login);