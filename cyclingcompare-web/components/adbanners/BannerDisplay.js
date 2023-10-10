import {useState, useEffect } from 'react';
import Config from '../../config';
import * as styles from './bannerDisplay.module.scss';

export default function BannerDisplay({width, height}){
    var [banner, setBanner] = useState();

    useEffect(()=>{
        if(width > 10 && height > 10){
            var url = Config.ApiGateway.HostName + Config.ApiGateway.GetRandomBanner + '?width=' + width + '&height=' + height;
            fetch(url)
            .then(res=> res.json())
            .then(res => setBanner(res))
        }
    }, [width, height])

    return (
        <div className={styles.bannerContainer}>
            { banner && <a href={banner.trackingUrl} target='_blank'                        
                        referrerPolicy='no-referrer-when-downgrade'
                        rel='nofollow noindex'><img src={banner.imageUrl}/></a> }
        </div>
    )
}