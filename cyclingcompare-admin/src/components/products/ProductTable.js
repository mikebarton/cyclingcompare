import { useEffect, useState } from 'react'
import * as styles from './products.module.scss'
import {
  Column,
  Table,
  Cell,
  EditableCell,
  RenderMode, SelectionModes, RegionCardinality 
} from '@blueprintjs/table'
import '@blueprintjs/table/lib/css/table.css'
import { connect } from 'react-redux'
import * as actionTypes from './actionTypes'

function ProductTable (properties) {

  const defaultRenderer = (colName, rowNum) => {
    var product = properties.products[rowNum]
    return (
      <Cell>
        <div>{product[colName]}</div>
      </Cell>
    )
  }

  const regionTransformer = e => {
      if(properties.onProductSelected && e.rows.length > 0){
          var selectedProduct = properties.products[e.rows[0]]
          properties.onProductSelected(selectedProduct);
      }
    return {
      rows: e.rows
    }
  }

  return (
    <>
      <Table
        selectedRegionTransform={regionTransformer}
        // selectionModes={[RegionCardinality.FULL_ROWS]}
        renderMode={RenderMode.NONE}
        columnWidths={[200, 400]}
        enableColumnResizing={false}
        numRows={properties.products?.length || 0}
      >
        <Column
          name={'Brand'}
          cellRenderer={rowNum => defaultRenderer('brand', rowNum)}
        />
        <Column
          name={'Product Name'}
          cellRenderer={rowNum => defaultRenderer('name', rowNum)}
        />
      </Table>
    </>
  )
}

var mapStateToProps = state => ({
  // products: state.Product.GlobalProducts
})

var mapDispatchToProps = dispatch => ({
  // getProductSummaries: ()=> dispatch({ type: actionTypes.GET_ALL_PRODUCT_SUMMARIES })
})

export default connect(mapStateToProps, mapDispatchToProps)(ProductTable)
