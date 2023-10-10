import {combineReducers} from 'redux'
import { connectRouter } from 'connected-react-router'
import AuthReducers from './components/membership/reducers';
import CategoryReducers from './components/categories/reducers';
import ProductReducers from './components/products/reducers';
import MerchantReducers from './components/merchants/reducers';
import FilterReducers from './components/filters/reducers';

const createRootReducer = (history) => combineReducers({
    router: connectRouter(history),
    Category: CategoryReducers,
    Product: ProductReducers,
    Merchant: MerchantReducers,
    Filters: FilterReducers
  })

export default createRootReducer