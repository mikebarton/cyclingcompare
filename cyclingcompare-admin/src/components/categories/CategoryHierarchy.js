import * as styles from './categories.module.scss'
import { useEffect, useState } from 'react'
import { connect } from 'react-redux';
import * as actionTypes from './actionTypes'
import { Menu, Item, Separator, Submenu, useContextMenu } from "react-contexify";
import "react-contexify/dist/ReactContexify.css";
import { show } from '@blueprintjs/core/lib/esm/components/context-menu/contextMenu';
import { Drawer, Classes } from '@blueprintjs/core';
import CreateCategoryForm from './CreateCategoryForm';
import { DRAWER } from '@blueprintjs/core/lib/esm/common/classes';
const CAT_MENU_ID = "hierarchy-cat-menu-id";
const SUBCAT_MENU_ID = "hierarchy-subcat-menu-id";

const CategoryHierarchy = function(props){
    const catMenu = useContextMenu({ id: CAT_MENU_ID });
    const subCatMenu = useContextMenu({ id: SUBCAT_MENU_ID });
    const [selectedTopTier, setSelectedTopTier] = useState({children:[]});
    const [secondTierCategories, setSecondTierCategories] = useState([]);
    const [selectedSecondTier, setSelectedSecondTier] = useState({});
    const [isDrawerOpen, setIsDrawerOpen] = useState(false);
    const [parentCatForCreation, setParentCatForCreation] = useState({});

    useEffect(()=>{
        props.retrieveCategories()
    },[])    

    useEffect(()=>{
        var cat= props.categories.find(x=>x.categoryId === selectedTopTier.categoryId);
        if(cat)setSelectedTopTier(cat);
    }, [props.categories])

    useEffect(()=>{
        var children = [...selectedTopTier.children];
        children.sort((a,b)=> a.position < b.position);
        setSecondTierCategories(children);
    }, [selectedTopTier])


    const onDeleteClick = function(category){
        if(category.parentId !== 0){
            category.children.forEach(x => {
                props.deleteCategory(x.categoryId);
            });
        }

        props.deleteCategory(category.categoryId)
    }

    const onMoveUpClick = function(category, list){
        var catIndex = list.indexOf(category);
        if(catIndex === 0) return;
        var previous = list[catIndex - 1];
        category.position = Math.max(category.position - 1, 0);
        previous.position = Math.min(previous.position + 1, list.length - 1);
        props.updateCategory(category);
        props.updateCategory(previous);
    }

    const onMoveDownClick = function(category, list){
        var catIndex = list.indexOf(category);
        if(catIndex === list.length - 1) return;
        var next = list[catIndex + 1];
        category.position = Math.min(category.position + 1, list.length - 1);
        next.position = Math.max(next.position - 1, 0);
        props.updateCategory(category);
        props.updateCategory(next);
    }

    const onCreateCategoryClick = function(setParent){
        if(setParent && selectedTopTier)
            setParentCatForCreation(selectedTopTier)
        else 
            setParentCatForCreation({})

        setIsDrawerOpen(true);    
    }

    return (
        <div className={styles.hierarchyContainer}>
            <div className={styles.tier}>
                <div className={styles.categoryPanel} onContextMenu={(e)=> catMenu.show(e)}>
                    { 
                        props.categories.map(x=> {
                                return <div onClick={()=> setSelectedTopTier(x)} className={`${styles.panelItem} ${x.categoryId===selectedTopTier.categoryId ? styles.selected : ''}`}>
                                    {x.title}
                                    {selectedTopTier.categoryId === x.categoryId && <div className={styles.buttonContainer}>
                                        <img onClick={()=> onMoveDownClick(x, props.categories)} src={'/content/icons/chevron-down.svg'}/>
                                        <img onClick={()=> onMoveUpClick(x, props.categories)} src={'/content/icons/chevron-up.svg'}/>
                                        <img onClick={()=> onDeleteClick(x)} src={'/content/icons/x-square.svg'}/>
                                    </div>}
                                </div>
                        })
                    }
                </div>
            </div>
            <div className={styles.tier}>=&gt;</div>
            <div className={styles.tier}>
            <div className={styles.categoryPanel} onContextMenu={e=>subCatMenu.show(e)}>
                    { 
                        secondTierCategories.map(x=> {
                            return <div key={x.categoryId + x.position} onClick={()=> setSelectedSecondTier(x)} className={`${styles.panelItem} ${x.categoryId===selectedSecondTier.categoryId ? styles.selected : ''}`}>
                                        {x.title}
                                        {selectedSecondTier.categoryId === x.categoryId && <div className={styles.buttonContainer}>
                                            <img onClick={()=> onMoveDownClick(x, selectedTopTier.children)} src={'/content/icons/chevron-down.svg'}/>
                                            <img onClick={()=> onMoveUpClick(x, selectedTopTier.children)} src={'/content/icons/chevron-up.svg'}/>
                                            <img onClick={()=> onDeleteClick(x)} src={'/content/icons/x-square.svg'}/>
                                        </div>}
                                    </div>
                        })
                    }
                </div>
            </div>
            <Menu id={CAT_MENU_ID}>
                <Item onClick={({event, props})=> onCreateCategoryClick(false)}>
                    Insert Category
                </Item>
            </Menu>
            <Menu id={SUBCAT_MENU_ID}>
                <Item onClick={({event, props})=> onCreateCategoryClick(true)}>
                    Insert SubCategory
                </Item>
            </Menu>
            <Drawer isOpen={isDrawerOpen} size={Drawer.SIZE_SMALL}>
                <div >
                    <div className={Classes.DRAWER_HEADER}>
                        Insert Category/SubCategory
                    </div>
                    <div className={Classes.DRAWER_BODY}>
                        <CreateCategoryForm parentId={parentCatForCreation.categoryId} parentName={parentCatForCreation.title} closeAction={()=>setIsDrawerOpen(false)}/>
                    </div>
                </div>
            </Drawer>
        </div>
    )
}

var mapStateToProps = state => ({
    categories: state.Category.HierarchyCategories?.sort((a,b)=> a.position - b.position) || [],
    flatCats: state.Category.FlattenedCategories
});

var mapDispatchToProps = dispatch => ({
    retrieveCategories: () => dispatch({ type: actionTypes.RETRIEVE_HIERARCHY_CATEGORIES }),
    updateCategory: (cat) => dispatch({ type: actionTypes.UPDATE_CATEGORY, category: cat }),
    deleteCategory: (catId) => dispatch({ type: actionTypes.DELETE_CATEGORY, categoryId: catId })
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CategoryHierarchy);