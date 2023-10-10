import Head from 'next/head'
import Link from 'next/link'
import { useRouter } from 'next/router'
import { useState } from 'react'
import CategoryMenu from './categoryMenu'
import * as styles from './layout.module.scss'

export default function Layout({categories, children, footer}) {
    var [searchText, setSearchText] = useState();
    const router = useRouter();

    function onSearchKey(e){
        if (e.key === 'Enter') {
            router.push({ pathname: searchText ? '/search/' + searchText : '/' })
          }
    }

    return (
        <div className={styles.container}>
            <Head>
                <title>Cycling Compare</title>
                <link rel="icon" href="/favicon.png" />
            </Head>

            <div className={styles.headerContainer}>
                <div className={styles.header}>
                    <Link href="/">
                        <img className={styles.headerLogo} src='/logos/site/original.svg' />
                    </Link>

                    

                    <div className={styles.headerRight}>
                        <div className={styles.mission}>
                            <h2>Australia's home of cycling deals</h2>
                        </div>

                        <div className={styles.headerFarRight}>
                            <div className={styles.linkBar}>
                                <Link href="/">Top Searches</Link>{' | '}
                                <Link href="/help/privacy">Privacy and Cookies</Link>{' | '}
                                <Link href="/help/terms">Terms and Conditions</Link>{' | '}
                                <Link href="/help/faq">FAQ</Link>{' | '}
                                <Link href="/help/about">About Us</Link>
                            </div>

                            <div className={styles.searchRegion}>
                                <div className={styles.searchInput}>
                                    <div className={styles.searchText}><input type='text' value={searchText} onKeyPress={onSearchKey} onChange={e=> setSearchText(e.target.value)}/></div>
                                    <div onClick={()=> router.push({ pathname: searchText ? '/search/' + searchText : '/' }) } className={styles.searchButton}>
                                        {/* <Link href={{ pathname: '/search', query: { query: searchText }}} prefetch={false} as={{ pathname: '/search', query: { query: searchText }}}> */}
                                            <img src="/icons/search40x40.svg"/>
                                        {/* </Link> */}
                                    </div>
                                </div>                            
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <CategoryMenu  categories={categories}/>    
            { children }
            <div className={styles.footerContainer}>
                <div className={styles.footerPage}>
                    <div className={styles.footerRow}>
                        <span><Link href="/help/privacy"> Privacy and Cookies</Link></span>
                        <span><Link href="/help/terms"> Terms and Conditions</Link></span>
                        <span><Link href="/help/faq"> FAQ</Link></span>
                        <span><Link href="/help/about"> About Us</Link></span>
                    </div>
                    <div className={styles.footerRow}>
                        <h2>Australia's home of cycling deals</h2>
                    </div>
                </div>
            </div>
        </div>
    )
}