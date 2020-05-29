import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';
import { FamilyService } from 'src/app/services/family.service';
import { BankAccount } from 'src/app/models/BankAccount';
import { BankAccountService } from 'src/app/services/bank-account.service';
import { FamilyMember } from 'src/app/models/FamilyMember';

@Component({
  selector: 'app-family',
  templateUrl: './family.component.html',
  styleUrls: ['./family.component.css']
})
export class FamilyComponent implements OnInit {
  private _userToken: string;

  currentUser: User;
  newUser: User = new User();
  usersList: User[] = [];
  accountsList: BankAccount[] = [];
  newFamilyMember: FamilyMember;
  familyMembers: FamilyMember[] = [];
  bankDetails: BankAccount;
  panelOpenState: boolean = false;
  showForm: boolean = false;
  confirmPassword: string;
  userRelation: string;
  hide: boolean = true;
  canAddUser: boolean = false;
  canDeleteUser: boolean = false;

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
        this.canDeleteUser = true;
      }
      this.getFamilyMembers();
    });

    this.initFormControls();
    this.getBankAcccounts();
  }

  validateUserInput() {
    if (this.validControls()) {
      this.newUser.FamilyId = this.currentUser.FamilyId;
      this.newUser.Role = "ordinary";
      this.createNewUserAccount();
      this.createFamilyMember();
      this.getFamilyMembers();
      this.getBankAcccounts();
      this.showForm = false;
    }
  }

  deleteFamilyMember(member: FamilyMember) {
    this._familyService.deleteFamilyMember$(member.Phone).subscribe({
      next(data) { console.log("Family deleted") },
      error(err) {
        console.error(`ERROR: ${err}`);
      },
      complete() { }
    });
    this.getFamilyMembers();
    this._userService.deleteUser$(member.Phone).subscribe({
      next(data) { console.log("User deleted.") },
      error(err) { console.log(`ERROR: ${err}`) },
      complete() { }
    });

    window.location.reload();
  }

  openPanel(familyMember: User) {
    this.bankDetails = this.accountsList.find(account => {
      return account.CustomerRef = familyMember.Id;
    })
  }
  cancelForm() {
    this.showForm = false;
  }

  addNewMember() {
    this.showForm = true;
  }

  private getBankAcccounts() {
    this._bankAccountService.getAllAccounts$().subscribe(data => {
      this.accountsList = BankAccount.mapResponseToBankAccountList(data);
    })
  }
  private async getFamilyMembers() {
    this._familyService.getAllFamilyMember$(this.currentUser.FamilyId).subscribe(data => {
      this.familyMembers = FamilyMember.mapResponseToFamilyMembersList(data)
      this.familyMembers = this.familyMembers.filter(member => {
        return member.Phone != this.currentUser.Phone;
      })
    });
  }

  private createNewUserAccount() {
    this._userService.addNewUser$(this.newUser).subscribe(data => {
      let userData: User = User.mapResponseToUser(data)
      let newAccount: BankAccount = new BankAccount();

      newAccount.CustomerRef = userData.Id;
      newAccount.Balance = 0;
      this._bankAccountService.addBankAccount$(newAccount).subscribe({
        next(data) { console.log("new bank account succefully added.") },
        error(err) { console.error("ERROR: failed to add new bank account.") },
        complete() { }
      });
    });
  }

  private createFamilyMember() {
    this.newFamilyMember = new FamilyMember();
    this.newFamilyMember.FamilyId = this.newUser.FamilyId;
    this.newFamilyMember.FirstName = this.newUser.FirstName;
    this.newFamilyMember.LastName = this.newUser.LastName;
    this.newFamilyMember.Phone = this.newUser.Phone;
    this.newFamilyMember.Relationship = this.userRelation;

    this._familyService.addFamilyMember$(this.newFamilyMember).subscribe({
      next(data) {
        console.log("Added new family member.");
      },
      error(err) { console.error("ERROR: could not add new family member.") },
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
      this.lastnameError = "Last name can only have letters.";
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
