import { UserManager } from 'oidc-client';
import 'babel-polyfill';

const userManagerConfig = {
  client_id: 'back-office',
  client_secret: 'secret',
  redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/callback`,
  response_type: 'code',
  scope: 'openid profile eshop-api',
  authority: 'https://localhost:5001',
  silent_redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/silent_renew.html`,
  automaticSilentRenew: true,
  filterProtocolClaims: true,
  loadUserInfo: true,
};

const userManager = new UserManager(userManagerConfig);

export default userManager;