import { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import * as styles from './filters.module.scss'
import * as actionTypes from './actionTypes';
import { EditableText, Icon, IconSize, Button, MenuItem } from '@blueprintjs/core';
import { Select } from "@blueprintjs/select";

function EditFilterView({filter, categoryId, translations, retrieveTranslations, filterGroups, removeTranslation, relocateFilter, mapTranslation}){
    const [selectedFilterGroup, setSelectedFilterGroup] = useState({name: 'Choose One...'})
    const [filterNameEditting, setFilterNameEditting] = useState();
    const [indexBeingEditted, setIndexBeingEditted] = useState();
    useEffect(()=>{
        if(filter){
            retrieveTranslations(filter.categoryFilterId);
        }
    },[filter])

    function itemRenderer(x){
        return (            
            <MenuItem key={x.categoryFilterGroupId} text={x.name} >
                <Button text='Move It!' onClick={()=>relocateFilter(filter.categoryFilterId, x.categoryFilterGroupId, categoryId)}/>
            </MenuItem>
        )
    }

    function onEditItem(name, index){
        setIndexBeingEditted(index);
        setFilterNameEditting(name);
    }

    function onFilterChanged(updatedValue, translation){
        setIndexBeingEditted();
        setFilterNameEditting()
        if(!updatedValue)
            return;

            var translationToSend = {
                ...translation || {},
                name: updatedValue,
                categoryFilterId: filter.categoryFilterId
            }

        mapTranslation(translationToSend, categoryId, filter.categoryFilterGroupId)
    }

    if(!filter)
        return <></>

    return (
        <div className={styles.translationContainer}>
            <h3>Filter:&nbsp;{filter.name}</h3>
            <div className={styles.toolbar}>
                <h5>Move Filter to Group:</h5>
                <Select items={filterGroups} itemRenderer={itemRenderer}>
                    <Button text={selectedFilterGroup?.name || 'Loading...'} rightIcon="double-caret-vertical" />
                </Select>
            </div>
            <div className={styles.filterList}>
                { translations.map((x,i)=>{
                   return <div className={styles.filter}>
                            
                            <EditableText value={i === indexBeingEditted ? filterNameEditting : x.name } 
                                        onChange={e=>setFilterNameEditting(e)} 
                                        onEdit={()=> onEditItem(x.name,i)} 
                                        onConfirm={(e)=>onFilterChanged(e, x)}/>
                            <div className={styles.filterButtons}>
                                <Icon
                                    icon={'delete'}
                                    htmlTitle={'Delete'} 
                                    iconSize={12}
                                    onClick={() => removeTranslation(filter.categoryId, filter.categoryFilterId, filter.filterTypeCode, x.categoryFilterTranslationId)}
                                />
                            </div>
                        </div> 
                })}
                <div className={styles.filter}>
                    <EditableText value={indexBeingEditted == -1 ? filterNameEditting : ''}
                                onChange={e=>setFilterNameEditting(e)} 
                                onEdit={()=> onEditItem('',-1)} 
                                onConfirm={(e)=>onFilterChanged(e)}/>
                </div>
            </div>
        </div>
    )
}

var mapStateToProps = (state, {filter, categoryId}) => ({
    translations: state.Filters.TranslationsByFilter[filter?.categoryFilterId ] || [],
    filterGroups: state.Filters.FilterGroups[categoryId] || []
});

var mapDispatchToProps = dispatch => ({
    retrieveTranslations: (categoryFilterId) => dispatch({ type: actionTypes.GET_TRANSLATIONS_BY_ID, filterId: categoryFilterId }),
    mapTranslation: (translation, categoryId, filterGroupId) => dispatch({ type: actionTypes.ADD_OR_UPDATE_TRANSLATION, translation: translation, categoryId: categoryId, filterGroupId: filterGroupId }),
    removeTranslation: (categoryId, filterId, filterType, translationId) => dispatch({ type: actionTypes.DELETE_TRANSLATION_BY_ID, translationId: translationId, filterId: filterId, categoryId: categoryId, filterType: filterType}), 
    relocateFilter: (filterId, targetGroupId, categoryId) => dispatch({ type: actionTypes.RELOCATE_FILTER, filterId: filterId, targetGroupId: targetGroupId, categoryId: categoryId})
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EditFilterView);