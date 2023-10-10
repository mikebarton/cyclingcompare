import authSagas from './components/membership/sagas';
import categorySagas from './components/categories/sagas';
import productSagas from './components/products/sagas';
import merchantSagas from './components/merchants/sagas';
import filterSagas from './components/filters/sagas';
import { all } from 'redux-saga/effects'

export default function* rootSaga(){
    yield all ([
        ...authSagas,
        ...categorySagas,
        ...productSagas,
        ...merchantSagas,
        ...filterSagas
    ])
}