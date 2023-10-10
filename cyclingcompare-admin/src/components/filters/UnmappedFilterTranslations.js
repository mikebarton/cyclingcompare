import { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import * as styles from './filters.module.scss'
import * as actionTypes from './actionTypes';
import { Icon, IconSize, Drawer, Classes, Button, DrawerSize, InputGroup } from '@blueprintjs/core';

function UnmappedFilterTranslations({categoryId, filterGroupId, selectedFilter, unmappedTranslations, allTranslations, addOrUpdateFilter, retrieveUnmappedTranslations, mapTranslation, mapTranslationBatch, retrieveAllTranslations}){
    const [isImageDrawerOpen, setIsImageDrawerOpen] = useState(false)
    const [imagesForTranslation, setImagesForTranslation] = useState();
    const [autoShowImage, setAutoShowImagge] = useState(false);
    const [showAllTranslationsToMap, setShowAllTranslationsToMap] = useState(false);
    const [searchFilter, setSearchFilter] = useState();

    const [translationsToDisplay, setTranslationsToDisplay] = useState([]);

    useEffect(()=>{
        if(unmappedTranslations, allTranslations){
            var relevantTranslations = showAllTranslationsToMap ? allTranslations : unmappedTranslations;
            if(searchFilter){
                var filtered = relevantTranslations.filter(x=>x.translationName.includes(searchFilter));
                filtered = filtered.sort(sortFunction);
                setTranslationsToDisplay(filtered)
            }
            else{
                relevantTranslations.sort(sortFunction)
                setTranslationsToDisplay(relevantTranslations);
            }            
        }
    }, [unmappedTranslations, allTranslations, showAllTranslationsToMap, searchFilter]);

    function sortFunction(a, b) {
        var nameA = a.translationName.toUpperCase(); // ignore upper and lowercase
        var nameB = b.translationName.toUpperCase(); // ignore upper and lowercase
        if (nameA < nameB) {
          return -1;
        }
        if (nameA > nameB) {
          return 1;
        }
      
        // names must be equal
        return 0;
      };
    
    useEffect(()=>{
        if(categoryId && (filterGroupId !== null && filterGroupId !== undefined)){
            retrieveUnmappedTranslations(categoryId, filterGroupId);
            retrieveAllTranslations(categoryId, filterGroupId);
        }
    }, [categoryId, filterGroupId])

    function matchByFilterName(){        
        if(!selectedFilter)
            return;
        
        var matchingTranslations = unmappedTranslations.filter(x=>x.toLowerCase().includes(selectedFilter.name.toLowerCase()) 
                                    ||  selectedFilter.name.toLowerCase().includes(x.toLowerCase()));

        mapTranslationBatch(matchingTranslations, selectedFilter.categoryFilterId, categoryId, filterGroupId);
    }

    function onTranslationClick(trans){
        if(!selectedFilter)
            addOrUpdateFilter({ name: trans.translationName, categoryId: categoryId, categoryFilterGroupId: filterGroupId});
        else
            mapTranslation(selectedFilter.categoryFilterId, trans.translationName, categoryId, filterGroupId)

    }    

    function showImages(translation){
        setIsImageDrawerOpen(true)
        var matchingTranslations = unmappedTranslations.filter(x=>x === translation);
        var images = matchingTranslations[0].imageUrls;
        setImagesForTranslation(images);
    }

    function toggleShowAllTranslations(){
        retrieveUnmappedTranslations(categoryId, filterGroupId);
        retrieveAllTranslations(categoryId, filterGroupId);
        setShowAllTranslationsToMap(!showAllTranslationsToMap)
    }

    return (<>
        <h3>{showAllTranslationsToMap ? <>Mapped Translations</> : <>Unmapped Translations</>} <Icon icon={'media'} htmlTitle={'Show all images'} iconSize={16} onClick={()=> setAutoShowImagge(!autoShowImage)}/><Icon icon={'expand-all'} htmlTitle={'Show all translations'} iconSize={16} onClick={()=> toggleShowAllTranslations()}/></h3>
        <div className={styles.translationContainer}>
            <p>Translations are the values that we receive from the suppliers. We need to assign all the translations to one of the filters</p>
            <div className={styles.actionButtons}>
                <Icon
                    icon={'layout-auto'}
                    htmlTitle={'Match By FilterName'} 
                    iconSize={14}
                    onClick={matchByFilterName}
                /> <div className={styles.buttonLabel}>Apply Translations with Partial Matching Names</div>
                <div className={styles.searchArea}>
                    <InputGroup value={searchFilter} onChange={e=> setSearchFilter(e.target.value)} />
                </div>
            </div>
            
            <div className={styles.filterList}>
                { translationsToDisplay.map(x=>{
                    return (
                        <div className={styles.filterTranslation}>
                            <div className={styles.actionButton}>
                                <Icon
                                    icon={'add'}
                                    htmlTitle={'Assign to Filter'} 
                                    iconSize={12}
                                    onClick={() => onTranslationClick(x)}
                                />                                
                            </div>                            
                            {x.translationName}
                            <div className={styles.actionButton}>
                                <Icon
                                        icon={'search'}
                                        htmlTitle={'Show Images for this Filter'} 
                                        iconSize={12}
                                        onClick={() => showImages(x)}
                                    />
                            </div>
                            {autoShowImage && x.imageUrls.map(i=> <img src={i}/>)}
                        </div>
                    )
                })}
            </div>
        </div>
        <Drawer isOpen={isImageDrawerOpen} size={'400px'}>
            <div className={Classes.DRAWER_BODY}>
                <div className={Classes.DIALOG_BODY}>
                    {imagesForTranslation && filterGroupId && imagesForTranslation?.map(x=> ( <div><img src={x} /></div>))}
                </div>
                <div className={Classes.DRAWER_FOOTER}>
                    <Button onClick={()=>setIsImageDrawerOpen(false)}>Close</Button>
                </div>
            </div>
        </Drawer>
        </>
    )
}

var mapStateToProps = (state, {filterGroupId}) => ({
    unmappedTranslations: state.Filters.UnmappedTranslations[filterGroupId] || [],
    allTranslations: state.Filters.TranslationsByCategory[filterGroupId] || [],
    images: state.Filters.Images[filterGroupId] || {}
});

var mapDispatchToProps = dispatch => ({
    retrieveUnmappedTranslations: (categoryId, filterGroupId) => dispatch({ type: actionTypes.GET_UNMAPPED_TRANSLATIONS_BY_CATEGORY, categoryId: categoryId, filterGroupId: filterGroupId }),
    retrieveAllTranslations: (categoryId, filterGroupId) => dispatch({ type: actionTypes.GET_MAPPED_TRANSLATIONS_BY_CATEGORY, categoryId: categoryId, filterGroupId: filterGroupId }),
    mapTranslation: (filterId, translationName, categoryId, filterType) => dispatch({ type: actionTypes.ADD_OR_UPDATE_TRANSLATION, translation: {categoryFilterId: filterId, name: translationName}, categoryId: categoryId, filterType: filterType }),
    mapTranslationBatch: (translations, filterId, categoryId, filterType) => dispatch({ type: actionTypes.ADD_OR_UPDATE_TRANSLATION_BATCH, translations: translations.map(x=> {return {categoryFilterId: filterId, name: x}}), categoryId: categoryId, filterType: filterType}),
    addOrUpdateFilter: (filter) => dispatch({ type: actionTypes.ADD_OR_UPDATE_FILTER, filter: filter})    
});

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(UnmappedFilterTranslations);