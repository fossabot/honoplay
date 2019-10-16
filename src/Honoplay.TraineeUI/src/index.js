import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import store from "./Redux/store";
import "./Helpers/Extensions";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import { init } from "@omegabigdata/terasu-api-proxy";
import { HONOPLAY_TRAINEE_PROJECT_ID } from "./Helpers/Statics";

init(HONOPLAY_TRAINEE_PROJECT_ID);
setTimeout(() => {
  render(
    <Provider store={store}>
      <App />
    </Provider>,
    document.getElementById("root")
  );
}, 650);

serviceWorker.register();
