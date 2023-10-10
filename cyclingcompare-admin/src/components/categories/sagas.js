import { takeEvery, put, call, all, take } from 'redux-saga/effects'
import * as actionTypes from './actionTypes';
import { GetAllCategoriesFlattened, GetAllCategoryHierarchies, UpdateCategory, DeleteCategory, CreateCategory, GetExternalCategories, GetCategoryMappings, UpdateCategoryMapping} from './api'

function* retrieveFlattenedCategories(){
    while(true){
        var action = yield take(actionTypes.RETRIEVE_FLAT_CATEGORIES)
        var json = yield call(GetAllCategoriesFlattened)
        var completeAction = { type: actionTypes.RETRIEVE_FLAT_CATEGORIES_COMPLETE, data: json}
        yield put(completeAction)
    }
}

function* retrieveHierarchyCategories(){
    while(true){
        var action = yield take(actionTypes.RETRIEVE_HIERARCHY_CATEGORIES)
        var json = yield call(GetAllCategoryHierarchies)
        var completeAction = { type: actionTypes.RETRIEVE_HIERARCHY_CATEGORIES_COMPLETE, data: json}
        yield put(completeAction)
    }
}

function* retrieveExternalCategories(){
    while(true){
        var action = yield take(actionTypes.RETRIEVE_EXTERNAL_CATEGORIES);
        var json = yield call(GetExternalCategories);
        var completeAction = { type: actionTypes.RETRIEVE_EXTERNAL_CATEGORIES_COMPLETE, data: json}
        yield put(completeAction);
    }
}

function* processUpdateCategory(action){
    var json = yield call(UpdateCategory, action.category)
    yield put({type: actionTypes.RETRIEVE_HIERARCHY_CATEGORIES})
    yield put({type: actionTypes.RETRIEVE_FLAT_CATEGORIES})
}

function* updateCategory(){
    yield takeEvery(actionTypes.UPDATE_CATEGORY, processUpdateCategory);        
}

function* processDeleteCategory(action){
    var json = yield call(DeleteCategory, action.categoryId)
    yield put({type: actionTypes.RETRIEVE_HIERARCHY_CATEGORIES})
    yield put({type: actionTypes.RETRIEVE_FLAT_CATEGORIES})
}

function* deleteCategory(){
    yield takeEvery(actionTypes.DELETE_CATEGORY, processDeleteCategory)
}

function* createCategory(){
    while(true){
        var action = yield take(actionTypes.CREATE_CATEGORY);
        var json = yield call(CreateCategory, action.data);
        yield put({type: actionTypes.RETRIEVE_HIERARCHY_CATEGORIES})
        yield put({type: actionTypes.RETRIEVE_FLAT_CATEGORIES})
    }
}

function* retrieveCategoryMappings(){
    while(true){
        var action = yield take(actionTypes.GET_CATEGORY_MAPPINGS);
        var json = yield call(GetCategoryMappings);
        var completeAction = {type: actionTypes.GET_CATEGORY_MAPPINGS_COMPLETE, data: json}
        yield put(completeAction);
    }
}

function* UpdateCategoryMappingSaga(){
    function* processMe(action) {
        var json = yield call(UpdateCategoryMapping, action.mapping);
        yield put({type: actionTypes.GET_CATEGORY_MAPPINGS});
    }

    yield takeEvery(actionTypes.UPDATE_CATEGORY_MAPPING, processMe);
}



export default [retrieveFlattenedCategories(), 
                updateCategory(), 
                deleteCategory(), 
                retrieveHierarchyCategories(), 
                createCategory(), 
                retrieveExternalCategories(), 
                retrieveCategoryMappings(),
                UpdateCategoryMappingSaga()];