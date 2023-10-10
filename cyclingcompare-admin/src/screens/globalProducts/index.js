import { useState } from 'react'
import * as styles from './globalProducts.module.scss'
import StandardLayout from '../../components/shared/layout/StandardLayout'
import Menu from '../../components/shared/menu'
import Header from '../../components/shared/header'
import ProductTable from '../../components/products/ProductTable'
import ProductSummaryFilter from '../../components/products/ProductSummaryFilter'
import ProductPanel from '../../components/products/ProductPanel'
import ProductSummarySearch from '../../components/products/ProductSummarySearch'
import ButtonPanel from '../../components/products/ButtonPanel'

const GlobalProducts = function () {
  var [productList, setProducts] = useState([])
  var [filteredProducts, setFilteredProducts] = useState([])
  var [selectedProduct, setSelectedProduct] = useState({})

  return (
    <StandardLayout top={<Header />} left={<Menu />}>
      <div className={styles.productContainer}>
        <div className={styles.productTable}>
          <div className={styles.productTableHeader}>
            <ProductSummaryFilter
              OnFilteredProductsChanged={products => setProducts(products)}
            />
            <ButtonPanel products={filteredProducts}/>
            <ProductSummarySearch
              products={productList}
              onFilteredProductsChanged={list => setFilteredProducts(list)}
            />
          </div>
          <ProductTable
            products={filteredProducts}
            onProductSelected={prod => setSelectedProduct(prod)}
          />
        </div>
        <div className={styles.productPanelContainer}>
          <ProductPanel productId={selectedProduct.globalProductId} />
        </div>
      </div>
    </StandardLayout>
  )
}

export default GlobalProducts
