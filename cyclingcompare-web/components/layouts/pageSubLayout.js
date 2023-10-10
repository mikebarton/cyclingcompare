import { useRouter } from 'next/router'
import * as styles from './pageSubLayout.module.scss'
import Link from 'next/link'

export default function PageSubLayout({ children, categories, leftPane, hideLeftPane }) {
    const router = useRouter()
    const { category, subCategory } = router.query

    const CategoryBreadCrumbs = function(props){        
        var categoryItem = categories.find(x => x.urlSlug === category)
        if(!categoryItem)
            return <span></span>

        var subCatItem = categoryItem.children.find(x => x.urlSlug === subCategory)
        return <><span className={styles.link}><Link href='/'>Home</Link></span>{ ' > '}<Link href={'/' +categoryItem.urlSlug}>{categoryItem.title}</Link> {subCatItem && ('> ' + subCatItem.title)}</>
    }

    
    return (
        <div className={styles.bodyContainer}>
            <div className={styles.bodyPage}>
                <div className={styles.blackSection} />
                <div className={styles.blueSection}>
                    <CategoryBreadCrumbs/>
                </div>
                <div className={styles.pageSplit}>
                    { !hideLeftPane && <div className={styles.filterContainer}>
                        <div className={styles.filterColumn}>
                            {leftPane}
                        </div>
                    </div> }
                    <div className={styles.mainColumn}>
                        {children}
                    </div>
                </div>
            </div>
        </div>
    )
}

