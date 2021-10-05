import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Asset } from '../_shared/asset.model';
import { AssetService } from '../_shared/asset.service';
import * as XLSX from 'xlsx'; 
import { Pagination } from '../_shared/pagination.model';
import { UserService } from '../_shared/user.service';

@Component({
  selector: 'app-asset-list',
  templateUrl: './asset-list.component.html',
  styles: []
})
export class AssetListComponent implements OnInit {

  constructor(public service: AssetService, private router: Router) { }

  fileName = 'Assets.xlsx';
  searchText: string = '';
  pagination: Pagination = new Pagination(1,0,7, [10,20,30,40]);

  ngOnInit() {  
    this.getAsset();
  }

  //**Helpers**//

  getAsset() {
    this.service.getAsset().subscribe(
      res=>{
        this.service.list_asset = res;
      }
    );
  }
  
  populateForm(formAsset: Asset) {
    this.service.formDataAsset = Object.assign({}, formAsset);
  }

  searchList(filter: string){
    this.service.search(filter).subscribe(
      res=>{
        this.service.list_asset = res;
      }
    );;
  } 

  onPageChange(event) {
    this.pagination.page = event;
    this.getFiltered();
  } 

  getFiltered() {
    this.service.getAssetList(this.searchText, this.pagination).subscribe(
      res=> {
        this.pagination.count = this.service.list_asset.length;
      },
      err=> {
        console.log(err);
      });
  }

  allStorage() {
    var values = [],
    keys = Object.keys(localStorage),
    i = keys.length;

    while ( i-- ) {
        values.push( localStorage.getItem(keys[i]) );
    }
    return values;
  }

  //**Buttons**//

  onSearch () {
    var filter = (<HTMLInputElement>document.getElementById("search")).value;
    if(filter!=null && filter!=""){
      this.searchList(filter);
    }     
    else{
      this.getAsset();
    }
  }

  onUpdate (asset: Asset) {
    this.populateForm(asset)
    this.router.navigate(['/tasinmazUpt']); 
  }

  onPrint () {
    let element = document.getElementById('table'); 
    const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, this.fileName);		    
  }

  onPost() {
    this.router.navigate(['/tasinmazAdd']);
  }

  onDelete(t_id: number) {
    if (confirm('Do you want to delete this record ?')) {
      this.service.deleteAsset(t_id).subscribe(res => {
          
        this.getAsset();
        },
        err => {
          console.log(err);
        })
      }
  }

}
