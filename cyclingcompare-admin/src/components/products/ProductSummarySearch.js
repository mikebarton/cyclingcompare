import { useState, useEffect } from 'react'
import { Icon, Intent, InputGroup, Classes } from '@blueprintjs/core'
import { IconNames } from '@blueprintjs/icons'
import * as styles from './products.module.scss'

function ProductSummarySearch (props) {
  var [keyword, setKeyword] = useState()
  useEffect(() => {
    if (props.products) {
      if (keyword) {
        var filtered = props.products.filter(x =>
          x.name.toLowerCase().includes(keyword.toLowerCase())
        )
        props.onFilteredProductsChanged(filtered)
      } else {
        props.onFilteredProductsChanged(props.products)
      }
    }
  }, [keyword, props.products])
  
  return (
    <div className={styles.searchBox}>
      <div>
        <InputGroup
          placeHolder='Search...'
          value={keyword}
          onChange={t => setKeyword(t.target.value)}
        />
      </div>
      <div className={styles.searchCross}>
        <Icon className={styles.searchIcon} icon={'cross'} onClick={()=>setKeyword('')}/>
      </div>
    </div>
  )
}

export default ProductSummarySearch
