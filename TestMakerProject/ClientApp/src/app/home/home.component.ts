import { Component } from '@angular/core';
import { HomeIndex } from './home.model';
import { HomeService } from './home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  homeData: HomeIndex[] = [];

  constructor(
    private service: HomeService
  ) { }

  ngOnInit() {
    return this.service.getAllTest().subscribe(data => {
      this.homeData = data;
    });
  }
}
