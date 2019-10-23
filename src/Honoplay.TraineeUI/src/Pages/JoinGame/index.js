import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import WithAuth from "../../Hoc/CheckAuth";
import { Code } from "../../Assets/index";
import { Button } from "../../Components/Button";
import History from "../../Helpers/History";
import {
  Join,
  PleaseEnterYourGameParticipationCode
} from "../../Helpers/TerasuKeys";
import { translate } from "@omegabigdata/terasu-api-proxy";
import SettingsPanel from "../../Containers/SettingsPanel/";

class JoinGame extends Component {
  render() {
    return (
      <React.Fragment>
        <SettingsPanel />
        <PageWrapper>
          <div class="shadow-sm box p-4">
            <div class="row pt-5 pb-5">
              <div class="col-sm-7">
                <p class="mt-5 mb-3 font-weight-bold">
                  {translate(PleaseEnterYourGameParticipationCode)}
                </p>

                <div class="form">
                  <input type="text" class="form-control" placeholder="---" />

                  <button
                    onClick={() => History.push("/game")}
                    title={translate(Join)}
                    class="btn my-btn form-control mt-4"
                  >
                    Katıl
                  </button>
                </div>
              </div>
              <div class="col-sm-5 text-center">
                <img src={Code} class="img-fluid d-none d-sm-block" />
              </div>
            </div>
          </div>
        </PageWrapper>
      </React.Fragment>
    );
  }
}

export default WithAuth(JoinGame);
