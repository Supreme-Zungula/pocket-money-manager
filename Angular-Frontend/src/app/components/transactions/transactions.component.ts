import { Component, OnInit } from '@angular/core';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';

import { User } from 'src/app/models/user';
import { BankAccount } from 'src/app/models/BankAccount';
import { UserService } from 'src/app/services/user.service';
import { BankAccountService } from 'src/app/services/bank-account.service';
import { TransactionService } from 'src/app/services/transaction.service';
import { AuthService } from 'src/app/services/auth.service';

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit {
  private _userNumber: string;
  private _bankAccount: BankAccount;
  private _bankAccountsList: BankAccount[];

  currentUser: User;
  depositAmount: string;
  matcher = new MyErrorStateMatcher();
  errorMessage: string = '';

  depositFormControl = new FormControl(this.depositAmount, [
    Validators.required,
    Validators.pattern(/^[0-9]\d*$/),
  ]);


  constructor(
    private _userService: UserService,
    private _bankAccountService: BankAccountService,
    private _transactionService: TransactionService,
    private _authService: AuthService,
    private _router: Router,
  ) { }

  ngOnInit(): void {
    if (this._authService.isLoggedIn() == false) {
      this._router.navigate(['login'])
    }

    this._userNumber = this._authService.getUserToken();
    this._bankAccountService.getAllAccounts$().subscribe(data => {
      this._bankAccountsList = BankAccount.mapResponseToBankAccountList(data);
    }).unsubscribe();

    this._userService.getUserByPhone$(this._userNumber).subscribe(data => {
      this.currentUser = User.mapResponseToUser(data);
    });

  }

  depositCash() {
    if (this.depositFormControl.invalid && (this.depositFormControl.dirty || this.depositFormControl.touched)) {
      this.errorMessage = "Please enter a valid deposit amount. No spaces or letters allowed."
    }

    this._bankAccount = this.getBankAccount();
    this._bankAccount.Balance += Number(this.depositAmount);
  }

  private getBankAccount(): BankAccount {
    return this._bankAccountsList.find(account => {
      return account.CustomerRef === this.currentUser.Id;
    })
  }
}
