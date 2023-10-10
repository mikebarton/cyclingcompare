 import config from '../../config'
import firebase from 'firebase';
import { SendAuthenticated, SendAnonymous } from '../../helpers/networkHelper'
 
 export const GetAllProductSummaries = async () => {    
     var targetUrl = config.ImportApi.host + config.ImportApi.getProductSummaries; 
     return SendAuthenticated(targetUrl, null, 'GET')
 }

 export const GetGlobalProduct = async (globalProductId) =>{
     var targetUrl = config.ImportApi.host + config.ImportApi.getGlobalProduct + globalProductId;
     return SendAuthenticated(targetUrl, null, 'GET');
 }

 export const UpdateGlobalProduct = async (product) => {
     var targetUrl = config.ImportApi.host + config.ImportApi.updateGlobalProduct;
     return SendAuthenticated(targetUrl, product, 'POST')
 }

 export const PublishGlobalProduct = async (productIds) => {
     var targetUrl = config.ImportApi.host + config.ImportApi.publishGlobalProducts;
     return SendAuthenticated(targetUrl, productIds, 'POST')
 }

 export const UnpublishGlobalProduct = async (productIds) => {
     var targetUrl = config.ImportApi.host + config.ImportApi.unpublishGlobalProducts;
     return SendAuthenticated(targetUrl, productIds, 'POST')
 }

 export const SetAsReviewed = async (globalProductId) => {
     var targetUrl = config.ImportApi.host + config.ImportApi.setAsReviewed + globalProductId;
     return SendAuthenticated(targetUrl, null, 'POST')
 }

 export const SetAsNotReviewed = async (globalProductId) => {
     var targetUrl = config.ImportApi.host + config.ImportApi.setAsNotReviewed + globalProductId;
     return SendAuthenticated(targetUrl, null, 'POST')
 }