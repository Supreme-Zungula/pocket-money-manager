import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';
import { FamilyService } from 'src/app/services/family.service';
import { BankAccount } from 'src/app/models/BankAccount';
import { BankAccountService } from 'src/app/services/bank-account.service';

@Component({
  selector: 'app-family',
  templateUrl: './family.component.html',
  styleUrls: ['./family.component.css']
})
export class FamilyComponent implements OnInit {
  private _userToken: string;

  currentUser: User;
  usersList: User[] = [];
  familyMembers: User[] = [];
  bankDetails: BankAccount;
  panelOpenState: boolean = false;
  showForm: boolean = false;
  newUser: User = new User();
  confirmPassword: string;
  hide: boolean = true;
  canAddUser: boolean = false;

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
    private _router: Router,
    private _authService: AuthService,
    private _userService: UserService,
    private _familyService: FamilyService,
    private _formBuilder: FormBuilder,
    private _bankAccountService: BankAccountService,
  ) {

  }

  ngOnInit(): void {
    this._userToken = this._authService.getUserToken();
    let isLoggedIn = this._authService.isLoggedIn();

    if (isLoggedIn == false) {
      this._router.navigate(['login']);
    }

    this._userService.getUserByPhone$(this._userToken).subscribe(data => {
      this.currentUser = User.mapResponseToUser(data);
      if (this.currentUser.Role === "admin") {
        this.canAddUser = true;
      }
      this.familyMembers = this._familyService.getFamilyMembers(this.currentUser.FamilyId);
    });

    this.initFormControls();
  }
  
  validateUserInput() {
    if (this.validControls()) {
      this.newUser.Role = "user";
      this.newUser.FamilyId = this.currentUser.FamilyId;
      this.createNewUserAccount();
      this.familyMembers = this._familyService.getFamilyMembers(this.currentUser.FamilyId);
      this.showForm = false;
    }
  }
  openPanel(familyMember: User) {
    this.bankDetails = this._familyService.getFamilyMemberBankDetails(familyMember);
  }
  cancelForm() {
    this.showForm = false;
  }
  addNewMember() {
    this.showForm = true;
  }

  private createNewUserAccount() {
    this._userService.addNewUser$(this.newUser).subscribe({
      next(data) {
        console.log('New user succesfully added.')
      },
      error(err) { console.error("ERROR: new user could not be added.") },
      complete() { }
    });

    let newAccount: BankAccount = new BankAccount();

    newAccount.CustomerRef = this.currentUser.Id;
    newAccount.Balance = 0;
    this._bankAccountService.addBankAccount$(newAccount).subscribe({
      next(data) { console.log("new bank account succefully added.") },
      error(err) { console.error("ERROR: failed to add new bank account.") },
      complete() { }
    });
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
