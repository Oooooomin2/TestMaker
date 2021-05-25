import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../user.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  user: User = new User();

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private service: UserService
  ) { }

  ngOnInit() {
    const userId = +this.activatedRoute.snapshot.paramMap.get('id');
    return this.service.getUser(userId).subscribe(data => {
      this.user = data;
    });
  }

  updateUser() {
    return this.service.updateUser(this.user, this.user.userId).subscribe(res => {
      console.log(this.user);
      alert(res.toString());
      this.router.navigate(['/']);
    });
  }
}
