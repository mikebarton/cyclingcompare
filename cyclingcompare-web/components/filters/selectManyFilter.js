import {useState, useEffect} from 'react'
import * as styles from './filters.module.scss'

const SelectManyFilter = function({filter, onFilterUpdated}){
    var [filterState, setFilterState] = useState(filter);
    var [isExpanded, setIsExpanded] = useState(false);
    var [isFiltered, setIsFiltered] = useState(false);

    useEffect(()=>{
        setFilterState(filter);
        if(Object.keys(filter.filterOptions).some(x=>filter.filterOptions[x]))
            setIsFiltered(true);
        else
            setIsFiltered(false);
    },[filter])

    function handleCheckBoxChange(filterKey, value){
        if(value)
            setIsFiltered(true);
        else if(Object.keys(filterState.filterOptions).some(x=>filterState.filterOptions[x]))
        {
            setIsFiltered(false);
        }

        var updatedFilterState = {...filterState};
        updatedFilterState.filterOptions[filterKey] = value; //=== 'on' ? true : false;
        setFilterState(updatedFilterState);
        onFilterUpdated(updatedFilterState);
    }

    function resetFilter(e){
        if(isFiltered){
            setIsFiltered(false);
            var updatedFilterState = {...filterState, filterOptions: {} };
            Object.keys(filterState.filterOptions).forEach(x=> updatedFilterState.filterOptions[x] = false)
            setFilterState(updatedFilterState);
            onFilterUpdated(updatedFilterState);
            e.stopPropagation()
        }
    }

    return (
        <div className={styles.filterContainer}>
            <div className={styles.filterTitle} onClick={()=> setIsExpanded(!isExpanded)}>{filter.name}{ isFiltered && <img src='/icons/x.svg' onClick={(e)=>resetFilter(e)}/>}</div>
            <div className={`${styles.checkContainer} ${isExpanded ? styles.expandedChecks : styles.restrictedChecks}`}>
                {
                    Object.keys(filter.filterOptions).map(x=>{
                        return (
                            <label key={filter.name + x}>{x}<input type='checkbox' checked={filter.filterOptions[x]} onChange={e=> handleCheckBoxChange(x, e.target.checked)}/></label>
                        )
                    })
                }
            </div>
            <div className={styles.listChange}><a onClick={()=> setIsExpanded(!isExpanded)}>{ isExpanded ? 'Shrink List' : 'Expand List' }</a></div>
        </div>
    )
}

export default SelectManyFilter;