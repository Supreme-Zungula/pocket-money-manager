import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, Validators } from '@angular/forms';

import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private _currentUser: User;
  private _usersList: User[] = [];

  /* public members */
  public phoneNumber: string;
  public password: string;
  public networkError: boolean = false;

  /* controls for form input validation */
  public phoneControl: FormControl;
  public passwordControl: FormControl;

  /* Error messages */
  public phoneError: string;
  public passwordError: string;

  constructor(
    private _userService: UserService,
    private _router: Router,
    private _formBuilder: FormBuilder,
    private _authService: AuthService,
  ) {
    this.initFormControls();
  }

  async ngOnInit() {
    await this.getUsers();
  }

  public async checkCanLogin() {
    this._currentUser = this._usersList.find(user => user.Phone == this.phoneNumber);
    if (this._currentUser == null) {
      this.phoneError = "No user is registered with this number.";
    }
    else {
      if (this._currentUser.Password == this.password) {
        this._authService.setLoggedIn(true, this._currentUser.Phone);
        this._router.navigate(['home']);
      }
      this.phoneError = 'Invalid password';
    }
  }

  clearPhoneErrorMessage() {
    this.phoneError = '';
  }
  clearPasswordErrorMessage() {
    this.passwordError = "";
  }

  private getUsers() {
    this._userService.getAllUsers$().subscribe(data => {
      this._usersList = User.mapResponseToUsers(data);
    });
  }

  private initFormControls(): void {
    this.phoneControl = this._formBuilder.control(this.phoneNumber, [Validators.required, Validators.pattern('^[0-9]{10}$')]);
    this.passwordControl = this._formBuilder.control(this.password, [Validators.required, Validators.minLength(6), Validators.pattern('^[a-zA-Z0-9]+$')])
  }
}
