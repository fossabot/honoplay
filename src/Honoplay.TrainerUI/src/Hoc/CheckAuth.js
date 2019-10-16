import React, { Component } from "react";
import actionIndex from "@omegabigdata/honoplay-redux-helper/Src/actions";

const WithAuth = HocComponent => {
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
        actionIndex.setTrainerUserToken(localStorageToken);
      }
    }
    render() {
      return <HocComponent {...this.props}></HocComponent>;
    }
  };
};

export default WithAuth;
