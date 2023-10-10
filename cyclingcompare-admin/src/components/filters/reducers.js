import * as actionTypes from './actionTypes';

export default function(state={FilterGroups: {}, FilterList: {}, UnmappedTranslations: {}, TranslationsByFilter: {}, TranslationsByCategory: {}, Images: {}}, action){
    switch(action.type){
        case actionTypes.GET_FILTERS_BY_CATEGORY_COMPLETE:
            return {...state, FilterList: {...state.FilterList, [action.filterGroupId]: action.data}};
        case actionTypes.GET_UNMAPPED_TRANSLATIONS_BY_CATEGORY_COMPLETE:
            return {...state, UnmappedTranslations: {...state.UnmappedTranslations, [action.filterGroupId]: action.data}};    
        case actionTypes.GET_MAPPED_TRANSLATIONS_BY_CATEGORY_COMPLETE:
            return {...state, TranslationsByCategory: {...state.TranslationsByCategory, [action.filterGroupId]: action.data}};    
        case actionTypes.GET_TRANSLATIONS_BY_ID_COMPLETE:
            return {...state, TranslationsByFilter: {...state.TranslationsByFilter, [action.filterId] : action.data}}    
        case actionTypes.GET_FILTER_GROUPS_BY_CATEGORY_COMPLETE:
            return {...state, FilterGroups: {...state.FilterGroups, [action.categoryId]: action.data}}
        default:
            return state;
    }
}
