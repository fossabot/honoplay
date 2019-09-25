import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import { Router as BrowserRouter, Route } from "react-router-dom";
import store from "./Redux/store";
import Login from "./Pages/Login";
import History from "./Helpers/History";
import JoinGame from "./Pages/JoinGame";
import Game from "./Pages/Game";
import EndGame from "./Pages/EndGame";
import "./Helpers/Extensions";

render(
  <Provider store={store}>
    <BrowserRouter history={History}>
      <Route exact path="/" component={Login} />
      <Route path="/login" component={Login} />
      <Route path="/joingame" component={JoinGame} />
      <Route path="/game" component={Game} />
      <Route path="/endgame" component={EndGame} />
    </BrowserRouter>
  </Provider>,
  document.getElementById("root")
);
