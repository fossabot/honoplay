import React, { Component } from "react";
import History from "../Helpers/History";
import actionIndex from "@omegabigdata/honoplay-redux-helper/Src/actions";

const WithAuth = HocComponent => {
  return class extends Component {
    componentWillMount() {
      const { match } = this.props;

      if (match.url == "/trainer/logout") {
        localStorage.removeItem("token");
        History.push("/trainer/login");
      }

      const token = localStorage.getItem("token");

      if (token == null) {
        History.go(0);
      }

      if (token) {
        actionIndex.setTrainerUserToken(token);
      }
    }
    render() {
      return <HocComponent {...this.props}></HocComponent>;
    }
  };
};

export default WithAuth;
