import { addReducerToList } from "@omegabigdata/honoplay-redux-helper/Src/reducers";

import { createStore, applyMiddleware, compose } from "redux";

import thunk from "redux-thunk";

import { persistStore, persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage"; // defaults to localStorage for web

const persistConfig = {
  key: "root",
  storage
};

const persistedReducer = persistReducer(persistConfig, addReducerToList({}));

let store = createStore(persistedReducer, {}, compose(applyMiddleware(thunk)));
let persistor = persistStore(store);

export { store, persistor };
