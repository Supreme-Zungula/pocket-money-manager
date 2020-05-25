import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(
    private _router: Router,
    private _userService: UserService,
    private _authService: AuthService,
  ) { }

  ngOnInit(): void {
    if (!this._authService.isLoggedIn()) {
      this._router.navigate(['login']);
    }
  }

}
