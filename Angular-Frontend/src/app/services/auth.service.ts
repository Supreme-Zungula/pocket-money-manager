import { Injectable } from '@angular/core';

import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private _loggedInStatus: boolean = JSON.parse(localStorage.getItem('loggedIn') || 'false');
  
  constructor(
    private _userService: UserService,
  ) { }

  setLoggedIn( value : boolean) {
    this._loggedInStatus = value;
    localStorage.setItem('loggedIn', 'true');
  }

  isLoggedIn() : boolean {
    return JSON.parse(localStorage.getItem('loggedIn') || this._loggedInStatus.toString());
  }

  getUserDetails(phone: string) {
    return this._userService.getUserByPhone(phone);
  }
}
