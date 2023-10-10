import React from 'react';
import Login from '../../components/membership/Login'
import { Card, Elevation} from '@blueprintjs/core';
import styles from './membership.module.css';

function Membership(){
    return (
        <div className={styles.container}>
            <Card interactive={true} elevation={Elevation.TWO}>                
                <Login/>
            </Card>
        </div>
    );
}

export default Membership;