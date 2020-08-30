import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  additionMode = false;

  constructor(private router: Router) { }

  // tslint:disable-next-line: typedef
  ngOnInit() {
  }

  additionToggle() {
    this.router.navigate(['category']);
    // this.additionMode = true;
  }

  cancelAdditionMode(additionMode: boolean) {
    this.additionMode = additionMode;
  }

}
