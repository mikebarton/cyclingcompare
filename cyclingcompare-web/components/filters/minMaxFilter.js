import {useState} from 'react'
import * as styles from './filters.module.scss'

const MinMaxFilter = function({filter, onFilterUpdated}){
    var [minValue, setMinValue] = useState();
    var [maxValue, setMaxValue] = useState();
    var [isFiltered, setIsFiltered] = useState(false);

    function handleUpdateClicked(){
        if(minValue || maxValue)
            setIsFiltered(true);

        var updatedFilter = {...filter, minValue: minValue === '' ? null : minValue , maxValue: maxValue === '' ? null : maxValue};
        onFilterUpdated(updatedFilter);
    }

    function resetFilter(){
        if(isFiltered){
            setIsFiltered(false);
            setMinValue('');
            setMaxValue('');
            var updatedFilter = {...filter, minValue: null, maxValue: null};
            onFilterUpdated(updatedFilter);
        }
    }

    return (
        <div className={styles.filterContainer}>
            <div className={styles.filterTitle}>{filter.name}{ isFiltered && <img src='/icons/x.svg' onClick={()=>resetFilter()}/>}</div>
            <div className={styles.row}>
                <div className={styles.column}>
                    <label>{filter.minLabel}</label>
                    <div><input type='text' value={minValue} onChange={e=> setMinValue(e.target.value)} className={styles.rangeInput}/></div>
                </div>
                <div className={styles.column}>
                    <label>{filter.maxLabel}</label>
                    <div><input type='text' value={maxValue} onChange={e=> setMaxValue(e.target.value)} className={styles.rangeInput}/></div>
                </div>
            </div>
            <div className={styles.updateButton} onClick={handleUpdateClicked}>Update</div>
        </div>
    )
}

export default MinMaxFilter;