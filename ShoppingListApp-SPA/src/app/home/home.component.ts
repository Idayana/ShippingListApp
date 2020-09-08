import { Component, OnInit, Input } from '@angular/core';
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

  additionProdToggle() {
    this.router.navigate(['product']);
    // this.additionMode = true;
  }

  additionToggle() {
    this.router.navigate(['category']);
    // this.additionMode = true;
  }

  cancelAdditionMode(additionMode: boolean) {
    this.additionMode = additionMode;
  }

}
