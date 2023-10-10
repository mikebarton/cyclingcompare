import { takeEvery, put, call, all, take } from 'redux-saga/effects'
import * as actionTypes from './actionTypes';
import { GetAllMerchants } from './api'

function* getMerchants(){
    while(true){
        var action = yield take(actionTypes.GET_ALL_MERCHANTS);
        var merchants = yield call(GetAllMerchants);
        var completeAction = { ...action, type: actionTypes.GET_ALL_MERCHANTS_COMPLETE, data: merchants };
        yield put(completeAction);
    }
}

export default [getMerchants()];