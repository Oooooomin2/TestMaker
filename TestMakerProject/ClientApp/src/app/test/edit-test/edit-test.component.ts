import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Test } from '../test.model';
import { TestService } from '../test.service';

@Component({
  selector: 'app-edit-test',
  templateUrl: './edit-test.component.html',
  styleUrls: ['./edit-test.component.css']
})
export class EditTestComponent implements OnInit {
  test: Test = new Test();

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private service: TestService
  ) { }

  ngOnInit() {
    const testId = +this.activatedRoute.snapshot.paramMap.get('id');
    return this.service.getUsersTest(testId).subscribe(data => {
      console.log(data);
      this.test = data;
    });
  }

  updateTest() {
    return this.service.updateTest(this.test, this.test.testId).subscribe(res => {
      alert(res.toString());
      this.router.navigate(['/']);
    });
  }
}
