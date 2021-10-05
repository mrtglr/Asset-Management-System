import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PageRequest } from './page-request.model';
import { Pagination } from './pagination.model';
import { Asset } from './asset.model';

@Injectable({
  providedIn: 'root'
})
export class AssetService {
  
  formDataAsset: Asset= {
    asset_id: null,
    worth: null,
    attribute: null,
    adress: null,
    isActive: null,
    province_name: null,
    district_name: null,
    neighbourhood_name: null,
    neighbourhood_id: null,
    user_id: null
  };

  readonly rootURL = 'https://localhost:5001/api';
  
  list_asset : Asset[];
  currentUser_assetCount: number;
  currentUser_totalAssetWorth: number;
  currentUserID: number = +Object.keys(localStorage);   

  constructor(private http: HttpClient) { }

  putAsset(_id: number, _asset: Asset) {
    return this.http.put(this.rootURL + '/Asset/'+ _id, _asset);
  }
  deleteAsset(id: number) {
    return this.http.delete(this.rootURL + '/Asset/'+ id);
  }
  getAsset(): Observable<Asset[]>{   
    return this.http.get<Asset[]>(this.rootURL + '/Asset'); 
  }
  getUserAsset(user_id: number): Observable<Asset[]>{   
    return this.http.get<Asset[]>(this.rootURL + '/Asset/asset/' + user_id); 
  }
  search(filter: string): Observable<Asset[]>{
    return this.http.get<Asset[]>(this.rootURL + '/Asset/filter/'+ filter);
  }
  getAssetList(searchText:string, pagination:Pagination):Observable<PageRequest<Asset>> {
    return this.http.get<PageRequest<Asset>>(`${this.rootURL}?searchText=${searchText}
    &&page=${pagination.page}&&pageSize=${pagination.pageSize}`);
  }
}
