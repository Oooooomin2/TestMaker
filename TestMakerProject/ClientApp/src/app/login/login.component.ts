import { HttpClient } from '@angular/common/http';
import { error } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from './login.model';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  auth: Login = new Login();

  constructor(
    private router: Router,
    private service: LoginService
  ) { }

  ngOnInit() {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('/');
  }

  login() {
    return this.service.postLogin(this.auth)
      .subscribe((res: any) => {
        console.log(res);
        localStorage.setItem('token', res.token);
        localStorage.setItem('userId', res.userId);
        localStorage.setItem('userName', res.userName);
        this.router.navigateByUrl('/');
      },
        err => {
          alert('error');
        }
    );
  }
}
