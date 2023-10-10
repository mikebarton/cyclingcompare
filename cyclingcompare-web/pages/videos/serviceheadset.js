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
            <h1>How to Service Your Headset</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='headset' className={styles.video}><iframe  src="https://www.youtube.com/embed/pARonM0tFpM" frameborder="0" allowfullscreen></iframe></div>
          <p>Just like most parts of your bike, the headset typically accumulates dirt, grit and water which, in time, will make the steering of your bike a bit less smooth and noisy.</p>
                <p>Most bikes have two sets of sealed bearings at the top and bottom of the head tube. Some of the tools you will need to service your headset include:</p>
                <ul><li>Headset wrenches (32 or 36mm) – these are required in case you have a threaded headset. These are more common in older bikes.</li>
                <li>4, 5 and 6mm Allen keys – needed to loosen the bolt at the top of your head tube cap. Most new bikes have threadless headsets or Aheadsets.</li>
                <li>Rubber hammer – you may need a rubber hammer to push your head tube out in case it is stuck.</li>
                <li>Grease and degreaser – you will need to give a thorough clean to your bearings and headset and regrease them in order to waterproof them again.</li>
                <li>Torque wrench</li></ul>
                <p>The video in this page explains how to service your headset step by step plus it provides some useful tips to make sure you avoid common beginner mistakes.</p>
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
