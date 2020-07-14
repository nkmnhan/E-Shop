import React from "react";
import { connect } from "react-redux";
import CallbackComponent from "../components/CallbackComponent";
import userManager from "../services/userManager";

class CallbackPage extends React.Component {
  render() {
    // just redirect to '/' in both cases
    return (
      <CallbackComponent
        userManager={userManager}
        successCallback={() => {
          this.props.history.push("/");
        }}
        errorCallback={(error) => {
          this.props.history.push("/");
          console.error(error);
        }}
      >
        <div>Redirecting...</div>
      </CallbackComponent>
    );
  }
}

export default connect()(CallbackPage);
