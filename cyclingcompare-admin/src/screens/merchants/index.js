import { useState, useEffect } from 'react'
import * as styles from './merchants.module.scss'
import StandardLayout from '../../components/shared/layout/StandardLayout'
import MerchantPanel from '../../components/merchants/MerchantPanel'
import Menu from '../../components/shared/menu'
import Header from '../../components/shared/header'

const Merchants = function () {
  return (
    <StandardLayout top={<Header />} left={<Menu />}>
      <div className={styles.productContainer}>
        <MerchantPanel />
      </div>
    </StandardLayout>
  )
}

export default Merchants
