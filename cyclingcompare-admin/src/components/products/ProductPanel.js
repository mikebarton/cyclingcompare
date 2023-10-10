import { useState, useEffect } from 'react'
import { connect } from 'react-redux'
import * as actionTypes from './actionTypes'
import { GetGlobalProduct } from './api'
import * as styles from './products.module.scss'
import { Icon, Intent, InputGroup, Classes, Collapse } from '@blueprintjs/core'
import { IconNames } from '@blueprintjs/icons'
import CategorySelector from '../categories/CategorySelector'
import NumberFormat from 'react-number-format'

function ProductPanel (props) {
  var [product, setProduct] = useState()
  var [features, setFeatures] = useState([])
  var [specs, setSpecs] = useState({})
  var [description, setDescription] = useState('')
  var [categoryId, setCategoryId] = useState('');
  var [title, setTitle] = useState('')

  useEffect(async () => {
    if (props.productId) {
      var retrievedProduct = await GetGlobalProduct(props.productId)
      setProduct(retrievedProduct)
    }
  }, [props.productId])

  useEffect(() => {
    if (product) {
      setCategoryId(product.categoryId)
      setDescription(product.description)
      setTitle(product.name)
      setFeatures(JSON.parse(product.features) || [])
      setSpecs(JSON.parse(product.specs) || {})
    }
  }, [product])

  function updateFeatureContent (updatedContent, index) {
    features[index] = updatedContent
    setFeatures([...features])
  }

  function removeFeatureItem (index) {
    features.splice(index, 1)
    setFeatures([...features])
  }

  function addFeatureItem () {
    features.splice(features.length, 0, '')
    setFeatures([...features])
  }

  function updateSpecContent (key, newKey, value) {
    delete specs[key]
    specs[newKey] = value
    setSpecs({ ...specs })
  }

  function removeSpecItem (key) {
    delete specs[key]
    setSpecs({ ...specs })
  }

  function addSpecItem () {
    specs[' '] = ''
    setSpecs({ ...specs })
  }

  function saveProductChanges () {
    var updatedProduct = {
      ...product,
      categoryId: categoryId,
      name: title,
      description: description,
      features: JSON.stringify(features),
      specs: JSON.stringify(specs)
    }
    props.updateProduct(updatedProduct)
  }

  function publishProduct () {
    props.publishProduct([product.globalProductId])
  }

  function unpublishProduct(){
    props.unpublishProduct([product.globalProductId]);
  }

  return (
    <div>
      {product && (
        <div className={styles.productPanel}>
          <div className={styles.controlButtons}>
            <div className={styles.controlButton}>
              <Icon
                icon={'floppy-disk'}
                htmlTitle={'Save Changes'}
                onClick={() => saveProductChanges()}
              />
            </div>
            <div className={styles.controlButton}>
              <Icon
                icon={'send-message'}
                htmlTitle={'Publish Product'}
                onClick={() => publishProduct()}
              />
            </div>
            <div className={styles.controlButton}>
              <Icon
                icon={IconNames.DISABLE}
                htmlTitle={'UnPublish Product'}
                onClick={() => unpublishProduct()}
              />
            </div>
            <div className={styles.controlButton}>
              <Icon
                icon={'confirm'}
                htmlTitle={'Set as Reviewed'}
                onClick={() => props.setReviewed(product.globalProductId)}
              />
            </div>
            <div className={styles.controlButton}>
              <Icon
                icon={'ban-circle'}
                htmlTitle={'Set as Not Reviewed'}
                onClick={() => props.setNotReviewed(product.globalProductId)}
              />
            </div>
          </div>
          <div className={styles.productTitle}>
            <h3>
              <EdittableTextArea
                srcText={title}
                updateContent={text => setTitle(text)}
              />
            </h3>
          </div>
          <div className={styles.categorySelector}>
            <span>Category:</span>
            <CategorySelector categoryId={product.categoryId} onCategorySelected={cat=> setCategoryId(cat.categoryId)}/>
          </div>
          <div className={styles.productData}>
            <div>
              <span>Brand:</span>
              {product.brand}
            </div>
            <div>
              <span>Colour:</span>
              {product.colour}
            </div>
            <div>
              <span>ContentRating:</span>
              {product.contentRating}
            </div>
            <div>
              <span>Currency:</span>
              {product.currency}
            </div>
            <div>
              <span>Delivery Cost:</span>
              {product.deliveryCost}
            </div>
            <div>
              <span>Delivery Time:</span>
              {product.deliveryTime}
            </div>
            <div>
              <span>Gender:</span>
              {product.gender}
            </div>
            <div>
              <span>Model Number:</span>
              {product.modelNumber}
            </div>
            <div>
              <span>Size:</span>
              {product.size}
            </div>
            <div>
              <span>PromoText:</span>
              {product.promoText}
            </div>
          </div>
          <div className={styles.imageContainer}>
            <img src={product.imageUrl} />
          </div>
          <p className={styles.productDescription}>
            <EdittableTextArea
              srcText={description}
              updateContent={desc => setDescription(desc)}
            />
          </p>
          <div className={styles.pageSplit}>
            <div className={styles.features}>
              <h3>Features</h3>
              <span>
                <Icon icon={'new-text-box'} onClick={addFeatureItem} />
              </span>
              <ul>
                {features?.map((x, i) => (
                  <FeatureRow
                    key={'feature' + i}
                    content={x}
                    index={i}
                    updateCallback={updateFeatureContent}
                    removeCallback={removeFeatureItem}
                  />
                ))}
              </ul>
            </div>
            <div className={styles.specs}>
              <h3>Specs</h3>
              <span>
                <Icon icon={'new-text-box'} onClick={addSpecItem} />
              </span>
              <ul>
                {Object.keys(specs).map(element => (
                  <SpecRow
                    key={'sk' + element}
                    rowKey={element}
                    rowValue={specs[element]}
                    updateContent={updateSpecContent}
                    removeItem={removeSpecItem}
                  />
                ))}
              </ul>
            </div>            
          </div>
          <div className={styles.merchantProducts}>
            { product.merchantProducts.map(x=>{
                return <MerchantProduct detail={x}/>
            })}
          </div>
        </div>
      )}
    </div>
  )
}

function EdittableTextArea ({ srcText, updateContent }) {
  var [isEditting, setIsEditting] = useState(false)
  var [localText, setLocalText] = useState()
  useEffect(() => {
    setLocalText(srcText)
  }, [srcText])
  function saveUpdatedContent () {
    setIsEditting(false)
    updateContent(localText)
  }

  return !isEditting ? (
    <>
      {localText}
      <span className={styles.iconContainer}>
        <Icon icon={'edit'} onClick={() => setIsEditting(true)} />
      </span>
    </>
  ) : (
    <>
      <textarea
        className={`${styles.fullTextArea} ${Classes.INPUT}`}
        value={localText}
        onChange={e => setLocalText(e.target.value)}
      />
      <span className={styles.iconContainer}>
        <Icon icon={'tick'} onClick={() => saveUpdatedContent()} />
        <Icon icon={'cross'} onClick={() => setIsEditting(false)} />
      </span>
    </>
  )
}

function MerchantProduct({detail}){
  var [showData, setShowData] = useState(false);
  var [features, setFeatures] = useState(JSON.parse(detail.features) || [])
  var [specs, setSpecs] = useState(JSON.parse(detail.specs) || {})

  useEffect(()=>{
    if(detail){
      var parsedFeatures = JSON.parse(detail.features) || []
      setFeatures(parsedFeatures);
      var parsedSpecs = JSON.parse(detail.specs) || {}
      setSpecs(parsedSpecs);
    }
  }, [detail])

  const isDiscounted = detail.priceRrp && detail.priceRrp > 0
  return (
    <div>    
      <div onClick={()=> setShowData(!showData)} className={styles.merchantProduct} target='_self'>
        <div className={styles.merchantProductHeader}>
          {detail.merchantName}
          <div className={styles.merchantPrice}>
            {isDiscounted ? (
                          <s><NumberFormat value={detail.priceRrp} displayType={'text'} thousandSeparator={true} prefix={'RRP $'} /></s>
                        ) : (
                          ''
                        )}
                        <span>
                          <NumberFormat value={detail.price} displayType={'text'} thousandSeparator={true} prefix={'$'} />
                        </span>
          </div>
          <div className={styles.productLink} onClick={e=> {window.open(detail.targetUrl,'_blank'); e.stopPropagation();}}>
            <span>Go to Merchant Product</span>
          </div>
        </div>
        <Collapse isOpen={showData}>
        <div className={styles.merchantProductPanel}>
          <div className={styles.productTitle}>
            <h3>{detail.name}</h3>
          </div>
          <div className={styles.categoryDisplay}>
            <span>{detail.category}</span><span>:</span><span>{detail.subCategory}</span>
          </div>
          <div className={styles.productData}>
            <div>
              <span>Brand:</span>
              {detail.brand}
            </div>
            <div>
              <span>Colour:</span>
              {detail.colour}
            </div>
            <div>
              <span>ContentRating:</span>
              {detail.contentRating}
            </div>
            <div>
              <span>Currency:</span>
              {detail.currency}
            </div>
            <div>
              <span>Delivery Cost:</span>
              {detail.deliveryCost}
            </div>
            <div>
              <span>Delivery Time:</span>
              {detail.deliveryTime}
            </div>
            <div>
              <span>Gender:</span>
              {detail.gender}
            </div>
            <div>
              <span>Model Number:</span>
              {detail.modelNumber}
            </div>
            <div>
              <span>Size:</span>
              {detail.size}
            </div>
            <div>
              <span>PromoText:</span>
              {detail.promoText}
            </div>
          </div>
          <div className={styles.imageContainer}>
            <img src={detail.imageUrl} />
          </div>
          <p className={styles.productDescription}>            
            {detail.description}              
          </p>
          <div className={styles.pageSplit}>
            <div className={styles.features}>
              <h3>Features</h3>              
              <ul>
                {features?.map((x, i) => (
                  <li>{x}</li>                                  
                ))}
              </ul>
            </div>
            <div className={styles.specs}>
              <h3>Specs</h3>              
              <ul>
                {Object.keys(specs).map(element => (
                  <li>{element} : {specs[element]}</li>
                ))}
              </ul>
            </div>            
          </div>
        </div>
      </Collapse>
      </div>      
    </div>
  )
}

function SpecRow ({ rowKey, rowValue, updateContent, removeItem }) {
  var [isEditting, setIsEditting] = useState(false)
  var [key, setKey] = useState(rowKey)
  var [value, setValue] = useState(rowValue)
  function saveUpdatedContent () {
    setIsEditting(false)
    updateContent(rowKey, key, value)
  }
  return (
    <li>
      {!isEditting ? (
        <>
          {key}: {value}
          <span className={styles.iconContainer}>
            <Icon icon={'edit'} onClick={() => setIsEditting(true)} />
            <Icon icon={'trash'} onClick={() => removeItem(rowKey)} />
          </span>
        </>
      ) : (
        <>
          <InputGroup value={key} onChange={e => setKey(e.target.value)} /> :{' '}
          <InputGroup value={value} onChange={e => setValue(e.target.value)} />
          <span className={styles.iconContainer}>
            <Icon icon={'tick'} onClick={() => saveUpdatedContent()} />
            <Icon icon={'cross'} onClick={() => setIsEditting(false)} />
          </span>
        </>
      )}
    </li>
  )
}

function FeatureRow ({ content, index, updateCallback, removeCallback }) {
  var [isEditting, setIsEditting] = useState(false)
  var [edittingContent, setEdittingContent] = useState(content)
  function saveUpdatedContent () {
    setIsEditting(false)
    updateCallback(edittingContent, index)
  }

  return (
    <li>
      {!isEditting ? (
        <>
          {content}
          <span className={styles.iconContainer}>
            <Icon icon={'edit'} onClick={() => setIsEditting(true)} />
            <Icon icon={'trash'} onClick={() => removeCallback(index)} />
          </span>
        </>
      ) : (
        <>
          <InputGroup
            className={styles.inlineText}
            value={edittingContent}
            onChange={e => setEdittingContent(e.target.value)}
          />
          <span className={styles.iconContainer}>
            <Icon icon={'tick'} onClick={() => saveUpdatedContent()} />
            <Icon icon={'cross'} onClick={() => setIsEditting(false)} />
          </span>
        </>
      )}
    </li>
  )
}

var mapStateToProps = state => ({
  // products: state.Product.GlobalProducts
})

var mapDispatchToProps = dispatch => ({
  // getProductSummaries: ()=> dispatch({ type: actionTypes.GET_ALL_PRODUCT_SUMMARIES })
  updateProduct: product => dispatch({ type: actionTypes.UPDATE_GLOBAL_PRODUCT, product: product }),
  publishProduct: productId => dispatch({ type: actionTypes.PUBLISH_GLOBAL_PRODUCT, productId: productId }),
  unpublishProduct: productId => dispatch({ type: actionTypes.UNPUBLISH_GLOBAL_PRODUCT, productId: productId }),
  setReviewed: productId => dispatch({ type: actionTypes.SET_PRODUCT_REVIEWED, productId: productId}),
  setNotReviewed: productId => dispatch({ type: actionTypes.SET_PRODUCT_NOT_REVIEWED, productId: productId})
})

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ProductPanel)
