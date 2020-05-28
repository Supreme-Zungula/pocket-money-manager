import { Component, OnInit } from '@angular/core';

import { User } from 'src/app/models/user';
import { BankAccount } from 'src/app/models/BankAccount';
import { UserService } from 'src/app/services/user.service';
import { BankAccountService } from 'src/app/services/bank-account.service';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account-details',
  templateUrl: './account-details.component.html',
  styleUrls: ['./account-details.component.css']
})
export class AccountDetailsComponent implements OnInit {
  private _phoneNumber: string;
  private _bankAccounts: BankAccount[];

  currentUser: User = new User();
  bankAccount: BankAccount = new BankAccount() ;

  constructor(
    private _userService: UserService,
    private _bankAccountService: BankAccountService,
    private _authService: AuthService,
    private _router: Router,
  ) { }

  ngOnInit(): void {
    if (this._authService.isLoggedIn() == false) {
      this._router.navigate(['login']);
    }

    this._phoneNumber = this._authService.getUserToken();

    this._bankAccountService.getAllAccounts$().subscribe(data => {
      this._bankAccounts = BankAccount.mapResponseToBankAccountList(data);
    }) 
    
    this.getUserDetails();
    this.getUserBankDetails();
  }

  private getUserDetails() {

    this._userService.getUserByPhone$(this._phoneNumber).subscribe(data => {
      this.currentUser = User.mapResponseToUser(data);
    });
  }

  private async getUserBankDetails() {
    this._bankAccountService.getAllAccounts$().subscribe(data => {
      this._bankAccounts = BankAccount.mapResponseToBankAccountList(data);

      this.bankAccount = this._bankAccounts.find(account => {
        return account.CustomerRef === this.currentUser.Id
      });
    })
  }
}
