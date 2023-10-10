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
            <h1>How to Choose a Road Bike Saddle</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='saddle' className={styles.video}><iframe src="https://www.youtube.com/embed/Psxa2kcGMp4" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>

          <p>There are lots of shapes, sizes and options when it comes down to choosing saddles. Two of the main considerations when choosing a saddle is to improve comfort and increase performance.</p>
                <p>Wide saddles with padding are typically used for leisure and town bikes. This type of saddle fits these bikes better as the upright position of the rider creates increased pressure on the sit bones. Conversely, road bike saddles do not require as much padding as the sitting angle of the rider puts less pressure on the sit bones.</p>
                <p>Comfort is mostly dictated by the shape of the saddle and not so much by the amount of padding. Other important consideration is the width of the saddle, which are typically designed for different sit bone widths.</p>
                <p>We highly recommend that you try a few saddles at your local bike shop to ensure that you pick one that fits not only your type of bike but also your own body. In the end, choosing the wrong saddle can make your riding experience a complete torture.</p>
                <p>The following video shows other great tips to help you choosing a saddle that not only provides comfort but increases your riding performance.</p>
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
