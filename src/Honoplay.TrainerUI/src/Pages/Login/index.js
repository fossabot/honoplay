import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Logo, Vector } from "../../Assets/index";
import { Button } from "../../Components/Button";
import History from "../../Helpers/History";
import { connect } from "react-redux";
import { fethTrainerUserToken } from "@omegabigdata/honoplay-redux-helper/Src/actions/TrainerUser";
import {
  ForgotPassword,
  Password,
  MobilePhoneAndEmail,
  LoginKey
} from "../../Helpers/TerasuKeys";
import { translate } from "@omegabigdata/terasu-api-proxy";

class Login extends Component {
  componentDidUpdate(prevProps, nextState) {
    const {
      userTrainerTokenIsLoading,
      userTrainerToken,
      userTrainerTokenError
    } = this.props;

    if (
      prevProps.userTrainerTokenIsLoading &&
      !userTrainerTokenIsLoading &&
      userTrainerToken
    ) {
      localStorage.setItem("token", userTrainerToken.token);
      this.props.history.push("/homepage");
    }
  }

  componentDidMount() {
    if (localStorage.getItem("token")) {
      this.props.history.push("/homepage");
    }
  }

  email = "agha@huseynov.com";
  password = "123456789";

  render() {
    return (
      <PageWrapper>
        <div className="col-sm-7">
          <img src={Logo} className="img-fluid" />
          <p className="mt-3 mb-5">
            <b>Omega Bigdata</b> Web Portalına Hoş Geldiniz.
          </p>

          <div className="form">
            <input
              defaultValue="agha@huseynov.com"
              onChange={e => (this.email = e.target.value)}
              type="email"
              className="form-control"
              placeholder={translate(MobilePhoneAndEmail)}
            />
            <input
              defaultValue="123456789"
              onChange={e => (this.password = e.target.value)}
              type="password"
              className="form-control mt-3 mb-2"
              placeholder={translate(Password)}
            />

            <a href="#" className="su">
              <u>{translate(ForgotPassword)}</u>
            </a>

            <Button
              onClick={() => {
                this.props.fethTrainerUserToken({
                  email: this.email,
                  password: this.password
                });
              }}
              title={translate(LoginKey)}
              className="btn my-btn form-control mt-4"
            />
          </div>
        </div>
        <div className="col-sm-5 text-center">
          <img src={Vector} className="img-fluid d-none d-sm-block" />
        </div>
      </PageWrapper>
    );
  }
}

const mapStateToProps = state => {
  const {
    userTrainerTokenIsLoading,
    userTrainerToken,
    userTrainerTokenError
  } = state.trainerUserToken;

  return { userTrainerTokenIsLoading, userTrainerToken, userTrainerTokenError };
};

export default connect(
  mapStateToProps,
  { fethTrainerUserToken }
)(Login);
