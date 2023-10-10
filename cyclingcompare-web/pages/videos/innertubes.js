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
            <h1>How to Choose Inner Tubes</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='tubes' className={styles.video}><iframe  src="https://www.youtube.com/embed/R_dpK0_LK4Q" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>
          <p>Unless you are running tubeless wheels, you are most likely going to need a spare tubes for your bike.  Whether you are a mountain bike or road bike rider, it is a good idea to carry a spare tube with you in case you have a flat tyre.</p>
                <p>The type of tube you choose needs to fit both your wheels and your tyres. If you ride a road bike, the most common type of circumference size is 700c. However, the depth of the rims varies greatly typically ranging between 25mm to 60mm.  The length of the valve must be longer than the depth of your rim by at least 20mm.</p>
                <p>Another important consideration is the type of valve. There are two types of tube valves, namely ‘Presta’ and ‘Schrader’.  ‘Presta’ valves are more commonly found on road bikes.</p>
                <p>At last, you must choose a tube width that fits your tyre width.  If you look on the side of your tyre, you will be able to find the size and width of the tyre.  Check out the video for more tips on choosing inner tubes.</p>
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
