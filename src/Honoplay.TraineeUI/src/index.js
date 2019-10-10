import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import store from "./Redux/store";
import "./Helpers/Extensions";
import App from "./App";
import * as serviceWorker from "./serviceWorker";

render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById("root")
);

serviceWorker.register();
