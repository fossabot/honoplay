import React from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Checked } from "../../Assets/index";

const EndGame = () => {
  return (
    <PageWrapper rowClass="pt-5 pb-5">
      <div className="col-sm-12 text-center">
        <img src={Checked} className="img-fluid" />
        <p className="mt-5 mb-3 font-weight-bold">
          Oyun tamamlandı. Katılımınız için teşekkürler.
        </p>
      </div>
    </PageWrapper>
  );
};

export default EndGame;
