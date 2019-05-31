import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import CssBaseline from '@material-ui/core/CssBaseline';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import {Paper, Checkbox, 
        FormControlLabel, FormControl, Button, 
        Avatar, MuiThemeProvider,TextField, 
        Typography} from '@material-ui/core';
import withStyles from '@material-ui/core/styles/withStyles';
import { Style, theme} from './Style';

class Login extends React.Component {
  render() {
    const { classes } = this.props;
    return (
      <MuiThemeProvider theme={theme}>
        <main className={classes.main}>
          <CssBaseline />
          <Paper className={classes.paper}>
            <Avatar className={classes.avatar}>
              <LockOutlinedIcon />
            </Avatar>
            <Typography variant="h5" className={classes.typography}>
             {translate('Login')}
            </Typography>
            <form className={classes.form}>
              <FormControl margin="normal"  fullWidth>
                  <TextField
                    label={translate('EmailAddress')}
                    id="email" 
                    name="email" 
                    autoComplete="email" 
                    autoFocus
                  />
              </FormControl>
              <FormControl margin="normal"  fullWidth>
                  <TextField
                    className={classes.margin}
                    label={translate('Password')}
                    name="password" 
                    type="text" 
                    id="password" 
                    autoComplete="current-password"
                  />
              </FormControl>
              <FormControlLabel
                control={<Checkbox value="remember" 
                                   color="primary"/>}
                label={translate('RememberMe')}
              />
              <Button
                type="submit"
                fullWidth
                variant="contained"
                color="secondary"
                className={classes.submit}
              >
                {translate('Login')}
              </Button>
  
            </form>
          </Paper>
        </main>
      </MuiThemeProvider>
    );
  }
}
export default withStyles(Style)(Login);