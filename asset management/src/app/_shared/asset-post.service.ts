import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { District } from './district.model';
import { Neighbourhood } from './neighbourhood.model';
import { AssetPost } from './asset-post.model';

@Injectable({
  providedIn: 'root'
})
export class AssetPostService {

  formDataAssetPost: AssetPost= {
    worth: null,
    attribute: null,
    adress: null,
    neighbourhood_id: null,
    user_id: null
  };
  
  readonly rootURL = 'https://localhost:5001/api';

  constructor(private http: HttpClient) { }

  postAsset(newAsset: AssetPost) {
    return this.http.post(this.rootURL + '/Asset', newAsset);
  }
}
