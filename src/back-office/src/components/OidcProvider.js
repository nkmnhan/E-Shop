import PropTypes from "prop-types";
import { useState, useEffect } from "react";
import { connect } from "react-redux";
import {
  userExpired,
  userFound,
  silentRenewError,
  sessionTerminated,
  userExpiring,
  userSignedOut,
  loadingUser,
} from "../redux/action";

function OidcProvider(props) {
  const [userManager, setUserManager] = useState(props.userManager);
  
  useEffect(() => {
    userManager.events.addUserLoaded(onUserLoaded);
    userManager.events.addSilentRenewError(onSilentRenewError);
    userManager.events.addAccessTokenExpired(onAccessTokenExpired);
    userManager.events.addAccessTokenExpiring(onAccessTokenExpiring);
    userManager.events.addUserUnloaded(onUserUnloaded);
    userManager.events.addUserSignedOut(onUserSignedOut);
    
    return () => {
      userManager.events.removeUserLoaded(onUserLoaded);
      userManager.events.removeSilentRenewError(onSilentRenewError);
      userManager.events.removeAccessTokenExpired(onAccessTokenExpired);
      userManager.events.removeAccessTokenExpiring(onAccessTokenExpiring);
      userManager.events.removeUserUnloaded(onUserUnloaded);
      userManager.events.removeUserSignedOut(onUserSignedOut);
    };
  });

  // event callback when the user has been loaded (on silent renew or redirect)
  function onUserLoaded(user) {
    props.userFound(user);
  }

  // event callback when silent renew errored
  function onSilentRenewError(error) {
    props.silentRenewError(error);
  }

  // event callback when the access token expired
  function onAccessTokenExpired() {
    props.userExpired();
  }

  // event callback when the user is logged out
  function onUserUnloaded() {
    props.sessionTerminated();
  }

  // event callback when the user is expiring
  function onAccessTokenExpiring() {
    props.userExpiring();
  }

  // event callback when the user is signed out
  function onUserSignedOut() {
    props.userSignedOut();
  }

  return props.children;
}

const mapStateToProps = (state) => {
  return { oidc: state.userManager };
};

export default connect(mapStateToProps, {
  userExpired,
  userFound,
  silentRenewError,
  sessionTerminated,
  userExpiring,
  userSignedOut,
  loadingUser,
})(OidcProvider);
