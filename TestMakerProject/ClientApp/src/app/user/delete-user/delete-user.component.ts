import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../user.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnInit {
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

  deleteUser() {
    return this.service.deleteUser(this.user.userId).subscribe(res => {
      alert(res.toString());
      localStorage.removeItem('token');
      localStorage.removeItem('userId');
      localStorage.removeItem('userName');
      this.router.navigate(['/']);
    });
  }
}
