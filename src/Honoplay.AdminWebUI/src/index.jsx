
import React from 'react';
import { render } from 'react-dom';
import App from './App';
import Login from './views/Login/Login';
import { init } from '@omegabigdata/terasu-api-proxy';
import { Provider } from 'react-redux';
import { BrowserRouter as Router, Route } from 'react-router-dom';
import { projeId } from './helpers/Terasu';
import store from './redux/store';

init(projeId);

render(
    <Provider store={store}>
        <Router>
            <Route exact path="/login" component={Login} />
            <Route path="/honoplay" component={App} />
        </Router>
    </Provider>,
    document.getElementById('root'),
);

