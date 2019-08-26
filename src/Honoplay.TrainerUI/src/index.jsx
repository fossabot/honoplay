import React from "react";
import { render } from "react-dom";
import App from "./App";
import { Provider } from "react-redux";
import { BrowserRouter as Router, Route } from "react-router-dom";
import store from "./Redux/store";
import Login from "./Pages/Login";
import Home from "./Pages/Home/index";
import Training from "./Pages/Training";

render(
  <Provider store={store}>
    <Router>
      <Route exact path="/" component={Home} />
      <Route path="/login" component={Login} />
      <Route path="/homepage" component={Home} />
      <Route path="/trainingdetail" component={Training} />
    </Router>
  </Provider>,
  document.getElementById("root")
);
