import { Component, OnInit } from '@angular/core';

import { TransactionService } from 'src/app/services/transaction.service';
import { Transaction } from 'src/app/models/transaction';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { BankAccount } from 'src/app/models/BankAccount';
import { BankAccountService } from 'src/app/services/bank-account.service';

@Component({
  selector: 'app-transactions-list',
  templateUrl: './transactions-list.component.html',
  styleUrls: ['./transactions-list.component.css']
})
export class TransactionsListComponent implements OnInit {
  private _allTransacitons: Transaction[] = [];
  private _userNumber: string;
  private _currentUser: User;
  private _bankAccounts: BankAccount[] = [];
  private _myAccount: BankAccount;


  myTransactionsList: Transaction[];
  panelOpenState: boolean = false;
  transactionsPanelOpenState: boolean = true;

  constructor(
    private _authService: AuthService,
    private _transactionService: TransactionService,
    private _userService: UserService,
    private _bankAccountService: BankAccountService,
  ) { }

  ngOnInit(): void {
    this._userNumber = this._authService.getUserToken();
    this._userService.getUserByPhone$(this._userNumber).subscribe(data => {
      this._currentUser = User.mapResponseToUser(data);
    })

    this._bankAccountService.getAllAccounts$().subscribe(data => {
      this._bankAccounts = BankAccount.mapResponseToBankAccountList(data);
      this._myAccount = this._bankAccounts.find(account => {
        return account.CustomerRef === this._currentUser.Id;
      })

      this._transactionService.getAllTransactions$().subscribe(data => {
        this._allTransacitons = Transaction.mapResponseToTransactionList(data);
        this.myTransactionsList = this._allTransacitons.filter(transaction => {
          return transaction.AccountNo === this._myAccount.AccountNo;
        })
      });
    })

  }

  deleteTransaction(transaction: Transaction) {
    this._transactionService.deleteTransaction$(transaction.Id).subscribe({
      next(data) { console.info('Transaction deleted') },
      error(err) { console.error(`ERROR: ${err}`) },
      complete() { }
    });
    window.location.reload();
  }

}
