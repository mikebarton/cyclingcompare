import React, { useState } from 'react';

import * as styles from './home.module.css';
import StandardLayout from '../../components/shared/layout/StandardLayout';
import Menu from '../../components/shared/menu';
import Header from '../../components/shared/header';
import { Tab, Tabs } from "@blueprintjs/core";



const Home = function(props){

    
    return (
        <StandardLayout 
            top={<Header/>}
            left={<Menu/>}>

                <div className={styles.container}>                                    
                Wooby woo!
                </div>
        </StandardLayout>
    )
}
export default Home;