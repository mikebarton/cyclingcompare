 import config from '../../config'
import firebase from 'firebase';
import { SendAuthenticated, SendAnonymous } from '../../helpers/networkHelper'
 
 export const GetAllCategoriesFlattened = async () => {     
     var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.getAllCategorys;
     return SendAuthenticated(targetUrl, null, 'GET')
 }

 export const GetAllCategoryHierarchies = async () => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.getCategoryHierarchy;
    return SendAuthenticated(targetUrl, null, 'GET')
 }

 export const UpdateCategory = async (cat) => {
     var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.updateCategory;
     return SendAuthenticated(targetUrl, cat, 'POST')
 }

 export const DeleteCategory = async (catId) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.deleteCategory + catId;
    return SendAuthenticated(targetUrl, catId, 'DELETE')
 }

 export const CreateCategory = async (category) => {
    var targetUrl = config.ProductSearchApi.host + config.ProductSearchApi.createCategory;
    return SendAuthenticated(targetUrl, category, 'POST')
 }

 export const GetExternalCategories = async () => {
     var targetUrl = config.ImportApi.host + config.ImportApi.getExternalCategories;
     return SendAuthenticated(targetUrl, null, 'GET');
 }

 export const GetCategoryMappings = async () => {
     var targetUrl = config.ImportApi.host + config.ImportApi.getCategoryMappings;
     return SendAuthenticated(targetUrl, null, 'GET');
 }

 export const UpdateCategoryMapping = async (mapping) => {
     var targetUrl = config.ImportApi.host + config.ImportApi.UpdateCategoryMappings;
     return SendAuthenticated(targetUrl, mapping, 'POST')
 }