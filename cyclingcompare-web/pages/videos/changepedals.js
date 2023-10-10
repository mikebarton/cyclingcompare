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
            <h1>How to Change Pedals</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='checks' className={styles.video}><iframe  src="https://www.youtube.com/embed/3nFaqUiyPQs?list=PLUdAMlZtaV1333Cy1QnIZwqDXj1q0Ooyy" frameborder="0" allowfullscreen></iframe></div>
          <p>While changing pedals may sound like an easy task, it is easy to get confused as the pedals are threaded differently on each side of the bike. This is to make sure that the pedals don’t become so tight as you pedal that it would be impossible to remove them afterwards. </p>
                <p>Some of the tools required to change pedals include allen key (8mm typically, sometimes it is a 6mm) and a 50mm wrench or spanner. There are special wrenches for changing pedals which are long and thinner and especially designed to allow you fitting the external spindle flats between the pedal and the crank leg.</p>
                <p>The non-drive side crank thread goes clockwise and the drive side thread goes anticlockwise.  When changing pedals, make sure that the crank is facing down.  This will make it easier to push the allen key down when trying the loose the pedals.</p>
                <p>Before replacing the pedals, clean the threads thoroughly and apply a good quality grease on the threads.  For more tips on changing pedals, please watch the video on this page.</p>
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
