import * as styles from './categories.module.scss';
import { useState, useEffect } from 'react'
import { connect } from 'react-redux';
import * as actionTypes from './actionTypes';
import { Column, Table, Cell, RenderMode } from "@blueprintjs/table";
import { MenuItem, Button } from '@blueprintjs/core';
import { Popover2, Tooltip2 } from "@blueprintjs/popover2";
import { Select } from "@blueprintjs/select";
import '@blueprintjs/table/lib/css/table.css'
import '@blueprintjs/popover2/lib/css/blueprint-popover2.css'

const CategoryMappingTable = function (props) {
    const [selectedCategories, setSelectedCategories] = useState([]);
    const [selectedSubCategories, setSelectedSubCategories] = useState([]);
    const [displayableExternalCats, setDisplayableExternalCats] = useState([])

    useEffect(() => {
        props.getExternalCategories();
        props.getCategoryHierarchy();
        props.getCategoryMappings();
    }, [])

    useEffect(() => {
        if (props.nestedCategories && props.nestedCategories.length > 0 && props.externalCategories && props.mappings) {
            var newSelectedCategories = [];
            var newSelectedSubCategories = [];
            var filteredExternalCategories = props.externalCategories.filter(x=> !props.merchant || x.merchantId === props.merchant.merchantId);
            filteredExternalCategories = props.showAllMappings ? filteredExternalCategories : filteredExternalCategories.filter(x=> !props.mappings.some(m=>m.externalCategoryName === x.externalCategoryName & m.externalSubCategoryName === x.externalSubCategoryName))
            setDisplayableExternalCats(filteredExternalCategories);
            props.mappings?.forEach(x => {
                var externalCategory = filteredExternalCategories?.find(y => x.externalCategoryName === y.externalCategoryName && x.externalSubCategoryName === y.externalSubCategoryName && x.merchantId === y.merchantId);
                if (externalCategory) {
                    var rowNum = filteredExternalCategories.indexOf(externalCategory);
                    var selectedCategory = props.nestedCategories.find(y => x.categoryId === y.categoryId) || props.nestedCategories.find(y => y.children.some(z => z.categoryId === x.categoryId));
                    var selectedSubCategory = selectedCategory.categoryId === x.categoryId ? selectedCategory : selectedCategory.children.find(y => y.categoryId === x.categoryId);
                    newSelectedCategories[rowNum] = selectedCategory;
                    newSelectedSubCategories[rowNum] = selectedSubCategory;
                }
            });
            setSelectedCategories(newSelectedCategories);
            setSelectedSubCategories(newSelectedSubCategories);
        }
    }, [props.mappings, props.externalCategories, props.nestedCategories, props.merchant, props.showAllMappings])

    const defaultRenderer = (colName, rowNum) => {
        var category = displayableExternalCats[rowNum];
        return <Cell>
            <Popover2>
                <Tooltip2 content={category[colName]}>
                    <div >
                        {category[colName]}
                    </div>
                </Tooltip2>
            </Popover2>
        </Cell>
    }

    const itemRenderer = function (item, { handleClick }) {
        return (
            <MenuItem
                key={item.categoryId}
                text={item.title}
                onClick={handleClick}
                shouldDismissPopover={true}
            />
        )
    }

    const filterCategories = function (query, cat, _index, exactMatch) {
        if (!query)
            return true;

        return cat.title.toLowerCase().indexOf(query.toLowerCase()) >= 0;
    }

    const categoryRenderer = (rowNum) => {

        const onCategorySelected = (cat) => {
            var selectedCats = selectedCategories;
            selectedCats[rowNum] = cat;
            setSelectedCategories(selectedCats)
        }

        return <Cell>
            <Select className={styles.selectBox}
                items={props.nestedCategories}
                itemRenderer={itemRenderer}
                onItemSelect={onCategorySelected}><Button text={selectedCategories[rowNum]?.title || 'Category'} rightIcon="caret-down" /></Select>
        </Cell>
    }

    const subCategoryRenderer = function (rowNum) {
        const category = selectedCategories[rowNum];

        const onCategorySelected = (cat) => {
            var selectedSubCats = selectedSubCategories;
            selectedSubCats[rowNum] = cat;
            setSelectedSubCategories(selectedSubCats)
            props.updateCategoryMapping(displayableExternalCats[rowNum], cat);
        }
        var children = [...category?.children || []];
        if (category)
            children.push(category);


        return <Cell>
            <Select className={styles.selectBox}
                items={children}
                itemRenderer={itemRenderer}
                onItemSelect={onCategorySelected}
                filterable={true}
                itemPredicate={filterCategories}><Button text={selectedSubCategories[rowNum]?.title || 'SubCategory'} rightIcon="caret-down" /></Select>
        </Cell>
    }



    return (

        <Table rowHeights={[...new Array(displayableExternalCats?.length || 0)].map(x => 35)} renderMode={RenderMode.NONE} columnWidths={[400, 300, 100, 50, 250, 250]} enableColumnResizing={true} numRows={displayableExternalCats?.length || 0}>
            <Column name={'External Category'} cellRenderer={rowNum => defaultRenderer('externalCategoryName', rowNum)} />
            <Column name={'External SubCategory'} cellRenderer={rowNum => defaultRenderer('externalSubCategoryName', rowNum)} />
            <Column name={'Merchant'} cellRenderer={rowNum => defaultRenderer('merchantName', rowNum)} />
            <Column name={''} cellRenderer={rowNum => <Cell>==&gt;</Cell>} />
            <Column name={'Target Category'} cellRenderer={rowNum => categoryRenderer(rowNum)} />
            <Column name={'Target SubCategory'} cellRenderer={rowNum => subCategoryRenderer(rowNum)} />
        </Table>

    )
}

var mapStateToProps = state => ({
    externalCategories: state.Category.ExternalCategories,
    nestedCategories: state.Category.HierarchyCategories,
    mappings: state.Category.Mappings
});

var mapDispatchToProps = dispatch => ({
    getExternalCategories: () => dispatch({ type: actionTypes.RETRIEVE_EXTERNAL_CATEGORIES }),
    getCategoryHierarchy: () => dispatch({ type: actionTypes.RETRIEVE_HIERARCHY_CATEGORIES }),
    getCategoryMappings: () => dispatch({ type: actionTypes.GET_CATEGORY_MAPPINGS }),
    updateCategoryMapping: (externalCategory, category) => dispatch({ type: actionTypes.UPDATE_CATEGORY_MAPPING, mapping: { externalCategoryName: externalCategory.externalCategoryName, externalSubCategoryName: externalCategory.externalSubCategoryName, merchantId: externalCategory.merchantId, categoryId: category.categoryId } })
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(CategoryMappingTable);
