import Link from 'next/link'
import OutsideAlerter from '../shared/OutsideAlerter'
import CategoryVertMenuItem from './categoryVertMenuItem'
import * as styles from './categoryVertMenuPane.module.scss'

export default function categoryVertMenuPane({categories, onCloseRequest}){

    return (
            <OutsideAlerter onClickedOutside={onCloseRequest}>
                <div className={styles.vertMenu}>
                    { categories.map(x=>{
                        return (
                            <CategoryVertMenuItem category={x}/>
                        )
                    })}
                </div>
            </OutsideAlerter>
    )
}