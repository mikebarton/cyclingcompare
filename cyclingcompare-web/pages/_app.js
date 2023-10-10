// import '../styles/globals.css'
import '../styles/main.scss'
import { useEffect } from 'react'
import { useRouter } from 'next/router'
import Head from 'next/head'
require('../helpers/stringFormat')

import * as ga from '../lib/ga'

function MyApp ({ Component, pageProps }) {
  const router = useRouter()

  useEffect(() => {
    const handleRouteChange = url => {
      ga.pageview(url)
    }

    router.events.on('routeChangeComplete', handleRouteChange)

    return () => {
      router.events.off('routeChangeComplete', handleRouteChange)
    }
  }, [router.events])

  return (
    <>
      <Head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
      </Head>
      <Component {...pageProps} />
    </>
  )
}

export default MyApp
