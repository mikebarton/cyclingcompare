import * as styles from './mobileGallery.module.scss'
import Carousel from 'react-elastic-carousel'

export default function MobileGallery({product}){

    return (
        <div className={styles.imageContainer}>            
            <Carousel itemsToScroll={1} itemsToShow={1}>
              {/* <div className={styles.previewImage}> */}
                <img src={product.imageUrl}/>
              {/* </div>               */}
            </Carousel>
          </div>
    )
}