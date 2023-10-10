import { useState } from 'react'
import Link from 'next/link'
import * as styles from './categoryVertMenuItem.module.scss'

export default function CategoryVertMenuItem({category}){
    var [isOpen, setIsOpen] = useState(false)

    const onToggleItem = function(){
        setIsOpen(!isOpen)
    }

    return (
        <div key={category.categoryId} className={styles.vertMenuItem} >
            <div className={`${styles.vertMenuItemHeader} ${isOpen ? styles.selectedMenuHeader : ''}`}>
            <Link href={'/' + category.urlSlug }><div>{category.title}</div></Link>
                <div onClick={onToggleItem} className={styles.chevron}>
                    {isOpen ? <img src='/icons/chevron-down.svg'/> :<img src='/icons/chevron-right.svg'/> }
                </div>
            </div>
            
            {isOpen && <div className={styles.subCatContainer}>
                { category.children.map(x=> <SubCategoryMenuItem category={category} subCategory={x}/>) }
            </div>}
        </div>
    )
}

const SubCategoryMenuItem = function({category, subCategory}){

    return (
        <Link href={'/' + category.urlSlug + '/' + subCategory.urlSlug}>
        <div className={styles.subCategory}>
            <span>{subCategory.title}</span>
        </div>
        </Link>
    )
}