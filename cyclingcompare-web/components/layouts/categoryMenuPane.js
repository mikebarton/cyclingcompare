import Link from 'next/link'
import * as styles from './categoryMenuPane.module.scss'

export default function CategoryMenuPane({category}){

    return (
        <div className={styles.menuContainer}>
            <div className={styles.menuLeft}>
                {category.children.map(x=>{
                    return <div className={styles.subMenuItem} key={x.categoryId}><Link href={'/' + category.urlSlug + '/' + x.urlSlug}>{x.title}</Link></div>
                })}
            </div>
            <div className={styles.menuRight}>

            </div>
        </div>
    )
}