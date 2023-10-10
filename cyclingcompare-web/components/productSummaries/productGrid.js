import * as styles from './productGrid.module.scss'
import ProductItem from './productItem'

export default function ProductGrid({category, products, isFiltered, onClearFiltersRequested}){
    
    return (
        <div className={styles.gridContainer}>
            <div className={styles.clearContainer}>
                {isFiltered && <div className={styles.clearButton} onClick={()=> onClearFiltersRequested()}>clear filters</div>}
            </div>
            <div className={styles.productContainer}>
                { products?.map((x, i)=> <div className={styles.gridItem} key={x.productId + i}><ProductItem key={x.productId + i} item={x}/></div>)}
            </div>
        </div>
    )
}