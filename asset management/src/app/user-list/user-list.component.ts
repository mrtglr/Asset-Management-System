import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../_shared/user.model';
import { UserService } from '../_shared/user.service';
import * as XLSX from 'xlsx'; 
import { Pagination } from '../_shared/pagination.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styles: []
})
export class UserListComponent implements OnInit {

  constructor(public service: UserService, private router: Router) { }

  fileName= 'Users.xlsx';
  searchText: string = '';
  pagination: Pagination = new Pagination(1,0,7, [10,20,30,40]);

  getKullanici() {
    this.service.createList().subscribe(
      res=>{
        this.service.list_user = res;
      }
    );
  }

  getKullaniciList() {
    this.service.getUserList(this.searchText, this.pagination).subscribe(
      res=> {
        this.pagination.count = this.service.list_user.length;
      },
      err=> {
        console.log(err);
      });
  }

  onPageChange(event) {
    this.pagination.page = event;
    this.getKullaniciList();
  } 

  ngOnInit() { 
    this.getKullanici();
  }

  onPost() {
    this.router.navigate(['/kullaniciAdd']);
  }

  onDelete(k_id: number) {
    if (confirm('Dou you want to delete this record ?')) {
      this.service.deleteUser(k_id).subscribe(
        res => {          
          this.getKullanici();
        },
        err => {
          console.log(err);
        })
      }
  }

  populateForm(user: User) {
    this.service.formDataUser = Object.assign({}, user);
  }

  onUpdate (user: User) {
    this.populateForm(user)
    this.router.navigate(['/kullaniciUpt']); 
  }

  onPrint () {
    let element = document.getElementById('table'); 
    const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, this.fileName);			    
  }

  onSearch () {
    var filter = (<HTMLInputElement>document.getElementById("search")).value;
    if(filter!=null && filter!=""){
      this.searchList(filter);
    }     
    else{
      this.getKullanici();
    }
  }

  searchList(filter: string){
    this.service.search(filter).subscribe(
      res=>{
        this.service.list_user = res;
      }
    );;
  }

}
