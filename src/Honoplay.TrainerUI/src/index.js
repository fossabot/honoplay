import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import { Provider } from "react-redux";
import { store, persistor } from "./Redux/store";
import { PersistGate } from "redux-persist/integration/react";
import "./Helpers/Extensions";
import * as serviceWorker from "./serviceWorker";
import { init } from "@omegabigdata/terasu-api-proxy";
import { HONOPLAY_TRAINER_PROJECT_ID } from "./Helpers/Statics";

init(HONOPLAY_TRAINER_PROJECT_ID);

ReactDOM.render(
  <Provider store={store}>
    <PersistGate loading={null} persistor={persistor}>
      <App />
    </PersistGate>
  </Provider>,
  document.getElementById("root")
);

serviceWorker.register();
