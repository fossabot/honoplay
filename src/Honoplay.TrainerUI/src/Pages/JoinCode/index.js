import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import WithAuth from "../../Hoc/CheckAuth";
import { JoinCode as GameCode } from "../../Helpers/TerasuKeys";
import { translate } from "@omegabigdata/terasu-api-proxy";

class JoinCode extends Component {
  render() {
    return (
      <PageWrapper>
        <div class="col-sm-12">
          <div class="mt-5 text-center">
            <h3 class="font-weight-bold mb-3">{translate(GameCode)}</h3>
            <h1 class="font-weight-bold text-primary">1234AERWW</h1>
          </div>
        </div>
      </PageWrapper>
    );
  }
}

export default WithAuth(JoinCode);
