import { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import * as styles from './filters.module.scss'
import * as actionTypes from './actionTypes';
import { EditableText, Icon, IconSize, RadioGroup, Radio } from '@blueprintjs/core';


function FilterList({filters, filterGroupId, categoryId, retrieveFilters, addOrUpdateFilter, deleteFilter, onSelectionChanged}){
    const [filterNameEditting, setFilterNameEditting] = useState();
    const [orderEditting, setOrderEditting] = useState();
    const [indexBeingEditted, setIndexBeingEditted] = useState();
    const [selectedFilter, setSelectedFilter] = useState();

    useEffect(()=>{
        if(categoryId && (filterGroupId !== null && filterGroupId !== undefined)){
            retrieveFilters(categoryId, filterGroupId);
        }
    }, [categoryId, filterGroupId])

    function onFilterChanged(updatedValue, filter){
        setIndexBeingEditted();
        setFilterNameEditting()
        if(!updatedValue)
            return;

        var filterToSave = {
            ...(filter || {}),
            categoryId: categoryId,
            name: updatedValue,
            categoryFilterGroupId: filterGroupId
        }
        addOrUpdateFilter(filterToSave);
    }

    function onOrderChanged(updatedValue, filter){
        setIndexBeingEditted();
        setOrderEditting()
        if(!updatedValue || isNaN(updatedValue))
            return;

            var filterToSave = {
                ...(filter || {}),
                order: parseInt(updatedValue)
            }
            addOrUpdateFilter(filterToSave);
    }    
    
    function moveUp(filter){
        if(!filter)
            return;

        filter.order = Math.max(0, filter.order - 1);
        addOrUpdateFilter({...filter});
    }

    function moveDown(filter){
        if(!filter)
            return;

        filter.order = filter.order + 1;
        addOrUpdateFilter({...filter});
    }

    function selectedFilterChanged(e, filter){
        setSelectedFilter(filter);
        onSelectionChanged(filter);

    }

    if(!categoryId || filterGroupId === null || filterGroupId === undefined)
        return <></>

    function onEditItem(item, itemIndex){
        setIndexBeingEditted(itemIndex);
        setFilterNameEditting(item);
    }

    function onEditOrder(newVal, index){
        setOrderEditting(newVal);
        setIndexBeingEditted(index);
    }

    return (
        <>
        <p>Filters are the values that will be displayed on the website. When a user searches with a filter, we will search for all products that match any of the translations that are assigned to that filter</p>
            <div className={styles.filterList}>
                { filters.map((x, i)=>{
                    return (
                        <div className={`${styles.filter} ${x===selectedFilter ? styles.selectedFilter : ''}`}>
                            <EditableText value={i === indexBeingEditted ? filterNameEditting : x.name} 
                                        onChange={e=>setFilterNameEditting(e)} 
                                        onEdit={()=> onEditItem(x.name,i)} 
                                        onConfirm={(e)=>onFilterChanged(e, x)}/>
                            <div className={styles.filterButtons}>
                                <Icon
                                    icon={'arrow-up'}
                                    htmlTitle={'Move up'} 
                                    iconSize={12}
                                    onClick={() => moveUp(x)}
                                />
                                <Icon
                                    icon={'arrow-down'}
                                    htmlTitle={'Move down'} 
                                    iconSize={12}
                                    onClick={() => moveDown(x)}
                                />
                                <Icon
                                    icon={'delete'}
                                    htmlTitle={'Delete'} 
                                    iconSize={12}
                                    onClick={() => deleteFilter(x.categoryFilterId, x.categoryId, x.categoryFilterGroupId)}
                                />
                                <Radio className={styles.radio} onChange={e=> selectedFilterChanged(e, x)} checked={x=== selectedFilter}/>
                            </div>
                            <EditableText value={i === indexBeingEditted ? orderEditting : x.order} 
                                        onChange={e=>setOrderEditting(e)} 
                                        onEdit={()=> onEditOrder(x.order, i)} 
                                        onConfirm={(e)=>onOrderChanged(e, x)}/>
                        </div>
                    )
                })}
                <div className={styles.filter}>
                    <EditableText value={indexBeingEditted == -1 ? filterNameEditting : ''}
                                onChange={e=>setFilterNameEditting(e)} 
                                onEdit={()=> onEditItem('',-1)} 
                                onConfirm={(e)=>onFilterChanged(e)}/>
                </div>
            </div>
            </>
    )
}

var mapStateToProps = (state, {filterGroupId}) => ({
    filters: state.Filters.FilterList[filterGroupId] || []
});

var mapDispatchToProps = dispatch => ({
    retrieveFilters: (categoryId, filterGroupId) => dispatch({ type: actionTypes.GET_FILTERS_BY_CATEGORY, categoryId: categoryId, filterGroupId: filterGroupId }),
    addOrUpdateFilter: (filter) => dispatch({ type: actionTypes.ADD_OR_UPDATE_FILTER, filter: filter}),
    deleteFilter: (filterId, categoryId, filterGroupId) => dispatch({ type: actionTypes.DELETE_FILTER, filterId: filterId, categoryId: categoryId, filterGroupId: filterGroupId})
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(FilterList);