import React from "react";
import { render } from "react-dom";
import App from "./App";
import { Provider } from "react-redux";
import { Router as BrowserRouter, Route } from "react-router-dom";
import store from "./Redux/store";
import Login from "./Pages/Login";
import Home from "./Pages/Home/index";
import Training from "./Pages/Training";
import JoinCode from "./Pages/JoinCode";
import History from "./Helpers/History";

render(
  <Provider store={store}>
    <BrowserRouter history={History}>
      <Route exact path="/" component={Home} />
      <Route path="/login" component={Login} />
      <Route path="/homepage" component={Home} />
      <Route path="/trainingdetail" component={Training} />
      <Route path="/joincode" component={JoinCode} />
    </BrowserRouter>
  </Provider>,
  document.getElementById("root")
);
