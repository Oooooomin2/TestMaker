import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TestService {
  readonly apiUrl = 'https://localhost:44312/api/test';

  constructor(private http: HttpClient) { }

  getUsersTest(): Observable<any[]> {
    return this.http.get<any>(this.apiUrl);
  }

  addTest(val:any) {
    return this.http.post<any>(this.apiUrl, val);
  }

  updateTest(val: any) {
    return this.http.put<any>(this.apiUrl,val);
  }

  deleteTest(val: any){
    return this.http.delete<any>(this.apiUrl, val);
  }
}
