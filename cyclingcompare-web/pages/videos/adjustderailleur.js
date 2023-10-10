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
            <h1>How to Adjust Your Front Derailleur</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='derailleur' className={styles.video}><iframe src="https://www.youtube.com/embed/Ea03ChN-7Vg" frameborder="0" allowfullscreen></iframe></div>
                <p>If your chain constantly falls off when changing gears or you hear a rubbing noise on your chain, most likely you need to adjust your front derailleur. To complete this job, all you need is an allen key (5mm) and a phillips screwdriver.</p>
                <p>The adjustment of the front derailleur is done via two limit screws that control how far your derailleur moves on each direction (i.e. inside to shift into to lower gears and outside for higher gears). To adjust the inside screw, place your gears on the smaller chain ring and the biggest cog on your cassette. Then turn the screw so the inside of the derailleur sits about 1mm from the chain. To adjust the outside screw, place your gears on the big chain ring and small cog on your cassette and turn the screw so the derailleur sits also about 1mm from the outside side of the chain.</p>
                <p>Another important factor to consider when indexing your derailleur is the tension on your cable. The latter is very important as it controls the movement of your derailleur.</p>
                <p>Before loosening the bolt holding the cable to try to alter the tension, try adjusting it using the small tension barrel. Most of the time this is sufficient and saves some time and effort, however if it doesn’t work, you will need to loosen the bolt and pull the cable by hand to adjust the tension.</p>
                <p>In addition to recalibrating your limit screws and checking the cable tension, you should also check the position of the derailleur. This is also critical as the set-up height and angle impact on the way the derailleur works and avoids the potential for chain dropping. As a rough guide, the edge of the derailleur should sit approximately 2mm above the top of the teeth of your chainring.</p>
                <p>Indexing the derailleur can be a tricky and iterative process as there as there are other potential factors affecting your shifting (e.g. worn cables). For a more detailed guide on adjusting your front derailleur and some great tips, please watch the video on this page.</p>
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
