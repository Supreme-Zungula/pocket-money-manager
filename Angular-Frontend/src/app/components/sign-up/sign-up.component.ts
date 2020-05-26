import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})

export class SignUpComponent implements OnInit {
  // Private members
  private currentUser: User;

  // Public members
  public usersList: User[] = [];
  public newUser: User;
  public confirmPassword: string;

  /* control for form input validation */
  public firstnameControl: FormControl;
  public lastnameControl: FormControl;
  public phoneControl: FormControl;
  public passwordControl: FormControl;
  public confirmPasswordControl: FormControl;

  /* form error message containers */
  firstnameError: string;
  lastnameError: string;
  phoneError: string;
  passwordError: string;
  confirmPasswordError: string;
  passwordMatchError: string;
  numberExistError: string;

  constructor(
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _router: Router,
    private _authService: AuthService,
  ) {
    this.newUser = new User();
    this.initFormControls();
  }

  async ngOnInit() {
    this.getAllUsers();
  }

  getAllUsers() {
    this._userService.getAllUsers()
      .subscribe((data: any) => {
        this.usersList = User.mapResponseToUsers(data);
      });
  }

  addUser(newUser: User) {
    this._userService.addNewUser(newUser).subscribe((data) => {
      console.log("new user added.")
    });
  }

  validateUserInput() {
    if (this.validControls()) {
      this.newUser.Role = "admin";
      this.addUser(this.newUser);
      this._authService.setLoggedIn(true);
      this._router.navigate(['home', this.newUser.Phone]);
    }
  }

  private initFormControls() {
    this.firstnameControl = this._formBuilder.control(this.newUser.FirstName, [Validators.required, Validators.pattern('^[a-zA-Z]+$')]);
    this.lastnameControl = this._formBuilder.control(this.newUser.LastName, [Validators.required, Validators.pattern('^[a-zA-Z]+$')]);
    this.phoneControl = this._formBuilder.control(this.newUser.Phone, [Validators.required, Validators.pattern('^[0-9]{10}$')]);
    this.passwordControl = this._formBuilder.control(this.passwordControl, [Validators.required, Validators.minLength(6), Validators.pattern('^[a-zA-Z0-9/]+$')]);
    this.confirmPasswordControl = this._formBuilder.control(this.confirmPassword, [Validators.required, Validators.minLength(6), Validators.pattern('^[a-zA-Z0-9/]+$')]);
  }

  private checkPhoneExists(phoneNumber: string): boolean {
    let isFound: boolean = false;
    this.usersList.forEach(user => {
      if (user.Phone == phoneNumber) {
        isFound = true;
        return;
      }
    });
    return isFound;
  }

  private validControls(): boolean {
    if (this.firstnameControl.invalid && (this.firstnameControl.dirty || this.firstnameControl.touched)) {
      this.firstnameError = "First name can only have letters.";
      return false;
    }
    if (this.lastnameControl.invalid && (this.lastnameControl.dirty || this.lastnameControl.touched)) {
      this.lastnameError = "First name can only have letters.";
      return false;
    }
    if (this.phoneControl.invalid && (this.phoneControl.dirty || this.phoneControl.touched)) {
      this.phoneError = "Phone number must be digits only.";
      return false;
    }
    if (this.passwordControl.invalid && (this.passwordControl.dirty || this.passwordControl.touched)) {
      this.passwordError = "Password must at least 6 characters long and have letters and digits.";
      return false;
    }
    if (this.confirmPasswordControl.invalid && (this.confirmPasswordControl.dirty || this.confirmPasswordControl.touched)) {
      this.confirmPasswordError = "Password must at least 6 characters long and have letters and digits.";
      return false;
    }
    if (this.newUser.Password != this.confirmPassword) {
      this.passwordMatchError = "Passwords do not match.";
      return false;
    }
    if (this.checkPhoneExists(this.newUser.Phone) == true) {
      this.numberExistError = "Phone number already used.";
      return false;
    }
    return true;
  }
}
