import React, { Component } from "react";
import WithAuth from "../../Hoc/CheckAuth";
import PageWrapper from "../../Containers/PageWrapper";
import { Button } from "../../Components/Button";
import History from "../../Helpers/History";

class Game extends Component {
  render() {
    return (
      <PageWrapper>
        <iframe
          src="https://www.construct.net/en/free-online-games/construct-bouncepok-3876/play"
          width="1000"
          height="1000"
        ></iframe>
        <Button
          onClick={() => History.push("/endgame")}
          title="Oyunu bitir"
          className="btn my-btn form-control mt-4"
        ></Button>
      </PageWrapper>
    );
  }
}

export default WithAuth(Game);
