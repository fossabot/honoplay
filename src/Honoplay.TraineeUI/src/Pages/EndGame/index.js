import React from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Checked } from "../../Assets/index";
import { TheGameIsCompleteThankYouForYourParticipation } from "../../Helpers/TerasuKeys";
import { translate } from "@omegabigdata/terasu-api-proxy";

const EndGame = () => {
  return (
    <PageWrapper rowClass="pt-5 pb-5">
      <div className="col-sm-12 text-center">
        <img src={Checked} className="img-fluid" />
        <p className="mt-5 mb-3 font-weight-bold">
          {translate(TheGameIsCompleteThankYouForYourParticipation)}
        </p>
      </div>
    </PageWrapper>
  );
};

export default EndGame;
