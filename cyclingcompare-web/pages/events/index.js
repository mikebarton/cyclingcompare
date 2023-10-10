import WidePageLayout from '../../components/layouts/widePageLayout'
import React, {useRef} from 'react'
import Link from 'next/link'
import Layout from '../../components/layouts/layout'
import Config from '../../config'
import * as styles from './index.module.scss'
import BannerDisplay from '../../components/adbanners/BannerDisplay'
import PageSubLayout from '../../components/layouts/pageSubLayout'
import { EventsObjects } from '../../lib/events/eventObjects'
import { Calendar, momentLocalizer, Views } from 'react-big-calendar'
import moment from 'moment'
import 'react-big-calendar/lib/css/react-big-calendar.css'

function Events ({ categories }) {
  function VertBanner () {
    return (
      <div className={styles.vertBannerCrop}>
        <BannerDisplay width={160} height={600} />
      </div>
    )
  }

  function EventItem ({ref, eventItem }) {
    const myRef = React.createRef();
    eventItem['scroll'] = ()=> myRef.current.scrollIntoView();

    return (
      <div ref={myRef} id={eventItem.Id} className={styles.event}>
        <div className={styles.col}>{moment(eventItem.Date)?.format('DD/MM/YYYY')}</div>
        <div className={styles.col}>
          <div className={styles.eventName}>
            <h2>
              {eventItem.State} -{' '}
              <Link href={eventItem.Link}>{eventItem.Name}</Link>
              <Link href={eventItem.Link}><img src={'/icons/link.svg'} /></Link>
            </h2>
          </div>
          <div className={styles.line}>{eventItem.Name}</div>
          <div className={styles.line}>{eventItem.Address}</div>
          <div className={styles.line}>{eventItem.City}</div>
          <div className={styles.line}>{eventItem.State}</div>
        </div>
      </div>
    )
  }
  const localizer = momentLocalizer(moment) 
  

  return (
    <Layout categories={categories}>
      <PageSubLayout categories={categories} leftPane={<VertBanner />}>
        <BannerDisplay width={728} height={90} />
        <div className={styles.page}>
          <h1>Event Calendar</h1>
          <div className={styles.calendar}>
            <Calendar popup selectable
              localizer={localizer}
              events={EventsObjects}
              titleAccessor='Name'
              tooltipAccessor='Name'
              views={[Views.MONTH]}
              startAccessor='Date'
              endAccessor='Date'
              style={{ height: 500, width: '100%' }}
              onSelectEvent={event=> event.scroll()}              
            />
          </div>
          <div className={styles.eventsContainer}>
            {EventsObjects.filter(x=> x.Date >= new Date()).map(x => <EventItem eventItem={x} /> )}
          </div>
        </div>
      </PageSubLayout>
    </Layout>
  )
}

export default Events

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
