import React from 'react';
import { render } from 'react-dom';
import {createStore,applyMiddleware,combineReducers } from 'redux';
import { Provider } from 'react-redux';
import App from './App';
import Login from './views/Login/Login';

var createHistory = require('history').createBrowserHistory;
import { Router, Route } from 'react-router';
import { routerReducer, routerMiddleware } from 'react-router-redux';

const history = createHistory();


const store = createStore(
    combineReducers({
        routing: routerReducer,
     }),
     applyMiddleware(routerMiddleware(history)),
);

render(
    <Provider store={store}>
       <Router history={history}>
            <Route exact path="/" component={Login} />
            <Route path="/home" component={App} />
       </Router>
    </Provider>,
    document.getElementById('root'),
);