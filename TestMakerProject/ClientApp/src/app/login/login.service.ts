import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  readonly apiUrl = 'https://localhost:44312/api/account/login';

  constructor(private http: HttpClient, private router: Router) { }

  postLogin(val: any) {
    return this.http.post<any>(this.apiUrl, val)
  }
}
