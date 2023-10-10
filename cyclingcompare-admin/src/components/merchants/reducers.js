import * as actionTypes from './actionTypes';

export default function(state={Merchants: []}, action){
    switch(action.type){        
        case actionTypes.GET_ALL_MERCHANTS_COMPLETE:
            return {...state, Merchants: action.data }
        default:
            return state;
    }
}

