import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProvinceService } from '../_shared/province.service';
import { DistrictService } from '../_shared/district.service';
import { NeighbourhoodService } from '../_shared/neighbourhood.service';
import { AssetPost } from '../_shared/asset-post.model';
import { AssetPostService } from '../_shared/asset-post.service';
import { AssetService } from '../_shared/asset.service';
import { UserService } from '../_shared/user.service';

@Component({
  selector: 'app-add-asset',
  templateUrl: './add-asset.component.html',
  styles: []
})
export class AddAssetComponent implements OnInit {

  constructor(public service: AssetService, public userService: UserService, public service_tas: AssetPostService, public service_province: ProvinceService, 
    public service_district: DistrictService, public service_neighbourhood: NeighbourhoodService, private router: Router) {}

    currentUserRole: string = this.allStorage().toString();

  ngOnInit() {
    this.getKullanici();
    this.service_province.getProvince();
    (<HTMLSelectElement>document.getElementById("district")).disabled = true;
    (<HTMLSelectElement>document.getElementById("neighbourhood")).disabled = true;
  }

  getKullanici() {
    this.userService.createList().subscribe(
      res=>{
        this.userService.list_user = res;
      }
    );
  }

  onSubmit() {   
    this.insertAsset();     
  }

  insertAsset(){    
    //debugger;
    var newTasinmaz: AssetPost;
    this.service_tas.formDataAssetPost.worth = +(<HTMLInputElement>document.getElementById("worth")).value;
    this.service_tas.formDataAssetPost.attribute = (<HTMLInputElement>document.getElementById("attribute")).value;
    this.service_tas.formDataAssetPost.adress = (<HTMLTextAreaElement>document.getElementById("adress")).value;
    this.service_tas.formDataAssetPost.neighbourhood_id = +(<HTMLSelectElement>document.getElementById("neighbourhood")).value;
    if(this.allStorage().toString()=="true"){
      this.service_tas.formDataAssetPost.user_id = +(<HTMLInputElement>document.getElementById("user_id")).value;
    }
    else{
      this.service_tas.formDataAssetPost.user_id = +Object.keys(localStorage);   
    }

    newTasinmaz = this.service_tas.formDataAssetPost;
    this.insertRecord(newTasinmaz);
  }

  insertRecord(newTasinmaz: AssetPost) {
    if (confirm('Do you want add this asset ?')) {
      this.service_tas.postAsset(newTasinmaz).subscribe(
        res => {
          (<HTMLFormElement>document.getElementById("insrt_asset")).reset();
          this.service.getAsset().subscribe(
            res=>{
              this.service.list_asset = res;
            }
          );
          if(this.allStorage().toString().match("false")){
            this.router.navigate(['/userProfile']);
          }
          else{
            this.router.navigate(['/tasinmazList']);
          }
        },
        err => {
          alert("Asset is already exist!")
          console.log(err);
        }
      )
    }   
  }

  //**Helpers**/

  getDistrict() {
    (<HTMLSelectElement>document.getElementById("district")).disabled = false;

    (<HTMLSelectElement>document.getElementById("district")).value = null;
    (<HTMLSelectElement>document.getElementById("neighbourhood")).value = null;
    this.service_district.searchRelative(+(<HTMLSelectElement>document.getElementById("province")).value)
  }

  getNeighbourhood() {
    (<HTMLSelectElement>document.getElementById("neighbourhood")).disabled = false;

    (<HTMLSelectElement>document.getElementById("neighbourhood")).value = null;
    this.service_neighbourhood.searchRelative(+(<HTMLSelectElement>document.getElementById("district")).value)
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

  backtoList() {
    if(confirm('Do you want to exit without adding new asset ?')){
      if(this.allStorage().toString().match("false")){
        this.router.navigate(['/userProfile']);
      }
      else{
        this.router.navigate(['/tasinmazList']);
      }
    }
  }
}
