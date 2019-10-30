import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Logo, Vector } from "../../Assets/index";
import { Button } from "../../Components/Button";
import History from "../../Helpers/History";
import {
  MobilePhoneAndEmail,
  ForgotPassword,
  Password,
  LoginKey
} from "../../Helpers/TerasuKeys";
import { translate } from "@omegabigdata/terasu-api-proxy";
import { fethTraineeUserToken } from "@omegabigdata/honoplay-redux-helper/Src/actions/TraineeUser";
import { connect } from "react-redux";

class Login extends Component {
  componentDidUpdate(prevProps, nextState) {
    const {
      userTraineeTokenIsLoading,
      userTraineeToken,
      userTrainerTokenError
    } = this.props;

    if (
      prevProps.userTraineeTokenIsLoading &&
      !userTraineeTokenIsLoading &&
      userTraineeToken
    ) {
      localStorage.setItem(
        "traineeUserData",
        JSON.stringify(userTraineeToken.user)
      );
      localStorage.setItem("token", userTraineeToken.token);
      this.props.history.push("/joingame");
    }
  }

  componentDidMount() {
    if (localStorage.getItem("token")) {
      this.props.history.push("/joingame");
    }
  }

  email = "agha@omegabigdata.com";
  password = "123456789";
  render() {
    return (
      <PageWrapper className={"box shadow p-5"}>
        <div className="col-sm-7">
          <img src={Logo} className="img-fluid" />
          <p className="mt-3 mb-5">
            <b>Omega Bigdata</b> Web Portalına Hoş Geldiniz.
          </p>

          <div className="form">
            <input
              onChange={e => (this.email = e.target.value)}
              defaultValue="agha@omegabigdata.com"
              type="text"
              className="form-control"
              placeholder={translate(MobilePhoneAndEmail)}
            />
            <input
              onChange={e => (this.password = e.target.value)}
              defaultValue="123456789"
              type="password"
              className="form-control mt-3 mb-2"
              placeholder={translate(Password)}
            />

            <a href="#" className="su">
              <u>{translate(ForgotPassword)}</u>
            </a>

            <Button
              onClick={() =>
                this.props.fethTraineeUserToken({
                  email: this.email,
                  password: this.password
                })
              }
              title={translate(LoginKey)}
              className="btn my-btn form-control mt-4"
            />
            <Button
              className="btn my-btn2 form-control mt-3"
              title="İlk Giriş"
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
    userTraineeTokenIsLoading,
    userTraineeToken,
    userTraineeTokenError
  } = state.traineeUserToken;

  return {
    userTraineeTokenIsLoading,
    userTraineeToken,
    userTraineeTokenError
  };
};

export default connect(
  mapStateToProps,
  {
    fethTraineeUserToken
  }
)(Login);
