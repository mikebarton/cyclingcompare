import { takeEvery, put, call, all, take } from 'redux-saga/effects'
import * as actionTypes from './actionTypes';
import { GetAllProductSummaries, GetGlobalProduct, UpdateGlobalProduct, PublishGlobalProduct, UnpublishGlobalProduct, SetAsReviewed, SetAsNotReviewed } from './api'


function* getProductSummaries(){
    while(true){
        var action = yield take(actionTypes.GET_ALL_PRODUCT_SUMMARIES);
        var json = yield call(GetAllProductSummaries);
        var completeAction = {type: actionTypes.GET_ALL_PRODUCT_SUMMARIES_COMPLETE, data: json}
        yield put(completeAction);
    }
}

// function* getGlobalProduct(){
//     while(true){
//         var action = yield take(actionTypes.GET_GLOBAL_PRODUCT_DETAILS);
//         var json = yield call(GetGlobalProduct);
//         var completeAction = { type: actionTypes.GET_GLOBAL_PRODUCT_DETAILS_COMPLETE, data: json };
//         yield put(completeAction);
//     }
// }

function* updateGlobalProduct(){
    while(true){
        var action = yield take(actionTypes.UPDATE_GLOBAL_PRODUCT);
        yield call(UpdateGlobalProduct, action.product)
    }
}

function* publishGlobalProduct(){
    while(true){
        var action = yield take(actionTypes.PUBLISH_GLOBAL_PRODUCT);
        yield call(PublishGlobalProduct, action.productId);
    }
}

function* unpublishGlobalProduct(){
    while(true){
        var action = yield take(actionTypes.UNPUBLISH_GLOBAL_PRODUCT);
        yield call(UnpublishGlobalProduct, action.productId);
    }
}

function* setReviewed(){
    while(true){
        var action = yield take(actionTypes.SET_PRODUCT_REVIEWED);
        yield call(SetAsReviewed, action.productId);
        var completeAction = { ...action, type: actionTypes.SET_PRODUCT_REVIEWED_COMPLETE };
        yield put(completeAction);
    }
}

function* setNotReviewed(){
    while(true){
        var action = yield take(actionTypes.SET_PRODUCT_NOT_REVIEWED);
        yield call(SetAsNotReviewed, action.productId);
        var completeAction = { ...action, type: actionTypes.SET_PRODUCT_NOT_REVIEWED_COMPLETE };
        yield put(completeAction);
    }
}

export default [getProductSummaries(), updateGlobalProduct(), publishGlobalProduct(), unpublishGlobalProduct(), setReviewed(), setNotReviewed()];