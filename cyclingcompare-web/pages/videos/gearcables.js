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
            <h1>How to Change Your Gear Cables</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='cables' className={styles.video}><iframe  src="https://www.youtube.com/embed/-7ea5tpiM7I" frameborder="0" allowfullscreen></iframe></div>
          <p>If you are a regular rider, you may have experienced the frustration and discomfort of stuck gears or unresponsive shifting. A couple of the reasons for this can be due to worn cables or dirt accumulating inside the cable casing. Fortunately, changing your gear cables is an easy task that doesn’t require many tools.</p>
                <p>To change your cables, you will need an allen key and a good set of cable cutters. Firstly, you will need to remove the old cables and to do this, you must shift the gears to the smaller rings to reduce the tension on the cable. Then, loosen the bolt that holds the cable and cut the end of the cable so you can remove it easily.</p>
                <p>To remove the cables you will also need to remove the cable casing which, in many cases, runs under the bar tape. Sometimes the casing can accumulate dirt, grit and water. This can create friction on the cables and affect the gear shifting. We recommend you also change the casing if you are changing your cables.</p>
                <p>When pulling the cable out through the shifter, take note of where the cable is coming out so you can run the new cable in afterwards.</p>
                <p>Measure the new cable and cut it to match the length of the old one. Run in the cable through the shifter, run it through the new casing and connect it back to the derailleur. Lastly, adjust the tension of the cable and test the shifters. For a more detailed guide on changing your gear cables and some great tips, please watch the video on this page.</p>
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
