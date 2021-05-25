import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { Choice } from '../choice.model';
import { Test } from '../test.model';
import { TestService } from '../test.service';

@Component({
  selector: 'app-detail-test',
  templateUrl: './detail-test.component.html',
  styleUrls: ['./detail-test.component.css']
})
export class DetailTestComponent implements OnInit {
  test: Test = new Test();
  questionNumber:number = 0;
  score: string = '';

  constructor(
    private activatedRoute: ActivatedRoute,
    private service: TestService
  ) { }

  ngOnInit() {
    const testId = +this.activatedRoute.snapshot.paramMap.get('id');
    return this.service.getUsersTest(testId).subscribe(data => {
      this.test = data;
      this.questionNumber = this.test.questions.length;
    });
  }

  scoreTest() {
    const testId = +this.activatedRoute.snapshot.paramMap.get('id');
    return this.service.scoreTest(this.test, testId).subscribe(data => {
      this.score = data;
    });
  }
}
