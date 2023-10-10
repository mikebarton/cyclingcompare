import { useState } from 'react'
import * as styles from './sorter.module.scss'

export default function Sorter ({onSortOrderUpdated}) {
  var [sortKey, setSortKey] = useState(0)

  function onSelectionChanged(newSortKey){
    var newKey = parseInt(newSortKey)
      setSortKey(newKey);
      onSortOrderUpdated(newKey)
  }

  return (
    <div className={styles.sortBox}>
      <div className={styles.sortLabel}>Sort By:</div>
      <select onChange={e => onSelectionChanged(e.target.value)} value={sortKey}>
        <option value={null}>Sort By</option>
        <option value={3}>$$$ - $</option>
        <option value={2}>$ - $$$</option>
        <option value={0}>A - Z</option>
        <option value={1}>Z - A</option>
      </select>
    </div>
  )
}
