import { useEffect, useState } from 'react'
import { Button, MenuItem } from "@blueprintjs/core";
import { Select } from "@blueprintjs/select";
import { connect } from 'react-redux';
import * as actionTypes from './actionTypes'
import * as styles from './products.module.scss';

function ProductSummaryFilter(props){
    var [brands, setBrands] = useState([])

    var filters = [
        { Name: 'All Products', filter: (prod)=> true},
        { Name: 'Reviewed Products', filter: (prod)=> prod.isReviewed},
        { Name: 'Not Reviewed Products', filter: (prod)=> !prod.isReviewed},
        { Name: '--------------------', filter: ()=>false },
        ...brands.map(x=> { return {Name: x, filter: (prod)=> prod.brand === x}})
    ]

    var [selectedFilter, setSelectedFilter] = useState(filters[0])
    var [filteredProducts, setFilteredProducts] = useState([]);


    useEffect(() => {
        props.getProductSummaries()
    }, [])

    useEffect(()=>{
        var filtered = props.products?.filter(selectedFilter.filter);
        if(props.OnFilteredProductsChanged)
            props.OnFilteredProductsChanged(filtered);

        var newBrands = props.products.map(x=> x.brand).filter((value, index, self) => self.indexOf(value) === index).sort()
        setBrands(newBrands)
    }, [props.products])

    function onFilterClicked(filter){
        setSelectedFilter(filter);
        var filtered = props.products.filter(filter.filter);
        setFilteredProducts(filtered);
        if(props.OnFilteredProductsChanged)
            props.OnFilteredProductsChanged(filtered);
    }

    return (
        <div className={styles.fullWidth}>
        <Select items={filters} 
                itemRenderer={(x)=><MenuItem key={x.Name} text={x.Name} onClick={()=>onFilterClicked(x)}/>}
        >
            <Button className={styles.fullWidth} text={selectedFilter.Name} rightIcon="double-caret-vertical" />
        </Select>
        </div>
    )
}

var mapStateToProps = state => ({
    products: state.Product.GlobalProducts
});

var mapDispatchToProps = dispatch => ({
    getProductSummaries: ()=> dispatch({ type: actionTypes.GET_ALL_PRODUCT_SUMMARIES })
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(ProductSummaryFilter);
