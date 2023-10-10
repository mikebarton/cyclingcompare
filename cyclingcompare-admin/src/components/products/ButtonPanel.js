import { useState, useEffect } from 'react';
import { Icon } from '@blueprintjs/core'
import { IconNames } from '@blueprintjs/icons'
import * as styles from './ButtonPanel.module.scss'
import { PublishGlobalProduct, UnpublishGlobalProduct } from './api'


export default function({products}){
    var [productIds, setProductIds] = useState([]);

    useEffect(()=>{
        var ids = products.map(x=> x.globalProductId);
        setProductIds(ids);
    }, [products])

    return (
        <div className={styles.buttonContainer}>
              <div className={styles.button}>
                <Icon
                  icon={'send-message'}
                  htmlTitle={'Publish all displayed products'}
                  onClick={() => PublishGlobalProduct(productIds)}
                />
              </div>
              <div className={styles.button}>
                <Icon
                  icon={IconNames.DISABLE}
                  htmlTitle={'Unpublish all displayed products'}
                  onClick={() => UnpublishGlobalProduct(productIds)}
                />
              </div>
            </div>
    )
}