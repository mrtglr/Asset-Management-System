import { Component, OnInit } from '@angular/core';
import { AssetService } from '../_shared/asset.service';
import { Pagination } from '../_shared/pagination.model';
import { User } from '../_shared/user.model';
import { UserService } from '../_shared/user.service';
import * as XLSX from 'xlsx'; 
import { Asset } from '../_shared/asset.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styles: []
})
export class UserProfileComponent implements OnInit {

  constructor(public userService : UserService, public assetService : AssetService, private router: Router) { }

  currentUser_id : number = +Object.keys(localStorage); 
  currentUser : User;
  fileName = 'UserAssets.xlsx';
  searchText: string = '';
  pagination: Pagination = new Pagination(1,0,7, [10,20,30,40]);

  ngOnInit() { 
    this.findUser(this.currentUser_id);
    this.getList();
  }

  getList(){
    this.assetService.getUserAsset(this.currentUser_id).subscribe(
      res=>{
        this.assetService.list_asset = res;
        this.assetService.currentUser_assetCount = res.length;
        this.assetService.currentUser_totalAssetWorth = this.calculateAssetWorth();
      }
    )    
  }

  calculateAssetWorth(){
    var totalWorth = 0;
    for(let i=0; i<this.assetService.currentUser_assetCount; i++){
      totalWorth += this.assetService.list_asset[i].worth;
    }
    return totalWorth;
  }

  findUser(user_id: number){
    this.userService.getUser(user_id).subscribe(
      res =>{
        this.currentUser = Object.assign({}, res);
      }
    );
  }

  onPost() {
    this.router.navigate(['/tasinmazAdd']);
  }

  onPrint () {
    let element = document.getElementById('table'); 
    const ws: XLSX.WorkSheet =XLSX.utils.table_to_sheet(element);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
    XLSX.writeFile(wb, this.fileName);		    
  }

  searchList(filter: string){
    this.assetService.search(filter).subscribe(
      res=>{
          this.assetService.list_asset = res;
      }
    );;
  }

  populateForm(formAsset: Asset) {
    this.assetService.formDataAsset = Object.assign({}, formAsset);
  }

  onUpdate (asset: Asset) {
    this.populateForm(asset)
    this.router.navigate(['/tasinmazUpt']); 
  }

  onDelete(t_id: number) {
    if (confirm('Kaydı silmek istediğinizden emin misniz?')) {
      this.assetService.deleteAsset(t_id).subscribe(res => {
          
        this.getList();
        },
        err => {
          console.log(err);
        })
      }
  }

}
