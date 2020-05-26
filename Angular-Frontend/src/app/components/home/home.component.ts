import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user';
import { BankAccountService } from 'src/app/services/bank-account.service';
import { BankAccount } from 'src/app/models/BankAccount';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  private _bankAccountsList: BankAccount[] = [];

  currentUser: User;
  bankAccount: BankAccount;
  phoneNumber: string;
  panelOpenState: boolean = true;
  accountDetailsPanel: boolean = true;

  constructor(
    private _router: Router,
    private _userService: UserService,
    private _authService: AuthService,
    private _bankAccountService: BankAccountService,
  ) { }

  async ngOnInit() {
    this.phoneNumber = this._authService.getUserToken();

    if (!this._authService.isLoggedIn() || this.phoneNumber == undefined) {
      this._router.navigate(['login']);
    }

    this.checkUserFromDB();
    this.getUserBankDetails();
  }





  private checkUserFromDB() {
    this._userService.getUserByPhone$(this.phoneNumber).subscribe({
      next(data) { },
      error(err) {
        if (err.status == 404) {
          localStorage.removeItem('loggedIn');
          localStorage.removeItem('userNumber');
          this._router.navigate(['login']);
        }
      },
      complete() { }
    });

    this._userService.getUserByPhone$(this.phoneNumber).subscribe(data => {
      this.currentUser = User.mapResponseToUser(data);
    });
  }

  private getUserBankDetails() {
    this._bankAccountService.getAllAccounts$().subscribe(data => {
      this._bankAccountsList = BankAccount.mapResponseToBankAccountList(data);

      this._bankAccountsList.forEach(account => {
        if (account.CustomerRef == this.currentUser.Id) {
          this.bankAccount = account;
        }
      });
    })
  }
}
