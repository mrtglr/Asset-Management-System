import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Province } from '../_shared/province.model';
import { ProvinceService } from '../_shared/province.service';
import { District } from '../_shared/district.model';
import { DistrictService } from '../_shared/district.service';
import { Neighbourhood } from '../_shared/neighbourhood.model';
import { NeighbourhoodService } from '../_shared/neighbourhood.service';
import { Asset } from '../_shared/asset.model';
import { AssetService } from '../_shared/asset.service';
import { UserService } from '../_shared/user.service';

@Component({
  selector: 'app-upt-asset',
  templateUrl: './upt-asset.component.html',
  styles: []
})
export class UptAssetComponent implements OnInit {

  constructor(public service: AssetService, public userService: UserService, public service_neighbourhood: NeighbourhoodService,
    public service_district: DistrictService, public service_province: ProvinceService, private router: Router) { }

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
    this.updateAsset();
  }

  updateAsset(){
    var _asset:Asset, _neighbourhood: Neighbourhood, _district: District, _province: Province;
    this.service.formDataAsset.asset_id = this.service.formDataAsset.asset_id;
    this.service.formDataAsset.worth = +(<HTMLInputElement>document.getElementById("worth")).value;
    this.service.formDataAsset.attribute = (<HTMLInputElement>document.getElementById("attribute")).value;
    this.service.formDataAsset.adress = (<HTMLTextAreaElement>document.getElementById("adress")).value;
    this.service.formDataAsset.isActive = true;
    _neighbourhood = this.findNeighbourhood((<HTMLSelectElement>document.getElementById("neighbourhood")).value);
    this.service.formDataAsset.neighbourhood_id = _neighbourhood.neighbourhood_id;
    this.service.formDataAsset.neighbourhood_name = _neighbourhood.neighbourhood_name;
    _district = this.findDistrict(_neighbourhood.district_id);
    this.service.formDataAsset.district_name = _district.district_name;
    _province = this.findProvince(_district.province_id);
    this.service.formDataAsset.province_name = _province.province_name;
    if(this.allStorage().toString()=="true"){
      this.service.formDataAsset.user_id = +(<HTMLInputElement>document.getElementById("user_id")).value;
    }
    else{
      this.service.formDataAsset.user_id = +Object.keys(localStorage);   
    }

    _asset = this.service.formDataAsset;
    this.updateRecord(_asset);
  }

  updateRecord(asset: Asset) {
    if (confirm('Do you want to update this asset ?')) {
      this.service.putAsset(asset.asset_id, asset).subscribe(res => {
        (<HTMLFormElement>document.getElementById("upt_tasinmaz")).reset();
        this.service.getAsset().subscribe(
          res=>{
            this.service.list_asset = res;
          }
        );
        this.resetForm();
        if(this.allStorage().toString().match("false")){
          this.router.navigate(['/userProfile']);
        }
        else{
          this.router.navigate(['/tasinmazList']);
        }
      },
      err => {
        alert("The updated asset cannot be the same with old one !")
        console.log(err);
      })
    }
  }

  //**Helpers**//

  findNeighbourhood(name: string){

    for(let i = 0; i < this.service_neighbourhood.list_neighbourhood.length; i++){
      
      if(name.match(this.service_neighbourhood.list_neighbourhood[i].neighbourhood_name)){
        
        return this.service_neighbourhood.list_neighbourhood[i];
      }
    }
  }

  findDistrict(id: number){

    for(let i = 0; i < this.service_district.list_district.length; i++){
      
      if(id == (this.service_district.list_district[i].district_id)){
        
        return this.service_district.list_district[i];
      }
    }
  }

  findProvince(id: number){

    for(let i = 0; i < this.service_province.list_province.length; i++){
      
      if(id == (this.service_province.list_province[i].province_id)){
        
        return this.service_province.list_province[i];
      }
    }
  }

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

  backToList() {
    if(confirm('Do you want to exit without update asset ?')){
      this.resetForm();
      if(this.allStorage().toString().match("false")){
        this.router.navigate(['/userProfile']);
      }
      else{
        this.router.navigate(['/tasinmazList']);
      }
    }
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

  resetForm() {
    this.service.formDataAsset= {
      asset_id: 0,
      worth: null,
      attribute: '',
      adress: '',
      isActive: null,
      province_name: '',
      district_name: '',
      neighbourhood_name: '',
      neighbourhood_id: 0,
      user_id: 0
    };
  }

  disableOpt(id: string) {
    if((<HTMLOptionElement>document.getElementById(id))!=null)
      (<HTMLOptionElement>document.getElementById(id)).remove();
  }

}
