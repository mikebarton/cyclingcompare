import { useState, useEffect} from 'react' 
import * as styles from './filterConfig.module.scss'
import StandardLayout from '../../components/shared/layout/StandardLayout'
import Menu from '../../components/shared/menu'
import Header from '../../components/shared/header'
import CategorySelector from '../../components/categories/CategorySelector'
import FilterTypeSelector from '../../components/filters/FilterTypeSelector';
import FilterList from '../../components/filters/FilterList';
import UnmappedFilterTranslations from '../../components/filters/UnmappedFilterTranslations';
import EditFilterView from '../../components/filters/EditFilterView';



const FilterConfig = function(){
    const [selectedCategory, setSelectedCategory] = useState();
    const [selectedFilterType, setSelectedFilterType] = useState();
    const [selectedFilter, setSelectedFilter] = useState();

    useEffect(()=>{
        setSelectedFilter();
    }, [selectedCategory])

    useEffect(()=>{
        setSelectedFilter();
    },[selectedFilterType])

    return (
        <StandardLayout top={<Header />} left={<Menu />}>
            <div className={styles.container}>
                <div className={styles.pane}>
                    <div className={styles.toolbar}>
                        <CategorySelector onCategorySelected={c=> setSelectedCategory(c)}/>
                        <FilterTypeSelector categoryId={selectedCategory?.categoryId} onSelectionChanged={f=> setSelectedFilterType(f)}/>
                    </div>
                    <FilterList filterGroupId={selectedFilterType?.categoryFilterGroupId} categoryId={selectedCategory?.categoryId} onSelectionChanged={f=> setSelectedFilter(f)}/>
                </div>
                <div className={styles.pane}>
                    <EditFilterView filter={selectedFilter} categoryId={selectedCategory?.categoryId}/>
                </div>
                <div className={`${styles.end} ${styles.pane}`}>
                    <UnmappedFilterTranslations categoryId={selectedCategory?.categoryId} filterGroupId={selectedFilterType?.categoryFilterGroupId} selectedFilter={selectedFilter}/>
                </div>
            </div>
        </StandardLayout>
    )
}

export default FilterConfig