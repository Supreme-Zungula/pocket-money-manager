import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Route } from '@angular/router';

import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  currentUser: User;
  phoneNumber: string;
  panelOpenState: boolean = true;
  accountDetailsPanel: boolean = true;

  constructor(
    private _router: Router,
    private _userService: UserService,
    private _authService: AuthService,
    private _activatedRoute: ActivatedRoute,
  ) { }

  async ngOnInit() {
    if (!this._authService.isLoggedIn()) {
      this._router.navigate(['login']);
    }

    this.phoneNumber = this._activatedRoute.snapshot.paramMap.get('phoneNumber');

    await this._userService.getUserByPhone(this.phoneNumber).subscribe({
      next(data) { },
      error(err) {
        if (err.status == 404) {
          localStorage.removeItem('loggedIn');
          this._router.navigate(['login']);
        }
      },
      complete() { }
    })
  }

}
