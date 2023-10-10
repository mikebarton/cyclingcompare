import { useEffect, useState } from 'react'
import { Button, MenuItem } from "@blueprintjs/core";
import { Select } from "@blueprintjs/select";
import { connect } from 'react-redux';
import * as actionTypes from './actionTypes';
import * as styles from './MerchantSelector.module.scss';

function MerchantSelector({loadMerchants, loadedMerchants, onMerchantChanged}){   
    useEffect(()=>{
        loadMerchants();
    }, []);

    useEffect(()=>{
        if(loadedMerchants)
            setMerchants(loadedMerchants);
    }, [loadedMerchants])

    var [merchants, setMerchants] = useState([]);

    var selectItems = [
        { Name: 'All Merchants', Merchant: null},
        { Name: '--------------------', Merchant: null },
        ...merchants.map(x=> { return {Name: x.name, Merchant: x}})
    ]

    var [selectedMerchant, setSelectedMerchant] = useState(selectItems[0])


    function onMerchantClicked(merchant){    
        var newMerchant = merchant || selectItems[0];
        setSelectedMerchant(newMerchant);
        if(onMerchantChanged)
            onMerchantChanged(newMerchant?.Merchant);   
        
    }

    function renderItem(x){
        return <MenuItem key={x.Name} text={x.Name} onClick={()=>onMerchantClicked(x)}/>;
    }

    return (
        <div className={styles.fullWidth}>
        <Select items={selectItems} 
                itemRenderer={renderItem}
        >
            <Button className={styles.fullWidth} text={selectedMerchant?.Name || 'Empty'} rightIcon="double-caret-vertical" />
        </Select>
        </div>
    )
}

var mapStateToProps = state => ({
    loadedMerchants: state.Merchant.Merchants || []
});

var mapDispatchToProps = dispatch => ({
    loadMerchants: ()=> dispatch({ type: actionTypes.GET_ALL_MERCHANTS })
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(MerchantSelector);
