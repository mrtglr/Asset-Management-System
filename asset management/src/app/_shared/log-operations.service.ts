import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LogOperations } from './log-operations.model';
import { PageRequest } from './page-request.model';
import { Pagination } from './pagination.model';

@Injectable({
  providedIn: 'root'
})
export class LogOperationsService {

  readonly rootURL = 'https://localhost:5001/api';

  constructor(private http: HttpClient) { }

  list_logOperations: LogOperations[];

  createLogs() : Observable<LogOperations[]>{   
   return this.http.get<LogOperations[]>(this.rootURL + '/LogOperations'); 
  }
  search(filter: string) : Observable<LogOperations[]>{
    return this.http.get<LogOperations[]>(this.rootURL + '/LogOperations/filter/'+ filter); 
  }
  getLogs(searchText:string, pagination:Pagination):Observable<PageRequest<LogOperations>> {
    return this.http.get<PageRequest<LogOperations>>(`${this.rootURL}?searchText=${searchText}
    &&page=${pagination.page}&&pageSize=${pagination.pageSize}`);
  }
}
