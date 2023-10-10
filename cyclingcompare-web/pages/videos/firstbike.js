import WidePageLayout from '../../components/layouts/widePageLayout'
import Link from 'next/link'
import Layout from '../../components/layouts/layout'
import Config from '../../config'
import * as styles from './videos.module.scss'
import BannerDisplay from '../../components/adbanners/BannerDisplay'

export default function FirstBike({ categories }) {
  return (
    <Layout categories={categories}>
      <WidePageLayout>
        <BannerDisplay width={728} height={90} />
        <div className={styles.videoPage}>
          <div>
            <h1>How to Buy Your First Bike</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='firstBike' className={styles.video}><iframe src="https://www.youtube.com/embed/_T7NTe3uBN4" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>

                  <p>If you are looking to buy your first road bike, one of the main considerations is your budget. Lots of people forget that there are some added costs such as pedals, water bottle, bottle cage, helmet and clothes which are typically in addition to your bike cost, so please don’t forget to make an allowance to cover these.</p>
                <p>Other considerations when buying your first bike are the frame material (e.g. aluminium or carbon), type of use (e.g. commuting hybrid, mountain bike, road bike) and amount of use. If you are planning to ride regularly, we suggest that you consider spending a little bit more on higher quality components that are more durable, improve comfort and increase performance. For example, a good pair of tyres can offer better puncture protection, improved grip and reliability during your ride.</p>
                <p>This video provides great tips about the type of components, materials and characteristics that you should be looking at when buying your first bike.</p>
                <p>Bike Sales or Bike Clearance events are a fantastic opportunity to stretch your budget and get more bang for your buck, so please browse through our website to find great deals!</p>
          </div>
        </div>
      </WidePageLayout>
    </Layout>
  )
}

export async function getStaticProps () {
  const catRres = await fetch(
    Config.ApiGateway.HostName + Config.ApiGateway.GetAllCategoriesPath
  )
  const categories = await catRres.json()

  if (!categories) {
    return { notFound: true }
  }

  return {
    props: {
      categories: categories
    },
    revalidate: 1800
  }
}
