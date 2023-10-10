import Config from '../../config'

export const pageview = (url) => {
    window.gtag('config', Config.GoogleAnalyticsId, {
      page_path: url,
    })
  }
  
  // log specific events happening.
  export const event = ({ action, params }) => {
    window.gtag('event', action, params)
  }