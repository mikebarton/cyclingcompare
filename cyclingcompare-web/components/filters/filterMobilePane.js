import { useState, useEffect, useContext } from 'react'
import * as styles from './filterMobilePane.module.scss'
import MinMaxFilter from './minMaxFilter';
import SelectManyFilter from './selectManyFilter';
import OutsideAlerter from '../shared/OutsideAlerter'

export default function FilterMobilePane({filters, onFiltersUpdated, onCloseRequest}){
    var [filtersToUse, setFiltersToUse] = useState(filters);

    useEffect(()=>{
        setFiltersToUse(filters);
    }, [filters])

    function handleFilterUpdated(filter){
        var updatedFilters = [...filtersToUse];
        var filterIndex = updatedFilters.findIndex(x=>x.filterId === filter.filterId);
        updatedFilters.splice(filterIndex, 1, filter);
        setFiltersToUse(updatedFilters)
        onFiltersUpdated(updatedFilters);
    }

    function getFilterComponent(filter, i){
        if(filter.filterType === 0)
            return <MinMaxFilter key={filter.name + i} filter={filter} onFilterUpdated={handleFilterUpdated}/>
        else if(filter.filterType === 1)
            return <SelectManyFilter filter={filter} key={filter.name + i} onFilterUpdated={handleFilterUpdated}/>
        
        return <></>
    }

    return (
        // <OutsideAlerter onClickedOutside={onCloseRequest}>            
            <div className={styles.paneContainer}>
                <div className={styles.closeButton} onClick={onCloseRequest}>close filters</div>
                {
                    filtersToUse.map((x,i)=> getFilterComponent(x, i))
                }
            </div>
        // </OutsideAlerter>
    )
}