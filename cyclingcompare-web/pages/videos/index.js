import WidePageLayout from '../../components/layouts/widePageLayout'
import Link from 'next/link'
import Layout from '../../components/layouts/layout'
import Config from '../../config'
import * as styles from './videos.module.scss'
import BannerDisplay from '../../components/adbanners/BannerDisplay'

function Videos ({ categories }) {
  return (
    <Layout categories={categories}>
      <WidePageLayout>
        <BannerDisplay width={728} height={90} />

        <div className={styles.videoPage}>
          <div>
            <h1>Guides to Bike Components and Accessories</h1>
          </div>
          <p>
            We would like to thank our friends from GCN (Global Cycling Network)
            for letting us show their awesome videos in our website. We
            recommend you visit their YouTube channel for many more useful
            videos.
          </p>
          <p>
            Below you can find great guides for general bike maintenance,
            reviews and how to choose bicycle components.
          </p>
        </div>

        <div className={styles.videoGrid}>
          <Link href={'/videos/firstbike'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/firstbike.jpg'} />
              </div>
              <h2>How to Buy Your First Bike</h2>
              <p>
                If you are looking to buy your first road bike, one of the main
                considerations is your budget. Lots of people forget that there
                are some added costs such as pedals, water bottle, bottle cage,
                helmet and clothes which are typically in addition to your bike
                cost, so please don’t forget to make an allowance to cover
                these.
              </p>
            </div>
          </Link>

          <Link href={'/videos/bikelights'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/bikelights.jpg'} />
              </div>
              <h2>Guide to Bike Lights</h2>
              <p>
                In Australia, bicycle riders must use a blinking or steady red
                rear light in conjunction with a flashing or steady white front
                light when riding at night or in low-light conditions.
              </p>
            </div>
          </Link>

          <Link href={'/videos/bikesaddle'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/bikesaddle.jpg'} />
              </div>
              <h2>How to Choose a Road Bike Saddle</h2>
              <p>
                There are lots of shapes, sizes and options when it comes down
                to choosing saddles. Two of the main considerations when
                choosing a saddle is to improve comfort and increase performance
              </p>
            </div>
          </Link>

          <Link href={'/videos/bikeshoes'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/cyclingshoes.jpg'} />
              </div>
              <h2>How to Choose the Right Cycling Shoes</h2>
              <p>
                Buying a new pair of cycling shoes is always an exciting
                purchase. Type, material, fit and look are many of the
                considerations when picking a new pair of shoes.
              </p>
            </div>
          </Link>

          <Link href={'/videos/bikehelmet'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/bikehelmet.jpg'} />
              </div>
              <h2>How to Choose a Cycle Helmet</h2>
              <p>
                In Australia it is compulsory to hear an approved helmet when
                cycling. Needless to say, wearing a helmet is fundamental to
                improve safety of bike riders.
              </p>
            </div>
          </Link>

          <Link href={'/videos/turbotrainer'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/turbotrainer.jpg'} />
              </div>
              <h2>How to choose a turbo trainer</h2>
              <p>Turbo trainers are also called indoor trainers, ergo trainers or static trainers. They are a great way to keep fit if the weather doesn’t allow you to ride outside or you simply want to do a quick training session in the mornings or the evenings. Turbo trainers have become incredibly popular during COVID-19 due to the lockdown restrictions that keep changing on a regular basis
              </p>
            </div>
          </Link>

          <Link href={'/videos/innertubes'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/innertubes.jpg'} />
              </div>
              <h2>How to choose inner tubes</h2>
              <p>Unless you are running tubeless wheels, you are most likely going to need a spare tubes for your bike. Whether you are a mountain bike or road bike rider, it is a good idea to carry a spare tube with you in case you have a flat tyre.
              </p>
            </div>
          </Link>

          <Link href={'/videos/chainlube'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/chainlube.jpg'} />
              </div>
              <h2>How to Select Chain Lube for Cycling</h2>
              <p>Choosing the right type of lube will help you extending the life of your chain, improve your shifting and increase your performance. The two most common types of lubes are ‘Wet’ and ‘Dry’ lubes
              </p>
            </div>
          </Link>

          <Link href={'/videos/bikegeometry'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/bikegeometry.jpg'} />
              </div>
              <h2>Road Bike Geometry Explained</h2>
              <p>Road bikes have different geometry parameters that have a significant impact on your ride
              </p>
            </div>
          </Link>

          <Link href={'/videos/changepedals'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/changepedals.jpg'} />
              </div>
              <h2>How to Change Pedals</h2>
              <p>While changing pedals may sound like an easy task, it is easy to get confused as the pedals are threaded differently on each side of the bike. This is to make sure that the pedals don’t become so tight as you pedal that it would be impossible to remove them afterwards.
              </p>
            </div>
          </Link>

          <Link href={'/videos/serviceshifters'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/serviceshifters.jpg'} />
              </div>
              <h2>How to Service Your Shifters</h2>
              <p>Over time, shifters can get dirty with grit, dust and crusty grease, therefore affecting gear shifting. Servicing your shifters is an easy task that only requires you to clean all the dirt. We recommend you use a spray degreaser to soften all that grease and help flushing out any stuck grit
              </p>
            </div>
          </Link>

          <Link href={'/videos/gearcables'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/gearcables.jpg'} />
              </div>
              <h2>How to Change Your Gear Cables</h2>
              <p>If you are a regular rider, you may have experienced the frustration and discomfort of stuck gears or unresponsive shifting. A couple of the reasons for this can be due to worn cables or dirt accumulating inside the cable casing. Fortunately, changing your gear cables is an easy task that doesn’t require many tools.
              </p>
            </div>
          </Link>

          <Link href={'/videos/adustderailleur'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/adustderailleur.jpg'} />
              </div>
              <h2>How to Adjust Your Front Derailleur</h2>
              <p>If your chain constantly falls off when changing gears or you hear a rubbing noise on your chain, most likely you need to adjust your front derailleur. To complete this job, all you need is an allen key (5mm) and a phillips screwdriver.
              </p>
            </div>
          </Link>

          <Link href={'/videos/creakybracket'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/creakybracket.jpg'} />
              </div>
              <h2>How to Fix a Creaking Bottom Bracket or Cranks</h2>
              <p>Riding your bike should always be an enjoyable experience and having creaking noises coming from your drivetrain can be quite unpleasant. Servicing your cranks or bottom bracket is a time-consuming exercise, so we recommend you checking other potential sources of squeaking noises (e.g. pedals, chain, shoe cleats, chainring bolts) before you take down your cranks and bottom bracket
              </p>
            </div>
          </Link>

          <Link href={'/videos/serviceheadset'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/serviceheadset.jpg'} />
              </div>
              <h2>How to Service your Headset</h2>
              <p>Just like most parts of your bike, the headset typically accumulates dirt, grit and water which, in time, will make the steering of your bike a bit less smooth and noisy.
              </p>
            </div>
          </Link>

          <Link href={'/videos/bikedegrease'}>
            <div className={styles.videoSummary}>
              <div className={styles.previewImage}>
                <img src={'/images/videos/bikedegrease.jpg'} />
              </div>
              <h2>Clean and Degrease your Bike in 30 mins</h2>
              <p>Cleaning your bike regularly is a good way to extend the life of its components and maintain good performance overall. To clean and degrease your bike you will need a bucket of hot water, degreaser spray, a flat head screwdriver or wooden stick, dishwasher liquid, a sponge and an old rag.
              </p>
            </div>
          </Link>

        </div>

        <div></div>
      </WidePageLayout>
    </Layout>
  )
}

export default Videos

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
