import React from 'react'
import { connect } from 'react-redux';
import * as styles from './StandardLayout.module.scss';

const StandardLayout = function(props){

    return (
        <div className={styles.container}>
            <div className={styles.header}>
                {props.top}
            </div>
            <div className={styles.main_container}>
                <div className={styles.menu}>
                    {props.left}
                </div>
                <div className={styles.main}>
                    {props.main || props.children}
                </div>
            </div>
        </div>
    )
}

var mapStateToProps = state => ({
    
});

var mapDispatchToProps = dispatch => ({
    
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(StandardLayout);