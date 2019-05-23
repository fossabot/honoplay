import React from 'react';
import { render } from 'react-dom';
import App from './App';
import Login from './views/Login/Login';

import terasuProxy from '@omegabigdata/terasu-api-proxy';
terasuProxy.init(3);

import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from './reducers/rootReducers';
import { composeWithDevTools } from 'redux-devtools-extension';
import { Provider } from 'react-redux';
import { BrowserRouter, Route } from 'react-router-dom';

const store = createStore(
    rootReducer,
    composeWithDevTools(
        applyMiddleware(thunk)
    )
);

render(
    <BrowserRouter>
        <Provider store={store}>
            <Route exact path="/" component={Login} />
            <Route path="/home" component={App} />
        </Provider>
    </BrowserRouter>,
    document.getElementById('root'),
);