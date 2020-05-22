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
  private _newUser: User;

  // Public members
  public usersList: User[] = [];
  public userData: User;
  public transactionsList: Transaction[];
  public accountList: BankAccount[];
  constructor(
    private _userService: UserService,
    private _transactionService: TransactionService,
    private _bankAccountService: BankAccountService,
  ) {

    this._newUser = new User();
    this._newUser.FirstName = "TestName1";
    this._newUser.LastName = 'TestSurname1';
    this._newUser.Password = 'Password';
    this._newUser.Phone = '0123456789';

  }

  async ngOnInit() {

    this.getAllUsers();

    /* let newTransaction = new Transaction();
    newTransaction.AccountNo = "1242";
    newTransaction.Deposit = 1500;
    newTransaction.Withdrawal = 0;
    newTransaction.Date = new Date();
    newTransaction.Reference = "Account creation";

    console.info(`newTransaction = ${newTransaction}`);
    await this._transactionService.addTransaction(newTransaction); */
    await this._bankAccountService.getAllAccounts().subscribe((data) => {
      this.accountList = BankAccount.mapResponseToBankAccountList(data)
    });
    
    let newAccount = new BankAccount();
    newAccount.Balance = 2340;
    newAccount.CustomerRef = "created new account";
    this._bankAccountService.addBankAccount(newAccount);

    this._bankAccountService.getAccountById("5ec7decaf95c0d6a143ab88f")
    .subscribe( (data : any) => {
      let account = BankAccount.mapResponseToBankAccount(data)
    });
    

    await this._transactionService.getAllTransactions().subscribe(data => {
      this.transactionsList = Transaction.mapResponseToTransactionList(data)
    });


    // this._userService.addNewUser(this._newUser).subscribe((user) => {
    //   this.userData = user;
    // });

    /*  this._userService.getUserByPhone("0123456789")
       .subscribe((data: User) => {
         this.userData = User.mapResponseToUser(data);
       }); */
  }

  getAllUsers() {
    this._userService.getAllUsers()
      .subscribe((data: any) => {
        this.usersList = User.mapResponseToUsers(data);
      });
  }
}
