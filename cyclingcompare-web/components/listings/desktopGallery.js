import * as styles from './desktopGallery.module.scss'
import Carousel from 'react-elastic-carousel'


export default function DesktopGallery({product}){


    return (
        <div className={styles.imageContainer}>            
            {/* <div><img className={styles.mainImage} src={product.imageUrl}/></div> */}
            <div className={styles.mainImage}>
                <Carousel itemsToScroll={1} itemsToShow={1}>
                <div className={styles.previewImage}>
                    <img src={product.imageUrl}/>
                </div>              
                </Carousel>
            </div>
          </div>
    )
}