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
            <h1>How To Choose A Cycle Helmet</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='helmet' className={styles.video}><iframe src="https://www.youtube.com/embed/jhbnJhQVC-s" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>
          <p>In Australia it is compulsory to hear an approved helmet when cycling. Needless to say, wearing a helmet is fundamental to improve safety of bike riders.</p>
                <p>There are several considerations when choosing a helmet namely price, comfort, weight, fit, ventilation and look. Buying a more expensive helmet doesn’t mean the helmet is safer. We recommend riders to look for an approved bicycle helmet that complies with Australian Standard AS 2063 or AS/NZS 2063 (look for the sticker inside).</p>
                <p>Perhaps the second most important consideration after safety is fit and comfort. Actually, they both go hand in hand as a helmet with the right fit will be better secured and therefore, be safer. We recommend you visit your local store and try a few helmets before you decide to buy one.</p>
                <p>Other factors such as ventilation and visibility greatly depend on the type of riding you do and the local conditions of your area. For instance, riders in hot countries may benefit from helmets with better ventilation than those who live in cold places. If you are a regular commuter, it is a good idea to buy a helmet with visible colours and/or reflective strips.</p>
                <p>The video in this page provides some further advice and guidance on choosing the right helmet. For great deals and promotions, please browse the helmets page under the Protection tap above.</p>
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
