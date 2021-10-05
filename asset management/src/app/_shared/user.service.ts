import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from './user.model';
import { PageRequest } from './page-request.model';
import { Pagination } from './pagination.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  formDataUser: User= {
    user_id: null,
    user_name: null,
    user_surname: null,
    user_email: null,
    user_role: null,
    isActive: null,
    user_adress: null,
    user_password: null,
    totalAssetWorth: null
  };
  
  readonly rootURL = 'https://localhost:5001/api';
  tmp_user: User;
  list_user : User[];

  constructor(private http: HttpClient) { }

  putUser(new_id: number, new_user: User) {
    return this.http.put(this.rootURL + '/User/'+ new_id, new_user);
  }
  deleteUser(id: number) {
    return this.http.delete(this.rootURL + '/User/'+ id);
  }
  getUser(id: number): Observable<User>{
    return this.http.get<User>(this.rootURL + '/User/'+ id);
  }
  createList(): Observable<User[]>{  
    return this.http.get<User[]>(this.rootURL + '/User'); 
  }
  search(filter: string): Observable<User[]>{
    return this.http.get<User[]>(this.rootURL + '/User/filter/'+ filter); 
  }
  logout(id: number): Observable<User[]>{
    return this.http.get<User[]>(this.rootURL + '/User/logout/'+ id); 
  }
  login(id: number): Observable<User[]>{
    return this.http.get<User[]>(this.rootURL + '/User/login/'+ id); 
  }
  getUserList(searchText:string, pagination:Pagination):Observable<PageRequest<User>> {
    return this.http.get<PageRequest<User>>(`${this.rootURL}?searchText=${searchText}
    &&page=${pagination.page}&&pageSize=${pagination.pageSize}`);
  }
}
