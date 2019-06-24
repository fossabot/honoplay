import React from 'react';
import { render } from 'react-dom';
import App from './App';
import Login from './views/Login/Login';
import { init } from '@omegabigdata/terasu-api-proxy';
import { Provider } from 'react-redux';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import store from './redux/store';

init(3);

render(
    <Provider store={store}>
        <Router>
            <Route exact path="/" component={Login} />
            <Route path="/home" component={App} />
        </Router>
    </Provider>,
    document.getElementById('root'),
);