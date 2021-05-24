import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Test } from '../test.model';
import { TestService } from '../test.service';

@Component({
  selector: 'app-delete-test',
  templateUrl: './delete-test.component.html',
  styleUrls: ['./delete-test.component.css']
})
export class DeleteTestComponent implements OnInit {
  test: Test = new Test();

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private service: TestService
  ) { }

  ngOnInit() {
    const testId = +this.activatedRoute.snapshot.paramMap.get('id');
    return this.service.getUsersTest(testId).subscribe(data => {
      this.test = data;
    });
  }

  deleteTest() {
    return this.service.deleteTest(this.test.testId).subscribe(res => {
      alert(res.toString());
      this.router.navigate(['/']);
    });
  }
}
