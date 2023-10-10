import { connect } from 'react-redux';
import { useEffect } from 'react';
import * as actionTypes from './actionTypes';


function withCategories(ChildComponent){
    
    function Inner ({categories, retrieveCategories, ...rest}){
        useEffect(()=>{
            if(!categories || categories.length < 1){
                retrieveCategories();
            }
        })

        return <ChildComponent {...rest} categories={categories}/>
    }

    var mapStateToProps = state => ({
        categories: state.Category.FlattenedCategories
    });

    var mapDispatchToProps = dispatch => ({
        retrieveCategories: () => dispatch({ type: actionTypes.RETRIEVE_FLAT_CATEGORIES })    
    });

    return connect(
        mapStateToProps,
        mapDispatchToProps
    )(Inner);
}
    
export default withCategories;
