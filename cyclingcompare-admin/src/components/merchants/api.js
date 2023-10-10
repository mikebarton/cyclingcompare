import config from '../../config'
import firebase from 'firebase';
import { SendAuthenticated, SendAnonymous } from '../../helpers/networkHelper'

export const GetAllMerchants = async () => {    
     var targetUrl = config.ImportApi.host + config.ImportApi.GetMerchants; 
     return SendAuthenticated(targetUrl, null, 'GET')
 }

 export const UnpublishGlobalProductByMerchantId = async (merchantId) => {    
    var targetUrl = config.ImportApi.host + config.ImportApi.UnPublishProductsByMerchant + merchantId; 
    return SendAuthenticated(targetUrl, null, 'GET')
}

export const PublishGlobalProductByMerchantId = async (merchantId) => {    
    var targetUrl = config.ImportApi.host + config.ImportApi.PublishProductsByMerchant + merchantId; 
    return SendAuthenticated(targetUrl, null, 'GET')
}