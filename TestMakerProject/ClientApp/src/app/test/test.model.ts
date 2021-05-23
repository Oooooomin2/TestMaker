import { Question } from "./question.model";

export class Test {
  testId:number;
  title:string = '';
  number: number = 0;
  createdTime: Date;
  updatedTime: Date;
  userId: number = 1;
  questions: Question[];
}
