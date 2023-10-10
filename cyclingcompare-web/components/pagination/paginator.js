import React from 'react'
import Link from 'next/link';
import * as styles from './paginator.module.scss'

const Paginator = function({totalProducts, pageActions, pageSize, pageNum}){    

    var pageEnd = Math.min(totalProducts || 0, ((pageNum * pageSize) + pageSize))       

    return (
            <div className={styles.container}>
                <div>Showing {(pageNum * pageSize) + 1} - {pageEnd} of {totalProducts || 0}</div>
                <div className={styles.buttonContainer}>
                    <div onClick={pageActions.prevPage}>prev</div>
                    <div onClick={pageActions.thirtyItems}>30</div>
                    <div onClick={pageActions.sixtyItems}>60</div>
                    <div onClick={pageActions.ninetyItems}>90</div>
                    <div onClick={pageActions.nextPage}>next</div>
                </div>
            </div>
    )
}

export default Paginator;