import React, { useEffect } from "react";
import { connect } from "react-redux";
import { Route } from "react-router-dom";

function PrivateRoute({ userManager, oidc, children, ...rest }) {
  useEffect(() => {
    if (!oidc || !oidc.user || !oidc.user.access_token) {
      userManager.signinRedirect();
    }
  });
  return <Route {...rest} render={({ location }) => children} />;
}
const mapStateToProps = (state) => {
  return { oidc: state.userManager };
};

export default connect(mapStateToProps, null)(PrivateRoute);
