import { createStore } from "redux";
import rootReducer from "./reducers";
import loadUser from '../services/loadUser'
import userManager from "../services/userManager"

const store = createStore(
  rootReducer,
  window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
);

loadUser(store, userManager);

export default store;
