import React from 'react';
import { render } from 'react-dom';
import App from './App';
import Login from './views/Login/Login';
import { init } from '@omegabigdata/terasu-api-proxy';
import { Provider } from 'react-redux';
import { HashRouter as Router, Route, Switch } from 'react-router-dom';
import { projeId } from './helpers/Terasu';
import store from './redux/store';
import { createBrowserHistory } from 'history';

const history = createBrowserHistory();

init(projeId);

render(
  <Provider store={store}>
    <Router history={history}>
      <Switch>
        <Route exact path="/login" component={Login} />
        <Route path="/" component={App} />
      </Switch>
    </Router>
  </Provider>,
  document.getElementById('root')
);
