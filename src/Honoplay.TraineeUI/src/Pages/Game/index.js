import React, { Component } from "react";
import WithAuth from "../../Hoc/CheckAuth";
import PageWrapper from "../../Containers/PageWrapper";
import { Button } from "../../Components/Button";
import History from "../../Helpers/History";
import { FinishGame } from "../../Helpers/TerasuKeys";

class Game extends Component {
  render() {
    return (
      <PageWrapper>
        <iframe
          src="https://gp.omegabigdata.com/P2bcbe503"
          width="1000"
          height="1000"
        ></iframe>
        <Button
          onClick={() => History.push("/endgame")}
          title={FinishGame}
          className="btn my-btn form-control mt-4"
        ></Button>
      </PageWrapper>
    );
  }
}

export default WithAuth(Game);
