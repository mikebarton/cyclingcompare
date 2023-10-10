import { useState, useCallback  } from 'react'
import Link from 'next/link'
import CategoryMenuPane from './categoryMenuPane'
import CategoryVertMenuPane from './categoryVertMenuPane'
import OutsideAlerter from '../shared/OutsideAlerter'
import * as styles from './categoryMenu.module.scss'
import debounce from 'lodash.debounce';

export default function CategoryMenu({ categories }) {
    var [overMenu, setOverMenu] = useState(false)
    var [overPane, setOverPane] = useState(false)
    var [overVertMenu, setOverVertMenu] = useState(false)
    var [selectedCategory, setSelectedCategory] = useState(categories[0]||{})

    // const debouncedSave = useCallback(
	// 	debounce(cat => {
    //         if(cat === possibleNextCategory)
    //             setSelectedCategory(cat)
    //     }, 500),
	// 	[], // will be created only once initially
	// );

    const debounceMenuOpenState = useCallback(
        debounce(openState=>{
            setOverMenu(openState)
    }, 500),
    []);

    const debounceOverMenuPane = useCallback(
        debounce(openState=>{
            setOverPane(openState)
    }, 500),
    []);
    
    var timer = null;
    const changeSelectedCategory = (cat) => {        
        if(timer)
            clearTimeout(timer)

        timer = setTimeout(() => {
            setSelectedCategory(cat);            
        }, 500);
    }


    
    return (
        <div className={styles.menuContainer}>
            { (overMenu || overPane) && <div className={styles.menuBackground}/>}
            

            <div className={styles.menuHeader}>
                <div className={styles.burger}><img src='/icons/menu.svg' onClick={()=> setOverVertMenu(true)}/></div>
                <div className={styles.menu} onMouseOver={()=> debounceMenuOpenState(true)} onMouseOut={()=>debounceMenuOpenState(false)}>
                    <Link href={'/'}>
                        <img src='/icons/home.svg'/>
                    </Link>
                    {
                        categories.map(x => {
                            return (<div key={x.categoryId} className={styles.menuItem} onMouseOver={()=>changeSelectedCategory(x)} onMouseOut={()=> {if(timer)clearTimeout(timer)}}>
                                <Link href={'/' + x.urlSlug}>{x.title}</Link>
                            </div>)
                        })
                    }
                </div>

            </div>
            {(overMenu || overPane) && <div className={styles.paneContainer} onMouseOver={()=> setOverPane(true)} onMouseLeave={()=>setOverPane(false)}>
                <CategoryMenuPane category={selectedCategory} />
            </div>}
            { overVertMenu && 
                <div className={styles.vertMenuContainer}>
                    <CategoryVertMenuPane onCloseRequest={()=> setOverVertMenu(false)} categories={categories}/>
                </div>
            }
        </div>
    )
}