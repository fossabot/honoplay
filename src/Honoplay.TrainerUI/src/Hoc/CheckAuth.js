import React, { Component } from "react";
import History from "../Helpers/History";
import actionIndex from "@omegabigdata/honoplay-redux-helper/Src/actions";
import { store } from "../Redux/store";

const WithAuth = HocComponent => {
  return class extends Component {
    componentWillMount() {
      const { match } = this.props;

      if (match.url == "/logout") {
        localStorage.removeItem("token");
        this.props.history.push("/login");
      }

      const token = store.getState().trainerUserToken.userTrainerToken.token;
      const localStorageToken = localStorage.getItem("token");

      if (token == null && localStorageToken == null) {
        History.go(0);
      }

      if (token || localStorageToken) {
        actionIndex.setTrainerUserToken(token || localStorageToken);
      }
    }
    render() {
      return <HocComponent {...this.props}></HocComponent>;
    }
  };
};

export default WithAuth;
