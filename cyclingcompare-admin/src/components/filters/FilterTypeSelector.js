import { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { Select } from "@blueprintjs/select";
import { Menu, MenuItem, Button, InputGroup, EditableText} from '@blueprintjs/core';
import * as actionTypes from './actionTypes'




function FilterTypeSelector({onSelectionChanged, categoryId, filterGroups, retrieveFilterGroups, addOrUpdateFilterGroup, deleteFilterGroup, relocateFilter}){
    const [selectedFilterType, setSelectedFilterType] = useState({});
    const [newGroupName, setNewGroupName] = useState();


    useEffect(()=>{
        if(categoryId)
            retrieveFilterGroups(categoryId)
    },[categoryId])

    useEffect(()=>{
        if(selectedFilterType){
            onSelectionChanged(selectedFilterType)
        }
    },[selectedFilterType])

    useEffect(()=>{
        if(filterGroups)
            setSelectedFilterType(filterGroups[0]);
    },[filterGroups])

    function createFilterGroup(){
        if(newGroupName && categoryId){
            var newGroup = {
                name: newGroupName,
                categoryId: categoryId,
                order: 0,
                filterType: 1,
                minLabel: null,
                maxLabel: null,
                filterCode: null
            }
            addOrUpdateFilterGroup(newGroup)
            setNewGroupName();
        }
    }
    
    function itemListRenderer({filteredItems, renderItem}){
        
        return (
            <Menu>
                <MenuItem text={'Create a new Filter Group'}>
                    <InputGroup value={newGroupName} onChange={e=> setNewGroupName(e.target.value)} Enter a Group Name/>
                    <Button text={'Create'} onClick={()=>createFilterGroup()}/>
                </MenuItem>
                {
                    filteredItems.map((x,index)=> renderItem(x, index))
                }
            </Menu>
        )
    }

    function moveUp(item){
        if(!item)
            return;

        item.order = Math.max(0, item.order - 1);
        addOrUpdateFilterGroup({...item});
    }

    function moveDown(item){
        if(!item)
            return;

        item.order = item.order + 1;
        addOrUpdateFilterGroup({...item});
    }

    function FilterGroupEditor({filterGroup}){
        const [isGroupEditting, setIsGroupEditting] = useState(false);
        const [groupName, setGroupName] = useState(filterGroup.name);

        function handleConfirm(){
            addOrUpdateFilterGroup({...filterGroup, name: groupName})
        }

        return (
            <>
                <Button text={isGroupEditting ? 'End Edit' : 'Edit'} onClick={()=> setIsGroupEditting(!isGroupEditting)}/>
                {
                    isGroupEditting && <EditableText isEditing={isGroupEditting} 
                                                    value={groupName} 
                                                    onChange={(x)=>setGroupName(x)}
                                                    onConfirm={()=>handleConfirm()}/>
                }
            </>
        )
    }

    function itemRenderer(x){
        return (            
            <MenuItem key={x.categoryFilterGroupId} text={x.name} onClick={()=>setSelectedFilterType(x)}>
                <Button text='Move Up' onClick={()=> moveUp(x)}/>
                <Button text='Move Down' onClick={()=> moveDown(x)}/>
                <Button text='Delete' onClick={()=> deleteFilterGroup(x.categoryFilterGroupId, categoryId)}/>
                <FilterGroupEditor filterGroup={x}/>                
            </MenuItem>
        )
    }

    return (
        <Select items={filterGroups} itemListRenderer={itemListRenderer} itemRenderer={itemRenderer}>
            <Button text={selectedFilterType?.name || 'Loading...'} rightIcon="double-caret-vertical" />
        </Select>
    )
}

var mapStateToProps = (state, {categoryId}) => ({
    filterGroups: state.Filters.FilterGroups[categoryId] || []
});

var mapDispatchToProps = dispatch => ({
    retrieveFilterGroups: (categoryId) => dispatch({ type: actionTypes.GET_FILTER_GROUPS_BY_CATEGORY, categoryId: categoryId }),
    addOrUpdateFilterGroup: (filterGroup) => dispatch({ type: actionTypes.ADD_OR_UPDATE_FILTER_GROUP, filterGroup: filterGroup}),
    deleteFilterGroup: (filterGroupId, categoryId) => dispatch({ type: actionTypes.DELETE_FILTER_GROUP, filterGroupId: filterGroupId, categoryId: categoryId}) ,
    relocateFilter: (filterId, targetGroupId) => dispatch({ type: actionTypes.RELOCATE_FILTER, filterId: filterId, targetGroupId: targetGroupId})
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(FilterTypeSelector);
