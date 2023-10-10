import { useState, useEffect} from 'react' 
import * as styles from './productMappings.module.scss'
import StandardLayout from '../../components/shared/layout/StandardLayout'
import Menu from '../../components/shared/menu'
import Header from '../../components/shared/header'

const ProductMapping = function(){


    return (
        <StandardLayout top={<Header />} left={<Menu />}>
            <div className={styles.productContainer}>
                
            </div>
    </StandardLayout>
    )
}

export default ProductMapping