﻿import React, { Component } from "react";
import { hot } from "react-hot-loader";
import { Route } from "react-router-dom";
import Layout from "./components/Layout/LayoutComponent";

import Questions from "./views/Questions/Questions";
import TenantInformation from "./views/TenantInformation/TenantInformation";
import Trainers from "./views/Trainers/Trainers";

import setToken from "@omegabigdata/honoplay-redux-helper/Src/actions/index";


class App extends Component {
  componentDidMount() {
    let token = localStorage.getItem("token");
    if (token) {
      console.log("token var :", token);
      setToken.setToken(token);
    }
  }

  render() {
    return (
      <Layout>
        <Route path="/home/questions" component={Questions} />
        <Route path="/home/tenantinformation" component={TenantInformation} />
        <Route path="/home/trainers" component={Trainers}/>
      </Layout>
    );
  }
}

export default hot(module)(App);
