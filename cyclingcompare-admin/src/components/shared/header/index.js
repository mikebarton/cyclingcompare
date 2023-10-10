import React from 'react';
import * as styles from './header.module.scss';
import SignOutLink from '../../membership/signoutLink'

const Header = function(props){

    return (
        <div className={styles.headerContainer}>
            <div className={styles.horiz_container}>
                <img className={styles.logo} src={process.env.PUBLIC_URL + '/content/logo.png'} />
                <div className={styles.signout}>
                    <SignOutLink/>
                </div>
            </div>
            <div className={styles.bar}/>
        </div>
    )
}

export default Header;