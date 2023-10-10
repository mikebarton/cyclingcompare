import { useState, useEffect } from 'react'
import { connect } from 'react-redux'
import * as actionTypes from './actionTypes'
import { Drawer, Classes, Button, Icon, Intent } from '@blueprintjs/core'
import { IconNames } from '@blueprintjs/icons'
import { Markup } from 'interweave';
import { GetAllMerchants, UnpublishGlobalProductByMerchantId, PublishGlobalProductByMerchantId } from './api'
import * as styles from './MerchantPanel.module.scss'

function MerchantPanel () {
  var [merchants, setMerchants] = useState([])
  var [isTCDrawerOpen, setIsTCDrawerOpen] = useState(false)
  var [currentTCs, setCurrentTCs] = useState()

  useEffect(async () => {
    var merchants = await GetAllMerchants()
    setMerchants(merchants)
  }, [])

  function showTCs (merchant) {
    setCurrentTCs(merchant.termsAndConditions)
    setIsTCDrawerOpen(true)
  }
  
  function publishProducts(merchant){

  }

  return (
    <>
      <div className={styles.merchantList}>
        {merchants.map(x => {
          return (
            <div className={styles.merchant}>
              <div className={styles.merchantHeader}>
                <h3>{x.name}</h3>({x.apiIdentifier} - {x.productCount} products)
              </div>
              <div className={styles.merchantButtons}>
                <div className={styles.controlButton}>
                  <Icon
                    icon={'send-to-map'}
                    htmlTitle={'Show Ts and Cs'}
                    onClick={() => showTCs(x)}
                  />
                </div>
                <div className={styles.controlButton}>
                  <Icon
                    icon={'send-message'}
                    htmlTitle={'Publish all Products'}
                    onClick={() => PublishGlobalProductByMerchantId(x.merchantId)}
                  />
                </div>
                <div className={styles.controlButton}>
                  <Icon
                    icon={IconNames.DISABLE}
                    htmlTitle={'Unpublish all Products'}
                    onClick={() => UnpublishGlobalProductByMerchantId(x.merchantId)}
                  />
                </div>
              </div>
              <div>{x.summary}</div>

            </div>
          )
        })}
      </div>
      <Drawer isOpen={isTCDrawerOpen} size={Drawer.SIZE_LARGE} canOutsideClickClose>
        <div>
          <div className={Classes.DRAWER_HEADER}><h2>Terms and Conditions</h2></div>
          <div className={Classes.DRAWER_BODY}><div className={styles.termsContainer}><Markup content={currentTCs}/></div></div>
          <div className={Classes.DRAWER_FOOTER}>
            <Button onClick={() => setIsTCDrawerOpen(false)}>Close</Button>
          </div>
        </div>
      </Drawer>
    </>
  )
}

var mapStateToProps = state => ({
  // merchants: state.Merchant.Merchants || []
})

var mapDispatchToProps = dispatch => ({
  // getMerchants: () => dispatch({ type: actionTypes.GET_ALL_MERCHANTS })
})

export default connect(mapStateToProps, mapDispatchToProps)(MerchantPanel)
