import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { District } from './district.model';

@Injectable({
  providedIn: 'root'
})
export class DistrictService {

  formDataDistrict: District = {
    district_id: null,
    district_name: null,
    province_id: null
  }

  readonly rootURL = 'https://localhost:5001/api';
  list_district : District[];

  constructor(private http: HttpClient) { }

  getDistrict(){   
    this.http.get(this.rootURL + '/District').toPromise().then(res => this.list_district = res as District[]);
  }
  searchRelative(id: number){
    this.http.get(this.rootURL + '/District/filter/'+ id).toPromise().then(res => this.list_district = res as District[]);
  }
}
