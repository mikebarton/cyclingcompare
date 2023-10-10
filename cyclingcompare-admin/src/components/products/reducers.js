import * as actionTypes from './actionTypes';

export default function(state={GlobalProducts: []}, action){
    switch(action.type){        
        case actionTypes.GET_ALL_PRODUCT_SUMMARIES_COMPLETE:
            return {...state, GlobalProducts: action.data }
        case actionTypes.SET_PRODUCT_REVIEWED_COMPLETE:
            return {...state, GlobalProducts: [...state.GlobalProducts.filter(x=> x.globalProductId !== action.productId), {...state.GlobalProducts.find(x=> x.globalProductId === action.productId), isReviewed: true}]}
            case actionTypes.SET_PRODUCT_NOT_REVIEWED_COMPLETE:
            return {...state, GlobalProducts: [...state.GlobalProducts.filter(x=> x.globalProductId !== action.productId), {...state.GlobalProducts.find(x=> x.globalProductId === action.productId), isReviewed: false}]}
        default:
            return state;
    }
}

