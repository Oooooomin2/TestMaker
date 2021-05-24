import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  readonly apiUrl = 'https://localhost:44312/api/home';

  constructor(private http: HttpClient) { }

  getAllTest(): Observable<any[]> {
    return this.http.get<any>(this.apiUrl);
  }
}
