import {useState} from 'react';
import * as styles from './categories.module.scss';
import Validate from "react-validate-form";
import { connect } from 'react-redux';
import { ControlGroup, InputGroup, Label, Button} from '@blueprintjs/core'
import * as actionTypes from './actionTypes';

const validationMap = {
    title: ["required"],
    urlSlug: ["required"]
}

const CreateCategoryForm = function(props){
    const [title, setTitle] = useState();
    const [description, setDescription] = useState();
    const [urlSlug, setUrlSlug] = useState();
    const [bannerImageUrl, setBannerImageUrl] = useState();

    const submitForm = function(e){
        e.preventDefault();
        props.onSubmitClick(title, description, urlSlug, bannerImageUrl, props.parentId);    
        onCancelClick();    
        return false;
    }

    const onCancelClick = function(){
        setTitle(null);
        setDescription(null);
        setUrlSlug(null);
        setBannerImageUrl(null);
        props.closeAction();
    }

    return (
        <Validate validations={validationMap}>
            {
                ({validate, errorMessages}) => (
                    <div className={styles.formContainer}>                        
                        <form onSubmit={submitForm}>
                            <ControlGroup vertical={true} fill={false}>
                                <Label>Parent Category: {props.parentName || 'None'}</Label>
                                <InputGroup name='title' placeholder='title' intent='primary' onChange={({target})=>setTitle(target.value)} onBlur={validate} large={true}/>
                                <Label  intent={"warning"}>{errorMessages.title}</Label>
                                <InputGroup name='description' placeholder='description' intent='primary' onChange={({target})=>setDescription(target.value)} onBlur={validate} large='true'/>
                                <Label  intent={"warning"}>{errorMessages.description}</Label>
                                <InputGroup name='urlSlug' placeholder='urlSlug' intent='primary' onChange={({target})=>setUrlSlug(target.value)} onBlur={validate} large='true'/>
                                <Label  intent={"warning"}></Label>
                                <InputGroup name='bannerImageUrl' placeholder='bannerImageUrl' intent='primary' onChange={({target})=>setBannerImageUrl(target.value)} onBlur={validate} large='true'/>
                                <Label  intent={"warning"}></Label>
                                <Button type='submit'>Create</Button>    
                                <Button onClick={()=> onCancelClick()}>Cancel</Button>                 
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
    onSubmitClick: (title, description, urlSlug, bannerImageUrl, parentId ) => dispatch({ type: actionTypes.CREATE_CATEGORY, data: { title: title, description: description, parentId: parentId, urlSlug: urlSlug, categoryBannerImage: bannerImageUrl}})
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CreateCategoryForm);