import { takeEvery, take, put, call, all } from 'redux-saga/effects'

import * as actionTypes from './actionTypes';
import firebase from 'firebase';
import { push } from 'connected-react-router'


function* processAuthChange(){
    while(true){
        var action = yield take(actionTypes.AUTH_USER_CHANGE);
        if(action.user && !action.user.isAnonymous){            
            yield put(push('/global-products'))
        }        
    }
}

function* processSignout(){
    while(true){
        var action = yield take(actionTypes.SIGN_OUT);
        yield call([firebase.auth(), firebase.auth().signOut]);
        yield put(push('/membership'))
    }
}

function* watchLoginRequests(){
    while(true){
        var action = yield take(actionTypes.LOGIN)
        // yield call([firebase.auth(), firebase.auth().signInWithEmailAndPassword], action.email, action.password)
        yield call(login, action.email, action.password)
    }
}

const login = function(email, password){
    return firebase.auth().signInWithEmailAndPassword(email, password)
        .then(result=>{},
            err=>{});
}

export default [watchLoginRequests(), processAuthChange(), processSignout()];