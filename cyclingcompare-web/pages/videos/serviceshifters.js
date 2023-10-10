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
            <h1>How to Service Your Shifters</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='serviceShifters' className={styles.video}><iframe  src="https://www.youtube.com/embed/nwJhi7F671U?list=PLUdAMlZtaV1333Cy1QnIZwqDXj1q0Ooyy" frameborder="0" allowfullscreen></iframe></div>
          <p>Over time, shifters can get dirty with grit, dust and crusty grease, therefore affecting gear shifting. Servicing your shifters is an easy task that only requires you to clean all the dirt. We recommend you use a spray degreaser to soften all that grease and help flushing out any stuck grit.</p>
                <p>First you need to remove the lever hood to let you reach the mechanism of your shifters. Then, spray the mechanism as necessary to flush out all the dirt and grease, wiping out any excess degreaser until the internals look clean.</p>
                <p>Finally, spray some light lube and add a bit of grease to weatherproof the shifters. For more tips, please watch the video on this page.</p>
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
