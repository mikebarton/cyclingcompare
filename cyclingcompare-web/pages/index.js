import Link from 'next/link'
import Head from 'next/head'
import Layout from '../components/layouts/layout'
import ProductCarousel from '../components/productSummaries/productSummaryCarousel'
import * as styles from './index.module.scss'
import WidePageLayout from '../components/layouts/widePageLayout'
import VideoLink from '../components/videos/videoLink'
import Config from '../config'
import Carousel from 'react-elastic-carousel'

function AboutUs () {
  return (
    <div className={`${styles.banner} ${styles.desktop}`}>
      <Carousel enableAutoPlay={false} autoPlaySpeed={15000} transitionMs={500}  
        renderPagination={()=>{
          return <></>;
        }}
      >
      <div className={`${styles.infoRow} ${styles.image1}`}>
        <div className={styles.colContainer}>
          <div className={`${styles.col} ${styles.empty}`}></div>
          <div className={`${styles.col}`}>
          <div className={styles.inner}>
              <h1>Search</h1>
              <p>Save time and effort by searching more than 100,000 cycling products in one website.</p>
            </div>            
          </div>
          <div className={styles.col}>
          <div className={styles.inner}>
              <h1>Compare</h1>
              <p>Easily compare products from the world's biggest cycling merchants side by side.</p>
            </div>
          </div>
          <div className={styles.col}>
            <div className={styles.inner}>
              <h1>Save</h1>
              <p>Go directly to the retailer's website once you've found the right product at the right price.</p>
            </div>
          </div>
        </div>
      </div>
      <div className={`${styles.infoRow} ${styles.image2}`}><div className={styles.contentContainer}></div></div>
      <div className={`${styles.infoRow} ${styles.image3}`}/>
      <div className={`${styles.infoRow} ${styles.image4}`}/>
    </Carousel>

    </div>
  )
}

function AboutUsMobile () {
  return (
    <div className={`${styles.banner} ${styles.mobile}`}>
      
          <Carousel enableAutoPlay={false} autoPlaySpeed={15000} transitionMs={500}  
          renderPagination={()=>{
            return <></>;
          }}>       
            <div className={`${styles.infoRow} ${styles.mobImage1}`}>
            <div className={styles.colContainer}>
              <div className={`${styles.col}`}>
                <div className={styles.inner}>
                  <h1>Search</h1>
                  <p>
                    Save time and effort by searching more than 100,000 cycling products
                    in one website.
                  </p>
                </div>
              </div>
            </div>
          </div>
          <div className={`${styles.infoRow} ${styles.mobImage2}`}>
            <div className={styles.colContainer}>    
              <div className={`${styles.col}`}>
                <div className={styles.inner}>
                  <h1>Compare</h1>
                  <p>
                    Easily compare products from the world's biggest cycling merchants
                    side by side.
                  </p>
                </div>
              </div>    
            </div>
          </div>
          <div className={`${styles.infoRow} ${styles.mobImage3}`}>
            <div className={styles.colContainer}>    
              <div className={`${styles.col}`}>
                <div className={styles.inner}>
                  <h1>Save</h1>
                  <p>
                    Go directly to the retailer's website once you've found the right
                    product at the right price.
                  </p>
                </div>
              </div>
            </div>
          </div>
          <div className={`${styles.infoRow} ${styles.mobImage4}`}></div>
          </Carousel>
        
    </div>
  )
}


export default function Home ({ categories, bikes, clothing, accessories }) {
  return (<>
    <Head>
      <meta property="og:title" content="CyclingCompare.com.au" key="title" />      
      <meta property="og:description" content="Australia's Home of Cycling Deals" key="description" />
      <meta property="og:url" content="https://cyclingcompare.com.au/" />
    </Head>
    <Layout categories={categories}>
      <AboutUs />
      <AboutUsMobile />
      <div className={styles.verticalSpacer}></div>

      <WidePageLayout showPage>
        <div className={styles.ruleContainer}>
          <div className={styles.rule}>
            <div className={styles.ruleLine} />
            <h2 className={styles.ruleContent}>Top Bike Deals</h2>
            <div className={styles.ruleLine} />
          </div>
        </div>
        <div className={styles.carouselContainer}>
          <div className={styles.carousel}>
            <ProductCarousel items={bikes} />
          </div>
        </div>

        <div className={styles.ruleContainer}>
          <div className={styles.rule}>
            <div className={styles.ruleLine} />
            <h2 className={styles.ruleContent}>Top Clothing Deals</h2>
            <div className={styles.ruleLine} />
          </div>
        </div>
        <div className={styles.carouselContainer}>
          <div className={styles.carousel}>
            <ProductCarousel items={clothing} />
          </div>
        </div>

        <div className={styles.ruleContainer}>
          <div className={styles.rule}>
            <div className={styles.ruleLine} />
            <h2 className={styles.ruleContent}>Other Top Deals</h2>
            <div className={styles.ruleLine} />
          </div>
        </div>
        <div className={styles.carouselContainer}>
          <div className={styles.carousel}>
            <ProductCarousel items={accessories} />
          </div>
        </div>
      </WidePageLayout>
      <div className={styles.verticalSpacer}></div>

      <WidePageLayout>
        <div className={styles.videoContainer}>
          <div className={styles.trainingGuideContainer}>
            <div className={styles.trainingGuideBackground}>
              <div className={`${styles.trainingGuide} ${styles.fullTitle}`}>
                <h1 className={styles.titleLink}><Link href='/events'>Event Calendar</Link></h1>
                <div className={styles.linkContainer}>
                  <p>Check out our calendar for details on most cycling events across Australia</p>
                </div>
              </div>
            </div>
          </div>
          <div className={`${styles.trainingGuideContainer} ${styles.trainingGuideContainer2}`}>
            <div className={styles.trainingGuideBackground}>
              <div className={`${styles.trainingGuide} ${styles.fullTitle}`}>
                <h1 className={styles.titleLink}><Link href='/videos'>Cycling Guide</Link></h1>
                <div className={styles.linkContainer}>
                  {/* <div className={styles.videoLink}><Link  href='/videos/components'>Bike Components and Accessories</Link></div>
                  <div className={styles.videoLink}><Link href='/videos/maintenance'>Bike Care and Maintenance</Link></div> */}
                  <p>If you are a cycling amateur or an enthusiast, you need to check out these fantastic video guides to help you find product reviews or teach you great tips for basic bike maintenance.</p>
                  <p>These are some of our favourite video guides from our friends at GCN. Visit our Guides page for more awesome content.</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </WidePageLayout>
    </Layout>
    </>
  )
}

export async function getStaticProps () {
  const catRres = await fetch(
    Config.ApiGateway.HostName + Config.ApiGateway.GetAllCategoriesPath
  )
  const categories = await catRres.json()

  const topBikesRes = await fetch(
    Config.ApiGateway.HostName + Config.ApiGateway.GetTopDealsByCategory + '1'
  )
  const topBikes = await topBikesRes.json()

  const clothingTopDeals = await fetch(
    Config.ApiGateway.HostName + Config.ApiGateway.GetTopDealsByCategory + '8'
  )
  const clothingDeals = await clothingTopDeals.json()

  const accessoryTopDeals = await fetch(
    Config.ApiGateway.HostName + Config.ApiGateway.GetTopDealsByCategory + '13'
  )
  const accessoryDeals = await accessoryTopDeals.json()

  if (!categories || !topBikes || !clothingDeals || !accessoryDeals) {
    return { notFound: true }
  }

  return {
    props: {
      categories: categories,
      bikes: topBikes,
      clothing: clothingDeals,
      accessories: accessoryDeals
    },
    revalidate: 1800
  }
}
