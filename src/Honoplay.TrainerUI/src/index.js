import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import { Provider } from "react-redux";
import store from "./Redux/store";
import "./Helpers/Extensions";
import * as serviceWorker from "./serviceWorker";
import { init } from "@omegabigdata/terasu-api-proxy";
import { HONOPLAY_TRAINER_PROJECT_ID } from "./Helpers/Statics";
init(HONOPLAY_TRAINER_PROJECT_ID);

setTimeout(() => {
  ReactDOM.render(
    <Provider store={store}>
      <App />
    </Provider>,
    document.getElementById("root")
  );
}, 500);
serviceWorker.register();
