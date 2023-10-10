import config from '../../config'
import firebase from 'firebase';
import { SendAuthenticated, SendAnonymous } from '../../helpers/networkHelper'

export const GetFiltersByCategory = async (categoryId, filterGroupId) => {     
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.getFiltersByCategory + categoryId + '?filterGroupId=' + filterGroupId;
    return SendAuthenticated(targetUrl, null, 'GET')
}

export const GetTranslations = async (filterGroupId, onlyUnmapped) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.getTranslations + filterGroupId + '?unmapped=' + onlyUnmapped;
    return SendAuthenticated(targetUrl, null, 'GET')
}

export const AddOrUpdateFilter = async (filter) =>{
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.addOrUpdateFilter;
    return SendAuthenticated(targetUrl, filter, 'POST')
}

export const DeleteFilter = async (filterId) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.deleteFilter + '?filterId=' + filterId;
    return SendAuthenticated(targetUrl, null, 'DELETE')
}

export const GetTranslationsByFilterId = async (categoryFilterId) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.getTranslationsByFilterId + categoryFilterId;
    return SendAuthenticated(targetUrl, null, 'GET')
}

export const AddOrUpdateTranslation = async (translation) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.addOrUpdateTranslation;
    return SendAuthenticated(targetUrl, translation, 'POST')
}

export const DeleteTranslation = async (translationId) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.deleteTranslationById + translationId;
    return SendAuthenticated(targetUrl, null, 'DELETE')
}

export const GetFilterGroups = async (categoryId) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.getFilterGroups + categoryId;
    return SendAuthenticated(targetUrl, null, 'GET')
}

export const AddOrUpdateFilterGroup = async (filterGroup) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.addOrUpdateFilterGroup;
    return SendAuthenticated(targetUrl, filterGroup, 'POST')
}

export const DeleteFilterGroup = async (filterGroupId) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.deleteFilterGroup + filterGroupId;
    return SendAuthenticated(targetUrl, null, 'DELETE')
}

export const RelocateFilter = async (filterId, targetGroupId) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.relocateFilter + filterId + '/' + targetGroupId;
    return SendAuthenticated(targetUrl, null, 'POST')
}