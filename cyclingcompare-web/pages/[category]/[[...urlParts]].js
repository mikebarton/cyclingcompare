import { useEffect, useState } from 'react'
import Head from 'next/head'
import { useRouter } from 'next/router'
import Layout from '../../components/layouts/layout'
import PageSubLayout from '../../components/layouts/pageSubLayout'
import FilterVerticalPane from '../../components/filters/filterVerticalPane'
import MobileFilterPane from '../../components/filters/filterMobilePane'
import ProductGrid from '../../components/productSummaries/productGrid'
import Sorter from '../../components/pagination/sorter'
import BannerDisplay from '../../components/adbanners/BannerDisplay'
import Paginator from '../../components/pagination/paginator'
import * as styles from './category.module.scss'
import Config from '../../config'

export default function Category ({ selectedCategory, categories, products, filters, pageData }) {  
  var [showMobileFilters, setShowMobileFilters] = useState(false)  
  var [productsToShow, setProductsToShow] = useState([])
  var [totalProductCount, setTotalProductCount] = useState()
  var [pageSize, setPageSize] = useState()
  var [pageNum, setPageNum] = useState()
  var [sortOrder, setSortOrder] = useState()
  var [storedFilters, setStoredFilters] = useState(filters)
  var [isFiltered, setIsFiltered] = useState(false)


  useEffect(()=>{
    if(storedFilters )
      handleFiltersUpdated(storedFilters, pageSize, pageNum, sortOrder);
      var somethingIsFiltered = storedFilters.some(x=>{
        return x.filterType == 0 ? (x.minValue || x.maxValue) : Object.keys(x.filterOptions).some(y=>x.filterOptions[y])
      });
      setIsFiltered(somethingIsFiltered);
  }, [storedFilters, sortOrder])

  useEffect(()=>{
    if(products){
      setProductsToShow(products);
      setTotalProductCount(pageData.totalCount);
      setPageNum(pageData.pageNum);
      setPageSize(pageData.pageSize);
      setSortOrder(pageData.sortOrder);
    }
  }, [products])

  function clearFilters(){
    var clearedFilters = storedFilters.map(x=> x.filterType === 0 ? 
      {...x, minValue: null, maxValue: null} : 
      {...x, filterOptions: Object.keys(x.filterOptions).reduce((o, key)=> ({...o, [key]: false}), {})})
      setStoredFilters(clearedFilters);
  }
  
  function handleFiltersUpdated (updatedFilters, requestedPageSize, requestedPageNum, requestedSortOrder) {
    var url = Config.ApiGateway.HostName + Config.ApiGateway.GetFilteredProducts.format(requestedPageSize || 30, requestedPageNum || 0, requestedSortOrder || 0, selectedCategory.categoryId);
    var requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(updatedFilters)
    }

    fetch(url, requestOptions)
      .then(res => res.json())
      .then(res =>{ 
        setTotalProductCount(res.totalCount);
        setPageSize(res.pageSize);
        setPageNum(res.pageNum);
        setSortOrder(res.sortOrder);
        setProductsToShow(res.products);
      })
  }
  

  const router = useRouter();
  if (router.isFallback) {
    return <div>Loading...</div>
  }
  
  var pageActions = {
    nextPage: ()=> handleFiltersUpdated(storedFilters, pageSize, pageNum+1, sortOrder),
    prevPage: ()=> handleFiltersUpdated(storedFilters, pageSize, pageNum-1, sortOrder),
    thirtyItems: ()=> handleFiltersUpdated(storedFilters, 30, 0, sortOrder),
    sixtyItems: ()=> handleFiltersUpdated(storedFilters, 60, 0, sortOrder),
    ninetyItems: ()=> handleFiltersUpdated(storedFilters, 90, 0, sortOrder)
  }

  return (
    <Layout categories={categories}>
      <Head>
        <title>Cycling Compare - {selectedCategory.title}</title>
      </Head>
      <PageSubLayout categories={categories} leftPane={
          <FilterVerticalPane
            filters={storedFilters}
            onFiltersUpdated={f => setStoredFilters(f)}
          />
        }
      >
        <div className={styles.pageRow}>
          <div>
            {showMobileFilters && (
              <MobileFilterPane
                filters={storedFilters}
                onFiltersUpdated={f => setStoredFilters(f)}
                onCloseRequest={() => setShowMobileFilters(false)}
              />
            )}
          </div>
            <div className={styles.mainContainer}>
              <div className={styles.bannerContainer}>
                <BannerDisplay width={728} height={90} />
              </div>
              <div className={styles.titleRow}>
                <h1>{selectedCategory.title}</h1>
                <div className={styles.titleWidgets}>
                  <Sorter onSortOrderUpdated={(so)=> setSortOrder(so)} />
                  <div className={styles.filterIcon} onClick={() => setShowMobileFilters(true)} >
                    <img src={'/icons/filter.svg'} />
                  </div>
                </div>
              </div>

              <div className={styles.paginationContainer}><Paginator totalProducts={totalProductCount} pageActions={pageActions} pageSize={pageSize} pageNum={pageNum}/></div>
              <ProductGrid onClearFiltersRequested={()=> clearFilters()} isFiltered={isFiltered} category={selectedCategory} products={productsToShow || []} />
              <div className={styles.paginationContainer}><Paginator totalProducts={totalProductCount} pageActions={pageActions} pageSize={pageSize} pageNum={pageNum}/></div>
            </div>
        </div>
      </PageSubLayout>
    </Layout>
  )
}

export async function getStaticPaths () {
  const res = await fetch(Config.ApiGateway.HostName + Config.ApiGateway.GetAllCategoriesPath)
  const categories = await res.json()

  var catPaths = categories.map(x => {
    return { params: { category: x.urlSlug, urlParts: [] } }
  })

  var reducer = (acc, currentVal)=>{
    var paths = currentVal.children.map(x=> {
      return { params : { urlParts :  [x.urlSlug], category: currentVal.urlSlug }}
    });
    var combined = [...acc, ...paths];
    return combined;
  }
  
  var subCatPaths = categories.reduce(reducer, []);

  var paths = [...catPaths, ...subCatPaths]

  return {
    paths: paths,
    fallback: true
  }
}

export async function getStaticProps ({ params }) {
  const res = await fetch(Config.ApiGateway.HostName + Config.ApiGateway.GetAllCategoriesPath)
  const categories = await res.json()
  var categorySlug = params.category;
  var [subCategorySlug] = (params.urlParts || [])
  var category = categories.find(x=> x.urlSlug === categorySlug);
  var categoryToShow = subCategorySlug ? category.children.find(x=>x.urlSlug === subCategorySlug) : category;
  

  var productQueryPath = Config.ApiGateway.GetPagedProductsByCategoryId.format('30', '0', '0', categoryToShow.categoryId)
  const productsRes = await fetch(Config.ApiGateway.HostName + productQueryPath)
  const productData = await productsRes.json()
  var products = productData.products;
  

  const filtersRes = await fetch(Config.ApiGateway.HostName + Config.ApiGateway.GetProductFilters + '?categoryId=' + categoryToShow.categoryId)
  const productFilters = await filtersRes.json()

  if (!categories || !products) {
    return { notFound: true }
  }

  return {
    props: {
      selectedCategory: categoryToShow,
      
      categories: categories,
      products: products || [],
      filters: productFilters,
      pageData: { pageSize: 30, pageNum: 0, sortOrder: 0, totalCount: productData.totalCount}
    },
    revalidate: 1800
  }
}
