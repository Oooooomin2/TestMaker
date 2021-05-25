import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly apiUrl = 'https://localhost:44312/api/user';

  constructor(private http: HttpClient) { }

  getUser(id: number): any {
    return this.http.get<any>(this.apiUrl + '/' + id);
  }

  addUser(val:any) {
    return this.http.post<any>(this.apiUrl, val);
  }

  updateUser(val: any, id: number) {
    return this.http.put<any>(this.apiUrl + '/' + id, val);
  }

  deleteUser(id: number){
    return this.http.delete<any>(this.apiUrl + '/' + id);
  }
}
