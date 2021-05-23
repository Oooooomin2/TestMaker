import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-settings-test',
  templateUrl: './settings-test.component.html',
  styleUrls: ['./settings-test.component.css']
})
export class SettingsTestComponent implements OnInit {
  title: string = '';
  number: string = '0';

  constructor(
    private router: Router
  ) { }

  ngOnInit() {
  }

  moveToCreate(): void {
    if (this.title === '' || this.number === '0') return;
    const param = {
      title: this.title,
      number: this.number
    };
    this.router.navigate(['/test/create'], { queryParams: param });
  }
}
