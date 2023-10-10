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
            <h1>How to Choose the Right Cycling Shoes</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='shoes' className={styles.video}><iframe  src="https://www.youtube.com/embed/j3Il8pHgyjY" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>

          <p>Buying a new pair of cycling shoes is always an exciting purchase. Type, material, fit and look are many of the considerations when picking a new pair of shoes.</p>
                <p>The two main types of shoes are Road Bike Shoes and Mountain Bike Shoes. Both shoes have different cleats which clip into different type of pedals. Road bike shoes have stiffer soles in comparison to mountain bike shoes. While you can fit either type of pedals to your bike, we recommend that you stick to mountain bike shoes if you are a commuter or typically do more mountain bike riding than road cycling.</p>
                <p>Other important considerations when choosing cycling shoes are ventilation and cleaning. Shoes with mesh panels have better ventilation but are more difficult to clean. A good alternative is to look for shoes with holes for ventilation as they are much easier to clean.</p>
                <p>The video on this page provides additional guidance about choosing cycling shoes and explains how different types of shoes can improve your riding performance.</p>
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
