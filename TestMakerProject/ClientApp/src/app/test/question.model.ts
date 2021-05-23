import { Choice } from "./choice.model";

export class Question {
  questionId:number;
  questionText:string = '';
  testId: number;
  choices: Choice[] = new Array<Choice>(4);
}
