import firebase from 'firebase';
import fetch from 'cross-fetch';

export const SendAuthenticated = function(url, parms = null, method ='POST', responseHandler = standardResponseHandler){
    
    var user = firebase.auth().currentUser;
    var message = {
        method: method,
        headers: {}
    }

    if(parms){
        message['body'] = JSON.stringify(parms);  
        message.headers['Content-Type'] = "application/json";      
    }

    return user.getIdToken()
        .then(token=>{
            message.headers['Authorization'] = "Bearer " + token            
            return fetch(url, message);            
        })
        .then(responseHandler, standardErrorHandler);       
}

export const SendAnonymous = function(url, parms = null, method= 'POST', responseHandler = standardResponseHandler){
    var message = {
        method: method,
        headers: {}
    }

    if(parms){
        message['body'] = JSON.stringify(parms);  
        message.headers['Content-Type'] = "application/json";    
    }
    
    return fetch(url, message)
    .then(responseHandler, standardErrorHandler);  
}

const standardResponseHandler = function(response){
    if(response.status === 200)
    {   
        if(response.headers.map['content-type']){
            if(response.headers.map['content-type'].includes('application/json')){
                return response.json();
            }
        }        
    }                
    return new Promise((resolve, reject) =>{
        console.log('empty response');
        resolve();
    });
}

const standardErrorHandler = function(err){
    return new Promise((resolve, reject)=>{
        reject(err);
    })
}