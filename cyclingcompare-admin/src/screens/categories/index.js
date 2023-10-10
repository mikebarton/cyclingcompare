import React, { useState } from 'react';
import * as styles from './categories.module.css';
import StandardLayout from '../../components/shared/layout/StandardLayout';
import Menu from '../../components/shared/menu';
import Header from '../../components/shared/header';
import { Tab, Tabs } from "@blueprintjs/core";
import CategoryTable from '../../components/categories/CategoryTable'
import CategoryHierarchy from '../../components/categories/CategoryHierarchy';

const CategoryTablePanel = function(){
    return (
        <div className={styles.tableContainer}>                                    
            <CategoryTable/>
        </div>
    )
}

const CategoryHeirarchyPanel = function(){
    return (
        <div className={styles.hierarchyContainer}>
            <CategoryHierarchy/>
        </div>
    )
}


const Categories = function(props){

    
    return (
        <StandardLayout 
            top={<Header/>}
            left={<Menu/>}>
                <Tabs className={styles.tabContainer}>
                    <Tab id="table" title="Category Data" panel={<CategoryTablePanel/>} />
                    <Tab id="hierarchy" title="Category Hierarchy" panel={<CategoryHeirarchyPanel />} />                        
                </Tabs>
        </StandardLayout>
    )
}
export default Categories;