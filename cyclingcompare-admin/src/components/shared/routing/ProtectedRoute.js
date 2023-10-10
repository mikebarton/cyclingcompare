import React from 'react';
import firebase from 'firebase';
import { Route, Redirect } from 'react-router';
import { connect } from 'react-redux';



const ProtectedRoute =  function({ component: Component,   ...rest }) {

  function isAuthenticated(){
    var user = firebase.auth().currentUser;
    if(user){      
      return true;
    }
    return false;
  }

    return (
      <Route
        {...rest}
        render={props =>
          isAuthenticated() ? (
            <Component {...props} />
          ) : (
            <Redirect
              to={`/membership?redirect=` + props.location.pathname }
            />
          )
        }
      />
    );
  }

var mapStateToProps = state => ({
});

var mapDispatchToProps = dispatch => ({
    
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ProtectedRoute);
