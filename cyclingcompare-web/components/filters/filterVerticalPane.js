import { useState, useEffect } from 'react'
import * as styles from './filterVerticalPane.module.scss'
import MinMaxFilter from './minMaxFilter';
import SelectManyFilter from './selectManyFilter';
import BannerDisplay from  '../adbanners/BannerDisplay';

export default function FilterVerticalPane({filters, onFiltersUpdated}){
    var [filtersToUse, setFiltersToUse] = useState(filters);

    useEffect(()=>{
        setFiltersToUse(filters);
    }, [filters])

    function handleFilterUpdated(filter){
        var updatedFilters = [...filtersToUse];
        var filterIndex = updatedFilters.findIndex(x=>x.filterId === filter.filterId && x.name === filter.name);
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
        <div className={styles.paneContainer}>
            {
                filtersToUse.map((x,i)=> getFilterComponent(x, i))
            }
            <div className={styles.bannerContainer}>
                <BannerDisplay width={160} height={600}/>
            </div>
        </div>
    )
}