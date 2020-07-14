import React from "react";
import logo from "../logo.svg";
import "../App.css";

import userManager from "../services/userManager";

class Home extends React.Component {
  onLoginButtonClick(event) {
    event.preventDefault();
    userManager.signinRedirect();
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          <button onClick={this.onLoginButtonClick}>login</button>
          <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn React
          </a>
        </header>
      </div>
    );
  }
}

export default Home;
