import { Component, OnInit } from '@angular/core';

import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';
import { Transaction } from 'src/app/models/transaction';
import { TransactionService } from 'src/app/services/transaction.service';
import { BankAccount } from 'src/app/models/BankAccount';
import { BankAccountService } from 'src/app/services/bank-account.service';
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
  public userData: User;
  // public transactionsList: Transaction[];
  // public accountsList: BankAccount[] = [];

  constructor(
    private _userService: UserService,
    private _transactionService: TransactionService,
    private _bankAccountService: BankAccountService,
  ) { }

  async ngOnInit() {


    let user: User = new User();
    user.FirstName = "Tasha";
    user.LastName = "Godspell";
    user.Password = "Spaz-2";
    user.Phone = "0121111111";
    user.Role = "Son";


    // this.addUser(user);
    // this._userService.updateUser(user).subscribe();
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
      console.log(data)
    });
  }

  /* async getAllAccounts() {
    await this._bankAccountService.getAllAccounts().subscribe((data) => {
      this.accountsList = BankAccount.mapResponseToBankAccountList(data)
    });
  } */


}
