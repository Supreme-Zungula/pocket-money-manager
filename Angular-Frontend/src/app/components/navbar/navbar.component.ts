import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  private phoneNumber: string;

  constructor(
    private _router: Router,
  ) {

  }

  async ngOnInit() {
    this.phoneNumber = await localStorage.getItem('userNumber');
  }

  navigateHome() {
    this._router.navigate(['/home', this.phoneNumber]);
  }

  navigateFamily() {

  }

  logout() {

  }
}
