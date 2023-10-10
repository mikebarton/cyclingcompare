import styles from './membership.module.css';
import { connect } from 'react-redux';
import * as actionTypes from './actionTypes';
import { AnchorButton} from '@blueprintjs/core';

function SignOut(props){
    return (
        <div className={styles.container}>
            <AnchorButton minimal text='Sign Out' onClick={()=> props.onSignOutClick()}/>
        </div>
    );
}

var mapStateToProps = state => ({
    
});

var mapDispatchToProps = dispatch => ({
    onSignOutClick: ()=> dispatch({type: actionTypes.SIGN_OUT})
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SignOut);