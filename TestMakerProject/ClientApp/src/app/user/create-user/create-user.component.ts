import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../user.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent implements OnInit {
  user: User = new User();

  constructor(
    private router: Router,
    private service: UserService
  ) { }

  ngOnInit() {
  }

  addUser() {
    return this.service.addUser(this.user).subscribe(res => {
      console.log(this.user);
      alert(res.toString());
      this.router.navigate(['/']);
    });
  }
}
