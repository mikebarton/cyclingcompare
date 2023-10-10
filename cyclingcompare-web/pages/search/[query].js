import { useRouter } from 'next/router'
import Head from 'next/head'
import Layout from '../../components/layouts/layout'
import PageSubLayout from '../../components/layouts/pageSubLayout'
import FilterVerticalPane from '../../components/filters/filterVerticalPane'
import ProductGrid from '../../components/productSummaries/productGrid'
import * as styles from './search.module.scss'
import Config from '../../config';
import { useEffect } from 'react'
import * as ga from '../../lib/ga'
import BannerDisplay from '../../components/adbanners/BannerDisplay'

export default function Search({ categories, products, searchTerm }) {
  useEffect(()=>{
    ga.event({
      action: "search",
      params : {
        search_term: searchTerm
      }
    })
  },[searchTerm])
  
  const router = useRouter()
  const cats = router.isFallback ? 
                [{ title:'loading categories...', urlSlug: 'loading categories...', categoryBannerImage: ''}] : 
                categories;
  
  function VertBanner(){
    return (
      <div className={styles.vertBannerCrop}>
          <BannerDisplay width={160} height={600}/>
          </div>
    )
  }

  return (
    <Layout categories={cats}>
      <Head>
        <title>Cycling Compare - {searchTerm}</title>
      </Head>
      <PageSubLayout categories={cats} leftPane={<VertBanner />}>
      
        <div className={styles.mainContainer}>
          <div className={styles.bannerCrop}>
          <BannerDisplay width={728} height={90} />
          </div>
          <h1>Search Results</h1>
          {router.isFallback ? <div>Loading...</div>
           : products && products.length > 0 ?<ProductGrid products={products}/> : <p>Sorry, We have no products that match that search query</p>}
          
        </div>
      </PageSubLayout>
    </Layout>
  )
}

export async function getStaticPaths(context) {    
  return {
    paths: [],
    fallback: true
  };
}

export async function getStaticProps(context) { 
  
  var keywords = context.params.query;

  const res = await fetch(Config.ApiGateway.HostName + Config.ApiGateway.GetAllCategoriesPath);
  const categories = await res.json();  
  
  var products = []  
  var searchUrl = Config.ApiGateway.HostName + Config.ApiGateway.SearchByKeywordPath + keywords;
  var encodedUrl = encodeURI(searchUrl); 

  const productsRes = await fetch(encodedUrl)
  products = await productsRes.json()


  if (!categories || !products) {
    return { notFound: true }
  }

  return {
    props: { categories: categories, products: products, searchTerm: keywords },
    revalidate: 1800
  }
}