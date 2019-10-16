import React, { Component } from "react";
import History from "../Helpers/History";
import actionIndex from "@omegabigdata/honoplay-redux-helper/Src/actions";

export default function WithAuth(HocComponent) {
  return class extends Component {
    componentWillMount() {
      const { match } = this.props;

      if (match.url == "/logout") {
        localStorage.removeItem("token");
        this.props.history.push("/login");
      }

      const localStorageToken = localStorage.getItem("token");

      if (localStorageToken == null) {
        this.props.history.push("/login");
      }

      if (localStorageToken) {
        actionIndex.setTraineeUserToken(localStorageToken);
      }
    }
    render() {
      return <HocComponent {...this.props}></HocComponent>;
    }
  };
}
