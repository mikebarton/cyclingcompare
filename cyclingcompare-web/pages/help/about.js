import * as styles from './about.module.scss'
import Head from 'next/head'
import Layout from '../../components/layouts/layout'
import PageSubLayout from '../../components/layouts/pageSubLayout'
import Config from '../../config'

export default function Faq ({ categories }) {
  return (
    <Layout categories={categories}>
      <Head><title>Cycling Compare - About Us</title></Head>
      <PageSubLayout categories={[]} leftPane={[]} hideLeftPane>
        <div className={styles.about}>
          <h1>About Us</h1>
          <p>
            Founded in 2021, CyclingCompare.com.au is the first dedicated
            cycling price comparison site in Australia. It compares the prices
            from the biggest merchants in Australia and abroad to find the best
            possible value for its customers.
          </p>

          <p>
            Cycling is not only a sport to us, it is also our passion. We love
            the adventure, the adrenaline and the friendship we find in cycling.
            That's why we want to help everyone access the best cycling
            products at the best prices available.
          </p>

          <p>
            We work together with high quality and reliable retailers to allow
            our customers to search for great deals from the best brands.
          </p>

          <p>
          We currently work with 12 merchants and are constantly looking to expand our website to improve our service and offer better prices to our customers.
          </p>

          <p>
            If you are interested in advertising with us and joining our group of
            merchants please contact us at admin@cyclingcompare.com.au
          </p>
        </div>
      </PageSubLayout>
    </Layout>
  )
}

export async function getStaticProps() {
  
  const res = await fetch(
    Config.ApiGateway.HostName + Config.ApiGateway.GetAllCategoriesPath
  )
  const categories = await res.json()

  if (!categories) {
    return { notFound: true }
  }

  return {
    props: { categories: categories }
  }
}


