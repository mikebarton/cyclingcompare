import { useRouter } from 'next/router'
import Head from 'next/head'
import Layout from '../../../components/layouts/layout'
import PageSubLayout from '../../../components/layouts/pageSubLayout'
import DesktopGallery from '../../../components/listings/desktopGallery'
import MobileGallery from '../../../components/listings/mobileGallery'
import * as styles from './[prodId].module.scss'
import NumberFormat from 'react-number-format'
import Config from '../../../config'
import { useEffect, useState } from 'react'
import * as ga from '../../../lib/ga'

export default function ProductPage ({ product, categories }) {
  const [topDeals, setTopDeals] = useState([]);
  const [expandDescription, setExpandDescription] = useState(false);
  useEffect(()=>{
    if(product){
    ga.event({
      action: "view_item",
      params : {
        productId: product.productId,
        name: product.name      
      }
    });

    var grouped = product.merchantListings.reduce((agg, current)=>{      
      agg[current.merchantId] = [...(agg[current.merchantId] || []), current]
      return {...agg};
    }, {});
    var shortenedTopDeals = Object.keys(grouped).reduce((agg, x)=>{
      var listings = grouped[x].slice(0, 2) || [];
      return [...agg, ...listings]
    }, []);
    setTopDeals(shortenedTopDeals);
  }
  },[product])


  const router = useRouter()
  if (router.isFallback) {
    return <div>Loading...</div>
  }
  const { category, subCategory } = router.query
  const categoryItem = categories.find(x => x.urlSlug === category)

  const FeaturesBlock = function (props) {
    if (props.features && props.features.length > 0) {
      return (
        <div>
          <h2>Features</h2>

          <div>
            <ul>
              {props.features.map((x, index) => (
                <li key={index}>{x}</li>
              ))}
            </ul>
          </div>
        </div>
      )
    }
    return <></>
  }

  const SpecsBlock = function (props) {
    if (props.specs && Object.keys(props.specs).length > 0)
      return (
        <div>
          <h2>Specifications</h2>
          <div className={styles.itemTable}>
            <ul>
              <>
                {Object.keys(props.specs).map(element => {
                  return (
                    <li>
                      {element}: {props.specs[element]}
                    </li>
                  )
                })}
              </>
            </ul>
          </div>
        </div>
      )
    return <></>
  }

  const postConversion = function(merchantListing){
    ga.event({ action: 'View Deal', params: { merchantName: merchantListing.merchantName }})
    var postData = {
      referer: document.referrer,
      productId: merchantListing.productId,
      merchantId: merchantListing.merchantId
    }

    var requestOptions = {
      method: 'POST',
      headers: { 
            'Content-Type': 'application/json',            
        },
      body: JSON.stringify(postData)
    }
    var url = Config.ListingService.Hostname + Config.ListingService.SubmitConversionPath
    fetch(url, requestOptions)
  }

  return (
    <Layout categories={categories}>
      <Head>
        <title>Cycling Compare - {product.name}</title>
        <meta property="og:title" content={product.name} key="title" />   
        {(typeof window !== "undefined") && <meta property="og:url" content={window.location.href} />}
        
      </Head>
      <PageSubLayout categories={[]} leftPane={[]} hideLeftPane>
        <div className={styles.mainContainer}>
          <h1>{product.name}</h1>
          <div className={styles.topPaneDesktop}>
            <div className={styles.desktopGallery}>
              <DesktopGallery product={product} />
            </div>
            <div className={styles.saleDetails}>
              <span className={styles.bestDealLabel}>
                Best Price -{' '}
                <NumberFormat
                  className={styles.priceLabel}
                  value={product.merchantListings[0]?.price}
                  displayType={'text'}
                  isNumericString                  
                  thousandSeparator={true}
                  prefix={ product.currency === 'GBP' ? '£' : '$'}
                />
              </span>
              <div className={styles.saleTarget}>
                for sale at{' '}
                <span>
                  <a
                    href={product.merchantListings[0].merchantTrackingUrl}
                    target='_blank'
                    referrerPolicy='no-referrer-when-downgrade'
                    rel='nofollow noindex'
                  >
                    {product.merchantListings[0].merchantName}
                  </a>
                </span>
              </div>
              <div className={styles.button}>
                <a onClick={()=>postConversion(product.merchantListings[0])}
                  target='_blank'
                  href={product.merchantListings[0].trackingUrl}
                  referrerPolicy='no-referrer-when-downgrade'
                  rel='nofollow noindex'
                >
                  Go to Deal!
                </a>
              </div>
              { product.merchantListings.length > 1 && <div className={styles.dealsLink}>
                <a href='#bestDealsSection'>See more prices...</a>
              </div>}
            </div>
          </div>
          <div className={styles.mobileGallery}>            
            <MobileGallery product={product} />
          </div>

          <div className={styles.deals} id='bestDealsSection'>
            <div className={styles.header}>Best Deals</div>
            {topDeals.map((listing, index) => {
              const isDiscounted = listing.priceRrp && listing.priceRrp > 0 & listing.price !== listing.priceRrp

              return (
                <div className={styles.dealRow}>
                  <div className={styles.listingDetails}>
                    <div className={styles.merchantName}>{listing.merchantName}</div>
                    <div className={styles.listingName}>{listing.name}</div>
                  </div>
                  
                  <div className={styles.merchantPrices}>
                    <div className={styles.merchantPrice}>
                      {isDiscounted ? (
                        <s className={styles.optionalItems}>
                          <NumberFormat
                            value={listing.priceRrp}
                            displayType={'text'}
                            thousandSeparator={true}
                            prefix={'RRP ' + (product.currency === 'GBP' ? '£' : '$')}
                          />
                        </s>
                      ) : (
                        ''
                      )}
                      <span>
                        <NumberFormat
                          value={listing.price}
                          displayType={'text'}
                          thousandSeparator={true}
                          prefix={product.currency === 'GBP' ? '£' : '$'}
                        />
                      </span>
                    </div>
                    {/* <div className={styles.merchantPrice}>
                        <NumberFormat value={listing.price} displayType={'text'} thousandSeparator={true} prefix={'$'}/>
                      </div> */}
                    <div className={styles.button}>
                      <a onClick={()=>postConversion(listing)}
                        target='_blank'
                        href={listing.trackingUrl}
                        referrerPolicy='no-referrer-when-downgrade'
                        rel='nofollow noindex'
                      >
                        Go to Deal!
                      </a>
                    </div>
                    </div>
                  
                </div>
              )
            })}
            { topDeals.length < product.merchantListings.length && <div className={styles.footer} onClick={()=> setTopDeals(product.merchantListings)}>Show all deals</div>}
          </div>

          <div className={styles.productDescription}>
            <p className={`${expandDescription ? styles.expandedDescription : styles.restrictedDescription}`}>{product.description}</p>
            <a onClick={()=>setExpandDescription(!expandDescription)}>{ expandDescription ? 'Hide' : 'Read More...'}</a>
          </div>
          <FeaturesBlock features={product.features} />
          <SpecsBlock specs={product.specs} />

          
        </div>
      </PageSubLayout>
    </Layout>
  )
}

export async function getStaticPaths(){

  return {paths: [], fallback: true}
}

export async function getStaticProps ({ params }) {
  
  const res = await fetch(
    Config.ApiGateway.HostName + Config.ApiGateway.GetAllCategoriesPath
  )
  const categories = await res.json()

  var productId = params.prodId

  const productRes = await fetch(
    Config.ApiGateway.HostName + Config.ApiGateway.GetListingsById +
      productId
  )
  const product = await productRes.json()

  if (!product) {
    return { notFound: true }
  }

  return {
    props: { product: product, categories: categories },
    revalidate: 1800
  }
}
