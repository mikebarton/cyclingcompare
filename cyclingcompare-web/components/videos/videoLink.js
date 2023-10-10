import * as styles from './videoLink.module.scss'
import Link from 'next/link'

export default function VideoLink ({
  authorLogo,
  authorText,
  linkUrl,
  linkText
}) {
  return (
    <div className={styles.linkPanel}>
      <div className={styles.logo}>
        <img src={authorLogo} className={styles.gcnIcon} />
      </div>
      <div className={styles.linkText}>
        <Link href={linkUrl}>{linkText}</Link>
        <div className={styles.authorText}>{authorText}</div>
      </div>
    </div>
  )
}
