import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import PrivateRoot from './services/privateRoot'

import Home from "./views/home";
import CallBack from "./views/callBack";

import userManager from "./services/userManager";

function App() {
  return (
    <Router>
      <div className="App">
        <Switch>
          <Route path="/callback" component={CallBack} />
          <PrivateRoot userManager={userManager} path="/" component={Home} />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
