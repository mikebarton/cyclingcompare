import { applyMiddleware, compose, createStore } from 'redux';
import { routerMiddleware } from 'connected-react-router'
import createSagaMiddleware from 'redux-saga'
import saga from '../sagas';
import createRootReducer from '../reducers';
import { createBrowserHistory } from 'history';

export const history = createBrowserHistory();

export default function configureStore(initialState) {

  // In development, use the browser's Redux dev tools extension if installed
  const enhancers = [];
  const isDevelopment = process.env.NODE_ENV === 'development';
  if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
    enhancers.push(window.devToolsExtension());
  }

  const sagaMiddleware = createSagaMiddleware()
  const middleware = compose(
    applyMiddleware(
      sagaMiddleware,
      routerMiddleware(history)
      ),    
    ...enhancers
  );

  const store = createStore(
    createRootReducer(history),
    initialState,
    middleware
  );
  
  sagaMiddleware.run(saga)
  return store;
}
