import * as actionTypes from './actionTypes';

export default function(state={FlattenedCategories: [], HierarchyCategories: []}, action){
    switch(action.type){
        case actionTypes.RETRIEVE_FLAT_CATEGORIES_COMPLETE:
            return {...state, FlattenedCategories: action.data};
        case actionTypes.RETRIEVE_HIERARCHY_CATEGORIES_COMPLETE:
            return {...state, HierarchyCategories: action.data};
        case actionTypes.RETRIEVE_EXTERNAL_CATEGORIES_COMPLETE:
            return { ...state, ExternalCategories: action.data};
        case actionTypes.UPDATE_CATEGORY_COMPLETE:
            return {...state, FlattenedCategories: [...state.FlattenedCategories.filter(x=>x.categoryId !== action.data.categoryId), action.data]}
        case actionTypes.DELETE_CATEGORY_COMPLETE:
            return {...state, FlattenedCategories: [...state.FlattenedCategories.filter(x=>x.categoryId !== action.data)]}        
        case actionTypes.GET_CATEGORY_MAPPINGS_COMPLETE:
            return {...state, Mappings: action.data}
        default:
            return state;
    }
}
