import React, { useState } from 'react';
import { connect } from 'react-redux';
import Validate from "react-validate-form"
import { Button, ControlGroup, InputGroup, Divider, Spinner,Label } from '@blueprintjs/core';
import * as actionTypes from './actionTypes';
import { history } from '../../helpers/configureStore';


const validationMap = {
    email: ["required", "email"],
    password: ["required"]
}

function Login(props){
    var [email, setEmail] = useState('');
    var [password, setPassword] = useState('');
    
    const submitForm = function(e){
        e.preventDefault();
        props.onLoginClick(email, password);        
        return false;
    }

    return (
        <Validate validations={validationMap}>
            {
                ({validate, errorMessages}) => (
                    <div>                        
                        <form onSubmit={submitForm}>
                            <ControlGroup vertical={true} fill={false}>
                                <InputGroup name='email' placeholder='email' intent='primary' onChange={({target})=>setEmail(target.value)} onBlur={validate} large={true}/>
                                <Label  intent={"warning"}>{errorMessages.email}</Label>
                                <InputGroup name='password' placeholder='password' type='password' intent='primary' onChange={({target})=>setPassword(target.value)} onBlur={validate} large='true'/>
                                <Label  intent={"warning"}>{errorMessages.password}</Label>
                                <Button type='submit'>Login</Button>                            
                            </ControlGroup>
                        </form>
                    </div>
                )
            }
        </Validate>
    )
}

var mapStateToProps = state => ({
    
});

var mapDispatchToProps = dispatch => ({
    onLoginClick: (email, password) => dispatch({ type: actionTypes.LOGIN, email: email, password: password}),
    onSignOutClick: ()=> dispatch({type: actionTypes.SIGN_OUT})
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(Login);