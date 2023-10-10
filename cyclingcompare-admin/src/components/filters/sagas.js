import { SNOWFLAKE } from '@blueprintjs/icons/lib/esm/generated/iconContents';
import { takeEvery, put, call, all, take } from 'redux-saga/effects'
import * as actionTypes from './actionTypes';
import { DeleteFilterGroup, RelocateFilter, AddOrUpdateFilterGroup, GetFilterGroups, GetFiltersByCategory, GetTranslations, AddOrUpdateFilter, DeleteFilter, GetTranslationsByFilterId, AddOrUpdateTranslation, DeleteTranslation, GetImagesForTranslation } from './api'

function* retrieveFiltersByCategory(){
    while(true){
        var action = yield take(actionTypes.GET_FILTERS_BY_CATEGORY)
        var json = yield call(GetFiltersByCategory, action.categoryId, action.filterGroupId)
        var completeAction = { type: actionTypes.GET_FILTERS_BY_CATEGORY_COMPLETE, data: json, filterGroupId: action.filterGroupId}
        yield put(completeAction)
    }
}

function* retrieveUnmappedTranslations(){
    while(true){
        var action = yield take(actionTypes.GET_UNMAPPED_TRANSLATIONS_BY_CATEGORY)
        var json = yield call(GetTranslations, action.filterGroupId, true)
        var completeAction = { type: actionTypes.GET_UNMAPPED_TRANSLATIONS_BY_CATEGORY_COMPLETE, data: json, filterGroupId: action.filterGroupId}
        yield put(completeAction)
    }
}

function* retrieveMappedTranslationsByCategory(){
    while(true){
        var action = yield take(actionTypes.GET_MAPPED_TRANSLATIONS_BY_CATEGORY);
        var json = yield call(GetTranslations, action.filterGroupId, false);
        var completeAction = { type: actionTypes.GET_MAPPED_TRANSLATIONS_BY_CATEGORY_COMPLETE, data: json, filterGroupId: action.filterGroupId}
        yield put(completeAction);
    }
}

function* addOrUpdateFilter(){
    while(true){
        var action = yield take(actionTypes.ADD_OR_UPDATE_FILTER)
        var json = yield call(AddOrUpdateFilter, action.filter)
        var completeAction = { type: actionTypes.GET_FILTERS_BY_CATEGORY, categoryId: action.filter.categoryId, filterGroupId: action.filter.categoryFilterGroupId}
        yield put(completeAction)
    }
}

function* deleteFilter(){
    while(true){
        var action = yield take(actionTypes.DELETE_FILTER)
        var json = yield call(DeleteFilter, action.filterId)
        var completeAction = { type: actionTypes.GET_FILTERS_BY_CATEGORY, categoryId: action.categoryId, filterGroupId: action.filterGroupId}
        yield put(completeAction)
    }
}

function* getTranslationsById(){
    while(true){
        var action = yield take(actionTypes.GET_TRANSLATIONS_BY_ID)
        var json = yield call(GetTranslationsByFilterId, action.filterId)
        var completeAction = { type: actionTypes.GET_TRANSLATIONS_BY_ID_COMPLETE, filterId: action.filterId, data: json}
        yield put(completeAction)
    }
}

function* addOrUpdateTranslation(){
    while(true){
        var action = yield take(actionTypes.ADD_OR_UPDATE_TRANSLATION)
        var json = yield call(AddOrUpdateTranslation, action.translation)
        var unmappedAction = { type: actionTypes.GET_UNMAPPED_TRANSLATIONS_BY_CATEGORY, categoryId: action.categoryId, filterGroupId: action.filterGroupId}
        yield put(unmappedAction)
        var getTranslationAction = { type: actionTypes.GET_TRANSLATIONS_BY_ID, filterId: action.translation.categoryFilterId}
        yield put(getTranslationAction)
    }
}

function* addOrUpdateTranslationBatch(){
    while(true){
        var action = yield take(actionTypes.ADD_OR_UPDATE_TRANSLATION_BATCH)              

        var puts = action.translations.map(x=> { return put({...action, type: actionTypes.ADD_OR_UPDATE_TRANSLATION, translation: x})});
        yield all(puts);
    }
}

function* deleteTranslation(){
    while(true){
        var action = yield take(actionTypes.DELETE_TRANSLATION_BY_ID)
        var json = yield call(DeleteTranslation, action.translationId)
        var unmappedAction = { type: actionTypes.GET_UNMAPPED_TRANSLATIONS_BY_CATEGORY, categoryId: action.categoryId, filterType: action.filterType}
        yield put(unmappedAction)
        var getTranslationAction = { type: actionTypes.GET_TRANSLATIONS_BY_ID, filterId: action.filterId}
        yield put(getTranslationAction)
    }
}

function* getFilterGroups(){
    while(true){
        var action = yield take(actionTypes.GET_FILTER_GROUPS_BY_CATEGORY)
        var json = yield call(GetFilterGroups, action.categoryId)
        var completeAction = { type: actionTypes.GET_FILTER_GROUPS_BY_CATEGORY_COMPLETE, categoryId: action.categoryId, data: json}
        yield put(completeAction)        
    }
}

function* addOrUpdateFilterGroup(){
    while(true){
        var action = yield take(actionTypes.ADD_OR_UPDATE_FILTER_GROUP)
        var json = yield call(AddOrUpdateFilterGroup, action.filterGroup)
        var getAction = { type: actionTypes.GET_FILTER_GROUPS_BY_CATEGORY, categoryId: action.filterGroup.categoryId}
        yield put(getAction);
    }
}

function* deleteFilterGroup(){
    while(true){
        var action = yield take(actionTypes.DELETE_FILTER_GROUP)
        var json = yield call(DeleteFilterGroup, action.filterGroupId)
        var getAction = { type: actionTypes.GET_FILTER_GROUPS_BY_CATEGORY, categoryId: action.categoryId}
        yield put(getAction);
    }
}

function* relocateFilter(){
    while(true){
        var action = yield take(actionTypes.RELOCATE_FILTER)
        var json = yield call(RelocateFilter, action.filterId, action.targetGroupId)
        var getAction = { type: actionTypes.GET_FILTER_GROUPS_BY_CATEGORY, categoryId: action.categoryId}
        yield put(getAction);
    }
}

export default [retrieveFiltersByCategory(), 
                retrieveUnmappedTranslations(), 
                addOrUpdateFilter(), 
                deleteFilter(), 
                getTranslationsById(), 
                addOrUpdateTranslation(),
                deleteTranslation(),
                addOrUpdateTranslationBatch(),
                retrieveMappedTranslationsByCategory(),
                getFilterGroups(),
                addOrUpdateFilterGroup(),
                deleteFilterGroup(),
                relocateFilter()];