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
            <h1>How to Select Chain Lube for Cycling</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='lube' className={styles.video}><iframe  src="https://www.youtube.com/embed/C4K90m3sygo" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>
          <p>Choosing the right type of lube will help you extending the life of your chain, improve your shifting and increase your performance.  The two most common types of lubes are ‘Wet’ and ‘Dry’ lubes.</p>
                <p>Wet lubes are thicker than dry lubes and are best used for rainy, gritty or muddy conditions. It is however a bit harder to clear in comparison to the dry lube.</p>
                <p>For dryer conditions, dry lubes are a good choice as they are much lighter and thinner.  However, as they are thinner, it also means that you will need to reapply them more often than the wet lubes.</p>
                <p>The video on this page shows some extra tips on choosing the right type of lube for your chain.</p>
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
