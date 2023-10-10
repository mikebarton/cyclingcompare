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
            <h1>Road Bike Geometry Explained</h1>
          </div>
          <div className={styles.videoContent}>            
          <div id='geometry' className={styles.video}><iframe src="https://www.youtube.com/embed/wfx3LqsCFSA?list=PLUdAMlZtaV1333Cy1QnIZwqDXj1q0Ooyy" frameborder="0" allowfullscreen></iframe></div>
          <p>Road bikes have different geometry parameters that have a significant impact on your ride. Choosing the right geometry will allow you achieving the following:</p>
                  <ul><li>Make your ride safer by improving your control and bike handling</li>
                  <li>Increased performance from better engagement of leg muscles</li>
                  <li>Reduced wind resistance resulting from a more aerodynamic riding position</li>
                  <li>Improved comfort from a better riding position</li></ul>
              <p>The angles and distances between key points on your bike like wheels, bottom bracket and head tube vary greatly between brands and models.  To achieve all the benefits listed above, we recommend you get a bike fitting that matches the geometry of your bike to your own body. </p>
              <p>Two of the key numbers to look out for when you are choosing a new bike are the reach and the stack. Reach is the horizontal distance from the centre of the bottom bracket to the middle of the head tube. Stack is the vertical distance from the centre of the bottom bracket to the mid-point at the head tube.  The reason they are so important is because of the difficulty adjusting the position of your handlebars, which can significantly affect the way your bike handles.</p>
              <p>The way your bike handles is governed by 4 key measurements namely trail, chainstay length, bottom bracket drop and stem length and bar reach.  Please watch the video on this page for further details and a more in-depth explanation of bike geometry.</p>
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
