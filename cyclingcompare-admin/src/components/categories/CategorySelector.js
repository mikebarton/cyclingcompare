import { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { Select } from "@blueprintjs/select";
import { MenuItem, Button} from '@blueprintjs/core';
import * as styles from './categories.module.scss';
import * as actionTypes from './actionTypes';

function CategorySelector({categories, retrieveCategories, categoryId, onCategorySelected}){
    var [parentCategory, setParentCategory] = useState({});
    var [childCategory, setChildCategory] = useState({});
    
    useEffect(()=>{
        retrieveCategories();
    },[])

    useEffect(()=>{
        if(categoryId && categories){}
        categories.forEach(x => {
            x.children.forEach(y => {
                if(y.categoryId===categoryId){
                    setParentCategory(x);
                    setChildCategory(y);
                }
            });
        });
    
    },[categoryId, categories])

    function internalCategorySelected(category){
        setChildCategory(category);
        if(onCategorySelected)
            onCategorySelected(category)
    }

    return (
        <>
        <Select items={categories} 
                itemRenderer={(x)=><MenuItem key={x.categoryId} text={x.title} onClick={()=>setParentCategory(x)}/>}
        >
            <Button className={styles.fullWidth} text={parentCategory.title} rightIcon="double-caret-vertical" />
        </Select>
        <span>:</span>
        <Select items={parentCategory.children || []} 
                itemRenderer={(x)=><MenuItem key={x.categoryId} text={x.title} onClick={()=>internalCategorySelected(x)}/>}
        >
            <Button className={styles.fullWidth} text={childCategory.title} rightIcon="double-caret-vertical" />
        </Select>
        </>
    )

}

var mapStateToProps = state => ({
    categories: state.Category.HierarchyCategories?.sort((a,b)=> a.position - b.position) || []    
});

var mapDispatchToProps = dispatch => ({
    retrieveCategories: () => dispatch({ type: actionTypes.RETRIEVE_HIERARCHY_CATEGORIES })    
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CategorySelector);