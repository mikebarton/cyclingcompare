import Link from 'next/link'
import Carousel from 'react-elastic-carousel'
import * as styles from './productSummaryCarousel.module.scss'
import ProductItem from './carouselProductItem'

export default function ProductSummaryCarousel({items}){
    const breakPoints = [
        { width: 1, itemsToShow: 1, itemsToScroll: 1},
        { width: 320, itemsToShow: 2, itemsToScroll: 1 },
        { width: 660, itemsToShow: 3, itemsToScroll: 1 },
        // { width: 620, itemsToShow: 4, itemsToScroll: 2 },
        { width: 850, itemsToShow: 4, itemsToScroll: 3 },
        { width: 1000, itemsToShow: 5, itemsToScroll: 4 },
        { width: 1150, itemsToShow: 6, itemsToScroll: 5 },
        { width: 1350, itemsToShow: 7 },
        { width: 1750, itemsToShow: 6 },
      ]

      function RenderPagination(pages, activePage, onClick){
          return (<></>)
      }

    return (
        <Carousel itemsToScroll={2} itemsToShow={10} breakPoints={breakPoints} renderPagination={RenderPagination}>
            { items.map(item => {
                    return <div className={styles.productWrapper} key={item.productId}><ProductItem item={item}/></div>
                })
            }
        </Carousel>
    )
}