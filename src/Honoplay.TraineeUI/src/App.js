import React from "react";
import { Router as BrowserRouter, Route } from "react-router-dom";
import Login from "./Pages/Login";
import History from "./Helpers/History";
import JoinGame from "./Pages/JoinGame";
import Game from "./Pages/Game";
import EndGame from "./Pages/EndGame";
import Logout from "./Helpers/Logout";
import Settings from "./Pages/Settings";

class App extends React.Component {
  render() {
    return (
      <React.Fragment>
        <BrowserRouter history={History}>
          <Route exact path="/" component={Login} />
          <Route path="/login" component={Login} />
          <Route path="/joingame" component={JoinGame} />
          <Route path="/game" component={Game} />
          <Route path="/endgame" component={EndGame} />
          <Route path="/settings" component={Settings} />
          <Route path="/logout" component={Logout} />
        </BrowserRouter>
      </React.Fragment>
    );
  }
}

export default App;
