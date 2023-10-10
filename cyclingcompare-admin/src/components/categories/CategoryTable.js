import { useEffect, useState } from 'react'
import * as styles from './categories.module.scss'
import { Column, Table, Cell, EditableCell, RenderMode } from "@blueprintjs/table";
import '@blueprintjs/table/lib/css/table.css'
import { connect } from 'react-redux';
import * as actionTypes from './actionTypes'
import { Menu, Item, Separator, Submenu, useContextMenu } from "react-contexify";
import "react-contexify/dist/ReactContexify.css";

const MENU_ID = "menu-id";

function CategoryTable(properties) {
    const { show } = useContextMenu({ id: MENU_ID });

    useEffect(() => {
        properties.retrieveCategories()
    }, [])
    
    const doShow = (e, category) =>{
        show(e, {props: {category: category}});
    }

    const defaultRenderer = (colName, rowNum) => {
        var category = properties.categories[rowNum];
        return <Cell>
                    <div onContextMenu={(e)=> doShow(e, category)}>
                        {category[colName]}
                    </div>
                </Cell>
    }

    const renderEdittable = (colName, rowNum) => {
        return <EditableCell onConfirm={(val) => onCellConfirm(val, colName, rowNum)}
            value={properties.categories[rowNum][colName]} />
    }

    const onCellConfirm = (updatedVal, colName, rowNum) => {
        var category = properties.categories[rowNum]
        if (category[colName] !== updatedVal) {
            category[colName] = updatedVal;
            properties.updateCategory(category);
        }
    }

    return (
        <>
            <Table renderMode={RenderMode.NONE} columnWidths={[100, 200, 80, 80, 200, 100, 200, 200]} enableColumnResizing={false} numRows={properties.categories?.length || 0}>
                <Column name={'CategoryId'} cellRenderer={rowNum => defaultRenderer('categoryId', rowNum)} />
                <Column name={'Title'} cellRenderer={rowNum => renderEdittable('title', rowNum)} />
                <Column name={'ParentId'} cellRenderer={rowNum => renderEdittable('parentId', rowNum)} />
                <Column name={'Position'} cellRenderer={rowNum => renderEdittable('position', rowNum)} />
                <Column name={'Description'} cellRenderer={rowNum => renderEdittable('description', rowNum)} />
                <Column name={'IsEnabled'} cellRenderer={rowNum => renderEdittable('isEnabled', rowNum)} />
                <Column name={'UrlSlug'} cellRenderer={rowNum => renderEdittable('urlSlug', rowNum)} />
                <Column name={'Category Image Url'} cellRenderer={rowNum => renderEdittable('categoryBannerImage', rowNum)} />
            </Table>
            <Menu id={MENU_ID}>
                <Item onClick={({event, props})=> properties.deleteCategory(props.category.categoryId)}>
                    Delete Category
                </Item>
            </Menu></>
    )
}



var mapStateToProps = state => ({
    categories: state.Category.FlattenedCategories
});

var mapDispatchToProps = dispatch => ({
    retrieveCategories: () => dispatch({ type: actionTypes.RETRIEVE_FLAT_CATEGORIES }),
    updateCategory: (cat) => dispatch({ type: actionTypes.UPDATE_CATEGORY, category: cat }),
    deleteCategory: (catId) => dispatch({ type: actionTypes.DELETE_CATEGORY, categoryId: catId })
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CategoryTable);