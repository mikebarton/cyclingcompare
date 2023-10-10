import * as styles from './faq.module.scss'
import Head from 'next/head'
import Layout from '../../components/layouts/layout'
import PageSubLayout from '../../components/layouts/pageSubLayout'
import Config from '../../config'

export default function Faq ({ categories }) {
  return (
    <Layout categories={categories}>
      <Head><title>Cycling Compare - FAQ</title></Head>
      <PageSubLayout categories={[]} leftPane={[]} hideLeftPane>
        <div className={styles.faq}>
          <h1>FAQs</h1>

          <h2>
            HOW MANY MERCHANTS DOES CYCLINGCOMPARE.COM.AU USE FOR THEIR PRICE
            COMPARISON?
          </h2>

          <p>
            We compare the prices of 12 different cycling merchants. Some of
            these are based abroad, mainly in the UK, and many others are
            Australian based.
          </p>

          <h2>IS IT CHEAPER TO BUY FROM AUSTRALIAN BASED MERCHANTS?</h2>

          <p>
            CyclingCompare.com.au compares the prices of all our merchants to
            find the best price available at the time of search. Sometimes
            foreign-based merchants run special deals that offer better value
            than Australian based ones and vice-versa.
          </p>

          <h2>
            ARE THE SHIPPING COSTS OF FOREIGN BASED MERCHANTS MUCH HIGHER THAN
            AUSTRALIAN BASED ONES?
          </h2>

          <p>
            Most merchants have free delivery when the cost of the purchase is
            above a specified threshold, which varies for each company. Below
            that threshold, most companies charge very similar delivery costs,
            regardless whether they are foreign or Australian based. We
            recommend you to confirm the delivery costs with each individual
            merchant.
          </p>

          <h2>WHAT SHOULD I DO TO RETURN AN ITEM?</h2>

          <p>
            Please visit directly the site of the merchant you purchased from
            for specific instructions on returns and refund policies.
          </p>

          <h2>DOES CYCLINGCOMPARE.COM.AU SELL PRODUCTS DIRECTLY?</h2>

          <p>
            No. CyclingCompare.com.au provides the service of price comparison
            only between all merchants, to find the best price possible for your
            chosen product at the time of the search. Purchases are made
            directly with the merchants.
          </p>

          <h2>
            ARE THE PRICES SHOWN IN CYCLINGCOMPARE.COM.AU SIMILAR TO THOSE
            OFFERED DIRECTLY BY THE MERCHANTS?
          </h2>

          <p>
            We update our products lists and prices on a daily basis
            consistently with the latest products’ list provided by each
            individual merchant.
          </p>

          <h2>HOW DOES CYCLINGCOMPARE.COM.AU MAKE A PROFIT?</h2>

          <p>
            We earn a commission on the purchases made by people who follow our
            product links. We are a completely independent company without any
            association to any merchants and, therefore, we are unbiased. Our
            mission is to help our customers find the best possible price for
            their cycling products.
          </p>

          <h2>HOW CAN I PROVIDE FEEDBACK ON A MERCHANT OR SPECIFIC PRODUCT?</h2>

          <p>
            If you have any positive or negative feedback on a specific Merchant
            or Product, please write to us (admin@cyclingcompare.com.au) with
            your review comments. We are committed to work only with good
            companies that can deliver a great service and would be happy to
            discuss your comments directly with the relevant Merchant
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

