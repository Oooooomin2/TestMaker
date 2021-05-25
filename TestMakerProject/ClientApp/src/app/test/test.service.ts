import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Test } from './test.model';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  readonly apiUrl = 'https://localhost:44312/api/test';

  constructor(private http: HttpClient) { }

  getUsersTest(id: number): any {
    return this.http.get<any>(this.apiUrl + '/' + id);
  }

  addTest(val:any) {
    return this.http.post<any>(this.apiUrl, val);
  }

  updateTest(val: any, id: number) {
    return this.http.put<any>(this.apiUrl + '/' + id, val);
  }

  deleteTest(id: number){
    return this.http.delete<any>(this.apiUrl + '/' + id);
  }

  scoreTest(val: Test, id: number) {
    return this.http.post<any>(this.apiUrl + '/score/' + id, val);
  }
}
