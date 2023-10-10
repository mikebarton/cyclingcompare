import Document, { Html, Head, Main, NextScript } from 'next/document'
import Config from '../config'

class MyDocument extends Document {
  static async getInitialProps(ctx) {
    const initialProps = await Document.getInitialProps(ctx)
    return { ...initialProps }
  }

  render() {
    return (
      <Html>
        <Head>
        <meta name='ir-site-verification-token' value='1814091725' />
        <meta name="commission-factory-verification" content="c958a4a43f7a44438e1dbf26d9862c96" />
        <script data-ad-client="ca-pub-6563884332573677" async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
        <script
            async
            src={`https://www.googletagmanager.com/gtag/js?id=${Config.GoogleAnalyticsId}`}
          />

<script
            dangerouslySetInnerHTML={{
              __html: `
            window.dataLayer = window.dataLayer || [];
            function gtag(){dataLayer.push(arguments);}
            gtag('js', new Date());
            gtag('config', '${Config.GoogleAnalyticsId}', {
              page_path: window.location.pathname,
            });
          `,
            }}
          />
          <link rel="icon" href="/logos/site/favicon.png" />
          <meta property="og:type" content="website" />
          <meta property="og:locale" content="en_AU" />
          <meta property="og:site_name" content="CyclingCompare" />
          <meta property="og:image" content="https://cyclingcompare.com.au/logos/site/ogLogo.png" />
          <meta property="og:image:type" content="image/png"/>
          <meta property="og:image:width" content="1200"/>
          <meta property="og:image:height" content="630"/>
          <meta property="og:description" content="Australia's Home of Cycling Deals - Search all the best deals" key="description" />
        </Head>
        <body>
          <Main />
          <NextScript />
        </body>
      </Html>
    )
  }
}

export default MyDocument