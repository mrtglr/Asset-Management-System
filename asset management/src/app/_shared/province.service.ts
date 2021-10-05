import { Injectable } from '@angular/core';
import { Province } from './province.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {

  formDataProvince: Province = {
    province_id: null,
    province_name: null
  }

  readonly rootURL = 'https://localhost:5001/api';
  list_province : Province[];

  constructor(private http: HttpClient) { }

  getProvince(){   
    this.http.get(this.rootURL + '/Province').toPromise().then(res => this.list_province = res as Province[]);
  }
  
}
