import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Neighbourhood } from './neighbourhood.model';

@Injectable({
  providedIn: 'root'
})
export class NeighbourhoodService {

  formDataNeighbourhood: Neighbourhood = {
    neighbourhood_id: null,
    neighbourhood_name: null,
    district_id: null
  }

  readonly rootURL = 'https://localhost:5001/api';
  list_neighbourhood : Neighbourhood[];

  constructor(private http: HttpClient) { }

  getNeighbourhood(){   
    this.http.get(this.rootURL + '/Neighbourhood').toPromise().then(res => this.list_neighbourhood = res as Neighbourhood[]);
  }
  searchRelative(id: number){
    this.http.get(this.rootURL + '/Neighbourhood/filter/'+ id).toPromise().then(res => this.list_neighbourhood = res as Neighbourhood[]);
  }
}
