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
            <h1>How to Fix a Creaking Bottom Bracket or Cranks</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='creakingcranks' className={styles.video}><iframe src="https://www.youtube.com/embed/ZEiIFbIklJg" frameborder="0" allowfullscreen></iframe></div>
          <p>Riding your bike should always be an enjoyable experience and having creaking noises coming from your drivetrain can be quite unpleasant. Servicing your cranks or bottom bracket is a time-consuming exercise, so we recommend you checking other potential sources of squeaking noises (e.g. pedals, chain, shoe cleats, chainring bolts) before you take down your cranks and bottom bracket.</p>
                <p>Many of the creaking noises can be solved by applying lube or grease to key components and connections to reduce friction. If you have checked, cleaned, lubed and greased all other drivetrain components and the noise still hasn’t gone away, you will need to remove the bottom bracket and bearings, clean them and grease them.</p>
                <p>There are several types of cranks and bottom brackets, so please make sure that you follow the guidance from your bike manufacturer. Different types of bottom bracket include threaded and press fit. You need to make sure that you have the right tools to remove and re-fit your individual bottom bracket before you start removing all other components.</p>
                <p>The video on this page shows not only how to maintain your bottom bracket and cranks, but also shows some great tips to maintain other drivetrain components.</p>
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
