import React from 'react';
import { NavLink } from 'react-router-dom';
import * as styles from './menu.module.scss';

const Menu = function(props){

    return (
        <div className={styles.menu_container}>
            <span><NavLink to='/' activeClassName={styles.active}>Home</NavLink></span>
            <span><NavLink to='/categories' activeClassName={styles.active}>Categories</NavLink></span>
            <span><NavLink to='/category-mapping' activeClassName={styles.active} >Category Mapping</NavLink></span>
            <span><NavLink to='/global-products' activeClassName={styles.active} >Products</NavLink></span>
            <span><NavLink to='/product-mapping' activeClassName={styles.active} >Product Mapping (Under Construction)</NavLink></span>
            <span><NavLink to='/filter-config' activeClassName={styles.active} >Filters</NavLink></span>
            <span><NavLink to='/merchants' activeClassName={styles.active} >Merchants</NavLink></span>
        </div>
    )
}

export default Menu;