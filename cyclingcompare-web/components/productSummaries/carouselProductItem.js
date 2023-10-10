import * as styles from './carouselProductItem.module.scss'
import Link from 'next/link'
import NumberFormat from 'react-number-format';

export default function ProductItem({item}){

    function RenderDiscount(){
        if((item.minPriceRrp) && (item.minPriceRrp > 0) && (item.minPrice !== item.minPriceRrp)){
            return (
                <div className={styles.saveBadge}>
                    <div className={styles.badgePrice}>Save <NumberFormat decimalScale={0} displayType={'text'} thousandSeparator={true} prefix={'$'} value={item.minPriceRrp - item.minPrice} /></div>
                </div>
            )
        }
        
        return <></>
    }

    function RenderButton(){
        if(item.hasStock === null || item.hasStock === true)
            return (<div className={styles.button}><Link className={styles.buttonLink} href={'/product/' + item.urlSlug + '/' + item.productId}>View all prices</Link></div>)
        else
            return (<div className={`${styles.button} ${styles.redButton}`}><Link className={styles.buttonLink} href={'/product/' + item.urlSlug + '/' + item.productId}>Out of Stock</Link></div>)
    }

    return (
        <div key={item.productId} className={styles.productSummaryContainer}>
            <div className={styles.imageContainer}>
                <Link href={'/product/' + (item.urlSlug || 'noslug') + '/' + item.productId}>
                    <img src={item.previewImageUrl} className={styles.previewImage}/>
                </Link>
            </div>
            <div className={styles.textContainer}>
                <div className={styles.productTitle}><Link href={'/product/' + (item.urlSlug || 'noslug') + '/' + item.productId}>{item.productName}</Link></div>
                <div className={styles.priceLabel}>from <NumberFormat decimalScale={2} value={item.minPrice} displayType={'text'} thousandSeparator={true} prefix={'$'}/></div>
            </div>
            <RenderButton/>
            <RenderDiscount/>
        </div>
    )
}