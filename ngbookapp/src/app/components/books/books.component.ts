import { Component, OnInit } from '@angular/core';
import { BooksService } from 'src/app/services/books.service';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { config } from 'rxjs';



@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  public list:any[]=[];
  public id:number=555;
  public name:string="5555";
  constructor(public http:HttpClient,public booksService:BooksService) {
   }

  ngOnInit(): void {
  }

  getBooks(){
    let api="http://localhost:19604/odata/books";
    var _this=this
    this.booksService.getBooks(api).then((data:any)=>{      
      console.log(data.data);
      _this.list=data.data.value;

    });
  }
  createBooks(){
    let api="http://localhost:19604/odata/books";
    var _this=this    
    this.booksService.postBooks(api,this.id,this.name).then((data:any)=>{      
      console.log(data.data);
      _this.list=data.data.value;

    });
  }

  delBook(key:number){
    let api="http://localhost:19604/odata/books("+key+")";
    var _this=this
    this.booksService.delBook(api).then((data:any)=>{      
      console.log(data.data);
      _this.list=data.data.value;

    });
  }
  login(){
    
    let api="http://localhost:5000/api/TokenAuth";
    
    let name="beck";
    let password="123456";
    var _this=this
    this.booksService.login(api,name,password).then((data:any)=>{    
        
      localStorage.setItem("Token",data.data.Token);

      console.log(data.data.Token);

    });
  }
}
