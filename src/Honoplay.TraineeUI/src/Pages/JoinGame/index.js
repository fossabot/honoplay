import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import WithAuth from "../../Hoc/CheckAuth";
import { Code } from "../../Assets/index";
import { Button } from "../../Components/Button";
import History from "../../Helpers/History";

class JoinGame extends Component {
  render() {
    return (
      <PageWrapper>
        <div className="row pt-5 pb-5">
          <div className="col-sm-7">
            <p className="mt-5 mb-3 font-weight-bold">
              Lütfen Oyun Katılım Kodunuzu Giriniz
            </p>
            <div className="form">
              <input type="text" className="form-control" placeholder="---" />
              <Button
                onClick={() => History.push("/game")}
                title="Katıl"
                className="btn my-btn form-control mt-4"
              />
            </div>
          </div>
          <div className="col-sm-5 text-center">
            <img src={Code} className="img-fluid d-none d-sm-block" />
          </div>
        </div>
      </PageWrapper>
    );
  }
}

export default WithAuth(JoinGame);
