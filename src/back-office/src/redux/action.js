import {
  USER_EXPIRED,
  SILENT_RENEW_ERROR,
  SESSION_TERMINATED,
  USER_EXPIRING,
  USER_FOUND,
  LOADING_USER,
  USER_SIGNED_OUT,
  LOAD_USER_ERROR,
} from "./actionTypes";

// dispatched when the existing user expired
export const userExpired = () => ({
  type: USER_EXPIRED,
});

// dispatched when a user has been found in storage
export const userFound = (user) => ({
  type: USER_FOUND,
  payload: user,
});

// dispatched when silent renew fails
// payload: the error
export const silentRenewError = (error) => ({
  type: SILENT_RENEW_ERROR,
  payload: error,
});

// dispatched when the user is logged out
export const sessionTerminated = () => ({
  type: SESSION_TERMINATED,
});

// dispatched when the user is expiring (just before a silent renew is triggered)
export const userExpiring = () => ({
  type: USER_EXPIRING,
});

// dispatched when a new user is loading
export const loadingUser = () => ({
  type: LOADING_USER,
});

export const userSignedOut = () => ({
  type: USER_SIGNED_OUT,
});

export const loadUserError = () => ({
  type: LOAD_USER_ERROR,
});
