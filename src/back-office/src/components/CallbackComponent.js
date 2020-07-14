import {useEffect} from "react";

function CallbackComponent(props) {

  useEffect(() => {
    props.userManager
      .signinRedirectCallback()
      .then((user) => onRedirectSuccess(user))
      .catch((error) => onRedirectError(error));
  })

  function onRedirectSuccess(user){
    props.successCallback(user);
  };

  function onRedirectError(error){
    if (props.errorCallback) {
      props.errorCallback(error);
    } else {
      throw new Error(`Error handling redirect callback: ${error.message}`);
    }
  };

    return (props.children);
}

export default CallbackComponent;
