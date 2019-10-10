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

class Login extends Component {
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
              type="text"
              className="form-control"
              placeholder={MobilePhoneAndEmail}
            />
            <input
              type="password"
              className="form-control mt-3 mb-2"
              placeholder={Password}
            />

            <a href="#" className="su">
              <u>{ForgotPassword}</u>
            </a>

            <Button
              onClick={() => History.push("/joingame")}
              title={LoginKey}
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

export default Login;
