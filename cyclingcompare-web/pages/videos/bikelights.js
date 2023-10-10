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
            <h1>Guide to Bike Lights</h1>
          </div>
          <div className={styles.videoContent}>            
                  <div id='lights' className={styles.video}>
                    <iframe src="https://www.youtube.com/embed/w4d5O4O8TxU" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                  </div>

                  <p>In Australia, bicycle riders must use a blinking or steady red rear light in conjunction with a flashing or steady white front light when riding at night or in low-light conditions.</p>
                  <p>A good pair of lights will greatly improve your safety as it will increase your visibility to car drivers. The level of luminosity of your lights is measured in lumens. The higher the number of lumens, the brighter the lights.</p>
                  <p>When buying bike lights, it is important to consider whether you will use them for urban lit roads or unlit rural roads. The latter may require a brighter set of bike lights with a recommended 800 lumens at least. Most bike lights come with different settings which allow riders to reduce or increase the brightness of their lights and set their lighting from steady to blinking mode.</p>
                  <p>Some of the new rear lights in the market even come with indicator lights that enable bike riders to indicate if they will be turning right or left. Whichever lights you choose, we highly recommend to buy USB rechargeable lights to save you the ongoing costs of replacing batteries.</p>
                  <p>The video in this page provides additional guidance on the selection of bike lights.</p>
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
