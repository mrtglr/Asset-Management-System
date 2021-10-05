import { Component, OnInit } from '@angular/core';
import { LogOperationsService } from '../_shared/log-operations.service';
import * as XLSX from 'xlsx'; 
import { Pagination } from '../_shared/pagination.model';

@Component({
  selector: 'app-log-operation-list',
  templateUrl: './log-operation-list.component.html',
  styles: []
})
export class LogOperationListComponent implements OnInit {

  constructor(public service: LogOperationsService) { }

  fileName= 'Loggers.xlsx';
  searchText: string = '';
  pagination: Pagination = new Pagination(1,0,9, [10,20,30,40]);

  ngOnInit() {
    this.getLogs();
  }

  //**Helpers**//

  getLogs() {
    this.service.createLogs().subscribe(
      res=>{
        this.service.list_logOperations = res;
      }
    );
  }

  getFiltered() {
    this.service.getLogs(this.searchText, this.pagination).subscribe(
      res=> {
        this.pagination.count = this.service.list_logOperations.length;
      },
      err=> {
        console.log(err);
      });
  }

  //**Buttons**//

  searchList(filter: string){
    this.service.search(filter).subscribe(
      res=>{
        this.service.list_logOperations = res;
      }
    );
  }

  onPrint () {
    let element = document.getElementById('table'); 
    const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, this.fileName);			    
  }

  onPageChange(event) {
    this.pagination.page = event;
    this.getFiltered();
  } 

  onSearch () {
    var filter = (<HTMLInputElement>document.getElementById("search")).value;
    if(filter!=null && filter!=""){
      this.searchList(filter);
    }   
    else {
      this.getLogs();
    }  
  }

}
