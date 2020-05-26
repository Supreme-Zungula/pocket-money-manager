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

  setLoggedIn(value: boolean, phone: string) {
    this._loggedInStatus = value;
    localStorage.setItem('loggedIn', 'true');
    localStorage.setItem('_userToken', phone);
  }

  isLoggedIn(): boolean {
    return JSON.parse(localStorage.getItem('loggedIn') || this._loggedInStatus.toString());
  }

  getUserToken(): string {
    return localStorage.getItem('_userToken');
  }
  clearSession() {
    localStorage.removeItem('loggedIn');
    localStorage.removeItem('userToken');
  }
}
