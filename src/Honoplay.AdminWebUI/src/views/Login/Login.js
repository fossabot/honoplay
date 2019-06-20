import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import CssBaseline from '@material-ui/core/CssBaseline';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import { Paper, Checkbox, 
         FormControlLabel, FormControl, Button, 
         Avatar, MuiThemeProvider,TextField, 
         Typography, CircularProgress } from '@material-ui/core';
import withStyles from '@material-ui/core/styles/withStyles';
import { Style, theme} from './Style';
import { connect } from 'react-redux';
import { fetchToken } from '@omegabigdata/honoplay-redux-helper/Src/actions/AdminUser';

class Login extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      email: " ",
      password: " ",
      error: false,
    }
    this.handleClick = this.handleClick.bind(this);
    this.handleChange = this.handleChange.bind(this);
  }

  componentDidUpdate(prevProps) {
    const { token, errorMessage } = this.props;
    if (token !== prevProps.token) {
      if(token) {
        this.props.history.push("/home");
      }
    }
    if (errorMessage !== prevProps.errorMessage)
    {
      if(errorMessage) {
        this.setState({error: !this.state.error})
      }
    }
  }

  handleChange(e) {
    const { name, value } = e.target;
    this.setState({ [name]: value });
    this.setState({
      error: false
    });
  }

  handleClick () {
    const { email, password } = this.state;
    this.props.fetchToken( email, password );
    this.setState({
      email: '',
      password: '',
      error: false
    })
  }

  render() {
    const { classes, isTokenLoading } = this.props;
    const { email, password, error } = this.state;
    return (
      <MuiThemeProvider theme={theme}>
        <main className={classes.main}>
          <CssBaseline />
          <Paper className={classes.paper}>
            { isTokenLoading ? 
                <CircularProgress 
                  className={classes.progress}  
                  disableShrink={true} 
                  color="primary" 
                /> 
              :
              <Avatar className={classes.avatar}>
                <LockOutlinedIcon />           
              </Avatar>
            }
            <Typography variant="h5" className={classes.typography}>
             {translate('Login')}
            </Typography>
            <form className={classes.form}>
              <FormControl margin="normal"  fullWidth>
                  <TextField
                    error = { error && true }
                    label={translate('EmailAddress')}
                    id="email" 
                    name="email" 
                    autoComplete="email" 
                    autoFocus
                    onChange={this.handleChange}
                    value={email}
                  />
              </FormControl>
              <FormControl margin="normal"  fullWidth>
                  <TextField
                    error = { error && true }
                    className={classes.marginInput}
                    label={translate('Password')}
                    name="password" 
                    type="password" 
                    id="password" 
                    autoComplete="current-password"
                    onChange={this.handleChange} 
                    value={password}
                  />
              </FormControl>
              <FormControlLabel
                control={<Checkbox value="remember" 
                                   color="primary"/>}
                label={translate('RememberMe')}
              />
              <Button
                fullWidth
                variant="contained"
                color="secondary"
                className={classes.button}
                onClick = {this.handleClick }              
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
const mapStateToProps = state => {
  const { isTokenLoading, 
          token, 
          errorMessage } = state.auth;
  return { isTokenLoading, 
           token, 
           errorMessage }
}

const mapDispatchToProps = {
  fetchToken
};

export default connect(mapStateToProps,mapDispatchToProps)(withStyles(Style)(Login));