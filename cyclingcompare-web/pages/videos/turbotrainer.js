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
            <h1>How to Choose a Turbo Trainer</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='trainer' className={styles.video}><iframe  src="https://www.youtube.com/embed/ulhpwTRvAfo" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>
          <p>Turbo trainers are also called indoor trainers, ergo trainers or static trainers. They are a great way to keep fit if the weather doesn’t allow you to ride outside or you simply want to do a quick training session in the mornings or the evenings. Turbo trainers have become incredibly popular during COVID-19 due to the lockdown restrictions that keep changing on a regular basis.</p>
                <p>Magnetic Trainers are the entry level trainers and, as their name suggests, they get their resistance at the back using magnets. Difficulty is typically adjusted manually. You may consider these type if you are on a budget or if you don’t believe you will be using them that often.</p>
                <p>Fluid Trainers are the next level of trainers and they get their resistance from fluid at the back roller. This type provides a more realistic road feel in comparison to the magnetic trainer and are also much quieter.</p>
                <p>The third type are the Direct Drive Trainers. In comparison to the magnetic and fluid trainers, you cannot mount your bike and rear wheel directly but instead, you need to take off your rear wheel and mount your frame on the trainer where you need to fit your chain around a cassette. The main advantage of these is the stability they provide, quiet operation and even better road feel.</p>
                <p>Please check out the video on this page for additional tips and guidance on choosing a turbo trainer.</p>
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
