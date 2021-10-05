import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserPost } from './user-post.model';

@Injectable({
  providedIn: 'root'
})
export class UserPostService {

  formDataUser: UserPost= {
    user_name: null,
    user_surname: null,
    user_email: null,
    user_role: null,
    user_adress: null,
    user_password: null,
    totalAssetWorth: null
  };
  
  readonly rootURL = 'https://localhost:5001/api';

  constructor(private http: HttpClient) { }

  postUser(newUser: UserPost) {
    return this.http.post(this.rootURL + '/User', newUser);
  }
}
