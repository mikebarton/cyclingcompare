import * as styles from './widePageLayout.module.scss'

export default function WidePageLayout({ children, showPage }) {
    
    return (
        <div className={styles.bodyContainer}>
            <div className={`${styles.bodyPage} ${showPage ? styles.showBackground : ''}`}>
                {children}                    
            </div>
        </div>
    )
}

