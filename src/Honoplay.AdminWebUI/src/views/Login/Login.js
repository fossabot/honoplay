import React from 'react';
import PropTypes from 'prop-types';
import CssBaseline from '@material-ui/core/CssBaseline';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import {Typography, Paper, InputLabel, Input, Checkbox, 
        FormControlLabel, FormControl, Button, Avatar, MuiThemeProvider, createMuiTheme, TextField} from '@material-ui/core';
import {red} from '@material-ui/core/colors';
import withStyles from '@material-ui/core/styles/withStyles';
import Style from './Style';

const theme = createMuiTheme({
  palette: {
    primary: red,
  },
  typography: { useNextVariants: true },
});

function Login(props) {
  const { classes } = props;
  return (
    <main className={classes.main}>
      <CssBaseline />
      <Paper className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Login
        </Typography>
        <form className={classes.form}>
          <FormControl margin="normal"  fullWidth>
            
            <MuiThemeProvider theme={theme}>
              <TextField
                label="Email Adres"
                id="email" 
                name="email" 
                autoComplete="email" 
                autoFocus
              />
            </MuiThemeProvider> 
          </FormControl>
          <FormControl margin="normal"  fullWidth>
            
            <MuiThemeProvider theme={theme}>
              <TextField
                className={classes.margin}
                label="Parola"
                name="password" 
                type="password" 
                id="password" 
                autoComplete="current-password"
              />
            </MuiThemeProvider> 
          </FormControl>
          <FormControlLabel
            control={<Checkbox value="remember" color="primary" />}
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
  );
}

Login.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(Style)(Login);