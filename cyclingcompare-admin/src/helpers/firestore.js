import firebase from 'firebase';
import Config from '../config';  
  
  var config = { 
    apiKey: Config.IdentityPlatform.ApiKey,
    authDomain: Config.IdentityPlatform.AuthDomain,
  };
  firebase.initializeApp(config);

  export default firebase;