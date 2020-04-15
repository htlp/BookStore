import { Injectable } from '@angular/core';

import axios from 'axios';

import Qs from 'qs';
import { promise } from 'protractor';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  constructor() { 
    //axios.defaults.baseURL = 'http://localhost:19604';
    //axios.defaults.headers.common['Authorization'] = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJlMDkzMDM1MS0xZjc1LTQ4ZDctODVmNS05NmI5ZGFlMDFkZTUiLCJpZCI6MSwibmFtZSI6ImJlY2siLCJhZG1pbiI6dHJ1ZSwibmJmIjoxNTg2Njk2OTM3LCJleHAiOjE1ODY2OTg5MzcsImlzcyI6Imp3dElzc3VlcnRlc3QiLCJhdWQiOiJqd3RBdWRpZW5jZXRlc3QifQ.UlISqREiR2guoXc15tmcopBB8PdMxUCQwVbJ1_8V-UY";
    //axios.defaults.headers.post['Content-Type'] = 'application/json';

  }

  getBooks(api:string){
    return new Promise((resolve,reject)=>{
      axios.get(api,{headers:{'Authorization': 'Bearer ' + localStorage.getItem("Token"),}})
      .then(function (response) {
        resolve(response);
      });
    });
    
  }

  postBooks(api:string,bookid:number,bookname:string){
    
    let obj = {id:bookid,name:bookname};
    console.log(Qs.stringify(obj));
    return new Promise((resolve,reject)=>{
      axios.post(api, Qs.stringify(obj),{
        headers:{'Authorization': 'Bearer ' + localStorage.getItem("Token")}
      })
      .then(function (response) {
        resolve(response);
      });
    });

  }

  delBook(api:string){
    return new Promise((resolve,reject)=>{
      axios.delete(api,{
        headers:{'Authorization': 'Bearer ' + localStorage.getItem("Token")} 
      })
      .then(function (response) {
        resolve(response);
      });
    });

  }

  login(api:string,name:string,password:string){
    return new Promise((resolve,reject)=>{
      axios.post(api, {
        Name: name,
        Password:password
      },{
        withCredentials: true
      })
      .then(function (response) {
        resolve(response);
      });
    });

  }

}
