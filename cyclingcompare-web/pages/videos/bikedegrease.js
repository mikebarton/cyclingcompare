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
            <h1>Clean and Degrease Your Bike – 30 minute bike wash</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='degrease' className={styles.video}><iframe src="https://www.youtube.com/embed/5ak4AzlUz5Q" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>            
            <p>Cleaning your bike regularly is a good way to extend the life of its components and maintain good performance overall. To clean and degrease your bike you will need a bucket of hot water, degreaser spray, a flat head screwdriver or wooden stick, dishwasher liquid, a sponge and an old rag.</p>
            <p>The first step is to spray your chain, derailleurs (front and rear), chainrings and rear cassette with the degreaser and let it penetrate for about 5 minutes. Use the screwdriver or wooden stick to remove any grease from the jockey wheels on your rear derailleur. The hot water and dishwasher liquid will help you softening and removing stuck grit and grease on your bike.</p>
            <p>Soak the sponge on the soapy water and start cleaning your bike from top to bottom thoroughly. To clean the drivetrain, we recommend you using an older sponge or an old rag. Take off the wheels and clean them with the sponge making sure you reach all spots (e.g. hub and spokes).</p>
            <p>Rinse your bike with clean water or gently spray it with the hose to remove all soap and dirt. After rinsing it, dry the whole bike to avoid any water marks.</p>
            <p>Finally, lube your chain and dry any excess lube as necessary. Please watch the video on this page for great tips on cleaning your bike.</p>
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
