import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { Route, Switch } from 'react-router-dom';
import './App.css';
import firebase from './helpers/firestore';
import Membership from './screens/membership';
import Home from './screens/home';
import Categories from './screens/categories'
import CategoryMappings from './screens/categoryMapping';
import GlobalProducts from './screens/globalProducts';
import ProductMapping from './screens/productMappings';
import Merchants from './screens/merchants';
import FilterConfig from './screens/filterConfig';
import * as actionTypes from './components/membership/actionTypes';
import ProtectedRoute from './components/shared/routing/ProtectedRoute';
import { ConnectedRouter } from 'connected-react-router'
import { history } from './helpers/configureStore';




function App(props) {
  useEffect(() => {
    var unsub = firebase.auth().onAuthStateChanged(user => {
      props.storeAuthStateData(user, window.location.pathname);
    });
    return () => {
      unsub();
    }
  }, []);

  return (
        <ConnectedRouter history={history} >
          <Switch>
            <ProtectedRoute exact path='/' component={Home} />
            <ProtectedRoute exact path='/categories' component={Categories} />
            <ProtectedRoute exact path='/category-mapping' component={CategoryMappings}/>
            <ProtectedRoute exact path='/global-products' component={GlobalProducts}/>
            <ProtectedRoute exact path='/product-mapping' component={ProductMapping}/>
            <ProtectedRoute exact path='/filter-config' component={FilterConfig}/>
            <ProtectedRoute exact path='/merchants' component={Merchants}/>
            <Route exact path='/membership' component={Membership} />
            <Route render={() => (<div>Route Miss</div>)} />
          </Switch>
        </ConnectedRouter>
  );
}

var mapStateToProps = state => ({

});

var mapDispatchToProps = dispatch => ({
  storeAuthStateData: (user, path) => dispatch({ type: actionTypes.AUTH_USER_CHANGE, user: user, path: path })
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(App);
