import { useState } from 'react';
import * as styles from './categoryMapping.module.scss';
import StandardLayout from '../../components/shared/layout/StandardLayout';
import Menu from '../../components/shared/menu';
import Header from '../../components/shared/header';
import CategoryMappingTable from '../../components/categories/CategoryMappingTable';
import MerchantSelector from '../../components/merchants/MerchantSelector';
import { Button, Checkbox } from '@blueprintjs/core';

const CategoryMapping = function(){
    var [selectedMerchant, setSelectedMerchant] = useState();
    var [showAllMappings, setShowAllMappings] = useState(true)

    return (
        <StandardLayout 
            top={<Header/>}
            left={<Menu/>}>
                <div className={styles.container}>
                    <div className={styles.tableHeader}>
                        <div className={styles.headerControl}>
                            <MerchantSelector onMerchantChanged={m => setSelectedMerchant(m)}/>
                        </div>
                        <div className={styles.headerControl}>                            
                            <Checkbox label = 'Show All Category Mappings' checked={showAllMappings} onChange={(e)=>setShowAllMappings(e.target.checked)}/>
                        </div>
                    </div>
                    <div className={styles.tableContainer}>
                        <CategoryMappingTable merchant={selectedMerchant} showAllMappings={showAllMappings}/>
                    </div>
                </div>
        </StandardLayout>
    )
}

export default CategoryMapping;