import React from "react";
import { translate } from "@omegabigdata/terasu-api-proxy";
import CssBaseline from "@material-ui/core/CssBaseline";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import Visibility from '@material-ui/icons/Visibility';
import VisibilityOff from '@material-ui/icons/VisibilityOff';
import {
  Paper,
  Checkbox,
  FormControlLabel,
  FormControl,
  Button,
  Avatar,
  MuiThemeProvider,
  TextField,
  Typography,
  CircularProgress,
  InputAdornment,
  IconButton
} from "@material-ui/core";
import withStyles from "@material-ui/core/styles/withStyles";
import { Style, theme } from "./Style";
import { connect } from "react-redux";
import { fetchToken } from "@omegabigdata/honoplay-redux-helper/dist/Src/actions/AdminUser";


class Login extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      email: " ",
      password: " ",
      error: false
    };
    this.handleClick = this.handleClick.bind(this);
    this.handleChange = this.handleChange.bind(this);
  }

  componentDidUpdate(prevProps) {
    const { token, errorToken } = this.props;

    if (token !== prevProps.token) {
      console.log(token);
      if (token) {
        localStorage.setItem("token", token.token);
        this.props.history.push("/honoplay");
      }
    }
    if (errorToken !== prevProps.errorToken) {
      if (errorToken) {
        this.setState({ error: !this.state.error });
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

  handleClick() {
    const { email, password } = this.state;
    this.props.fetchToken(email, password);
    this.setState({
      email: "",
      password: "",
      error: false
    });
  }

  handleClickShowPassword = () => {
    this.setState(
      state => ({
        showPassword: !state.showPassword
      }));
  };

  render() {
    const { classes, isTokenLoading } = this.props;
    const { email, password, error } = this.state;
    return (
      <MuiThemeProvider theme={theme}>
        <main className={classes.main}>
          <CssBaseline />
          <Paper className={classes.paper}>
            {isTokenLoading ? (
              <CircularProgress
                className={classes.progress}
                disableShrink={true}
                color="primary"
              />
            ) : (
                <Avatar className={classes.avatar}>
                  <LockOutlinedIcon />
                </Avatar>
              )}
            <Typography variant="h5" className={classes.typography}>
              {translate("Login")}
            </Typography>
            <form className={classes.form}>
              <FormControl margin="normal" fullWidth>
                <TextField
                  error={error && true}
                  label={translate("EmailAddress")}
                  id="email"
                  name="email"
                  autoComplete="email"
                  autoFocus
                  onChange={this.handleChange}
                  value={email}
                />
              </FormControl>
              <FormControl margin="normal" fullWidth>
                <TextField
                  error={error && true}
                  className={classes.marginInput}
                  label={translate("Password")}
                  name="password"
                  type={this.state.showPassword ? 'text' : 'password'}
                  id="password"
                  autoComplete="current-password"
                  onChange={this.handleChange}
                  value={password}
                  InputProps={{
                    endAdornment: (
                      <InputAdornment position="end">
                        <IconButton
                          onClick={this.handleClickShowPassword}
                        >
                          {this.state.showPassword ? <VisibilityOff /> : <Visibility />}
                        </IconButton>
                      </InputAdornment>
                    ),
                  }}
                />
              </FormControl>
              <Button
                fullWidth
                variant="contained"
                color="secondary"
                className={classes.button}
                onClick={this.handleClick}
              >
                {translate("Login")}
              </Button>
            </form>
          </Paper>
        </main>
      </MuiThemeProvider>
    );
  }
}
const mapStateToProps = state => {
  const { isTokenLoading, token, errorToken } = state.auth;
  return { isTokenLoading, token, errorToken };
};

const mapDispatchToProps = {
  fetchToken
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withStyles(Style)(Login));
