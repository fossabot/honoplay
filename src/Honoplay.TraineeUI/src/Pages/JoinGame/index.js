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
import { getAvatar } from "@omegabigdata/honoplay-redux-helper/Src/actions/TraineeUserAvatar";
import { connect } from "react-redux";

class JoinGame extends Component {
  componentDidMount() {
    const avatarId = JSON.parse(localStorage.getItem("traineeUserData"))
      .avatarId;

    this.props.getAvatar(avatarId);
  }

  render() {
    return (
      <React.Fragment>
        <SettingsPanel ImageByte={this.props.traineeAvatar} />
        <PageWrapper>
          <div className="shadow-sm box p-4">
            <div className="row pt-5 pb-5">
              <div className="col-sm-7">
                <p className="mt-5 mb-3 font-weight-bold">
                  {translate(PleaseEnterYourGameParticipationCode)}
                </p>

                <div className="form">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="---"
                  />

                  <button
                    onClick={() => History.push("/game")}
                    title={translate(Join)}
                    className="btn my-btn form-control mt-4"
                  >
                    Katıl
                  </button>
                </div>
              </div>
              <div className="col-sm-5 text-center">
                <img src={Code} className="img-fluid d-none d-sm-block" />
              </div>
            </div>
          </div>
        </PageWrapper>
      </React.Fragment>
    );
  }
}

const mapStateToProps = state => {
  const {
    isTraineeAvatarIsLoading,
    traineeAvatar,
    errorTraineeAvatar
  } = state.fetchAvatar;

  return {
    isTraineeAvatarIsLoading,
    traineeAvatar,
    errorTraineeAvatar
  };
};

export default connect(
  mapStateToProps,
  {
    getAvatar
  }
)(WithAuth(JoinGame));
