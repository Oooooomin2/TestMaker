import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router  } from '@angular/router';
import { Choice } from '../choice.model';
import { Question } from '../question.model';
import { Test } from '../test.model';
import { TestService } from '../test.service';

@Component({
  selector: 'app-create-test',
  templateUrl: './create-test.component.html',
  styleUrls: ['./create-test.component.css']
})
export class CreateTestComponent implements OnInit {
  test: Test = new Test();

  constructor(
    private activatedRouter: ActivatedRoute,
    private router: Router,
    private service: TestService
  ) { }

  ngOnInit() {
    this.activatedRouter.queryParams
      .subscribe(params => {
        this.test.title = params.title;
        this.test.number = Number(params.number);
        this.test.questions = new Array<Question>(Number(params.number));
        for (let i = 0; i < this.test.questions.length; i++) {
          this.test.questions[i] = new Question();
          for (let j = 0; j < this.test.questions[i].choices.length; j++) {
            this.test.questions[i].choices[j] = new Choice();
            this.test.questions[i].choices[j].choiceText = '';
          }
          this.test.questions[i].questionText = '';
        };
      });
  }

  arrayNumberLength(number: number): any[] {
    return [...Array(Number(number))];
  }

  addTest() {
    return this.service.addTest(this.test).subscribe(res => {
      alert(res.toString());
      this.router.navigate(['/']);
    });
  }
}
