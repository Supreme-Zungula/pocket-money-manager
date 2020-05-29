import { Component, OnInit } from '@angular/core';
import { ErrorStateMatcher } from '@angular/material/core';
import { Router } from '@angular/router';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import { User } from 'src/app/models/user';
import { BankAccount } from 'src/app/models/BankAccount';
import { UserService } from 'src/app/services/user.service';
import { BankAccountService } from 'src/app/services/bank-account.service';
import { TransactionService } from 'src/app/services/transaction.service';
import { AuthService } from 'src/app/services/auth.service';
import { TransactionType } from 'src/app/enums/TransactionType';
import { Transaction } from 'src/app/models/transaction';

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
  private _familyMembers: User[];
  private _recipient: User;

  currentUser: User;
  transactionAmount: string;
  matcher = new MyErrorStateMatcher();
  errorMessage: string = '';

  showDepositForm: boolean = false;
  showWithdrawalForm: boolean = false;
  showTransferForm: boolean = false;
  showTransactionsList: boolean = false;

  depositFormControl = new FormControl(this.transactionAmount, [
    Validators.required,
    Validators.pattern(/^[0-9]\d*$/),
  ]);

  myControl = new FormControl();
  options: string[] = ['One', 'Two', 'Three'];
  filteredOptions: Observable<User[]>;

  constructor(
    private _userService: UserService,
    private _bankAccountService: BankAccountService,
    private _transactionService: TransactionService,
    private _authService: AuthService,
    private _router: Router,
  ) { }

  ngOnInit(): void {
    this._userNumber = this._authService.getUserToken();
    if (this._authService.isLoggedIn() == false || this._userNumber == undefined) {
      this._router.navigate(['login'])
    }
    this._userService.getUserByPhone$(this._userNumber).subscribe(data => {
      this.currentUser = User.mapResponseToUser(data);
    });

    this._userService.getAllUsers$().subscribe(data => {
      this._familyMembers = User.mapResponseToUsers(data);
      this._familyMembers = this._familyMembers.filter(member => {
        return member.Id !== this.currentUser.Id;
      })
    })

    this._bankAccountService.getAllAccounts$().subscribe(data => {
      this._bankAccountsList = BankAccount.mapResponseToBankAccountList(data);

      this._bankAccount = this._bankAccountsList.find(account => {
        return account.CustomerRef === this.currentUser.Id;
      })
    });

    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  transferFunds() {
    if (this.depositFormControl.invalid && (this.depositFormControl.dirty || this.depositFormControl.touched)) {
      this.errorMessage = "Please enter a valid amount. No spaces or letters allowed."
    }
    else if (this._bankAccount.Balance <= parseFloat(this.transactionAmount)) {
      this.errorMessage = "Insufficient funds."
    }
    else {
      let fromAccount: BankAccount = this._bankAccountsList.find(account => {
        return account.CustomerRef === this.currentUser.Id;
      })

      let toAccount: BankAccount = this._bankAccountsList.find(account => {
        return account.CustomerRef === this._recipient.Id;
      })

      this.performTransactions(fromAccount, toAccount);

      // let fromReference = `Payment: -R${this.transactionAmount} transfer money to ${this._recipient.FirstName} ${this._recipient.LastName}`;
      // let toReference = `Deposit: +R${this.transactionAmount} received money from ${this.currentUser.FirstName} ${this.currentUser.LastName}`;
      // this._withdrawCash(fromAccount, parseFloat(this.transactionAmount), fromReference);
      // this._depositCash(toAccount, parseFloat(this.transactionAmount), toReference);
    }
  }

  depositCash() {
    let depositAmount = parseFloat(this.transactionAmount);
    if (this.depositFormControl.invalid && (this.depositFormControl.dirty || this.depositFormControl.touched)) {
      this.errorMessage = "Please enter a valid deposit amount. No spaces or letters allowed."
    }
    else {
      this._bankAccount.Balance += depositAmount;
      this._bankAccountService.updateAccount$(this._bankAccount.Id, this._bankAccount).subscribe({
        next(data) { console.log('Accounts successfully updated.') },
        error(err) { console.error(`ERROR: ${err}`) },
        complete() { window.location.reload() }
      });

      let reference = `Cash deposit +R${depositAmount}`;
      this.createTransaction(this._bankAccount, depositAmount, reference, TransactionType.DEPOSIT);
      this.showDepositForm = false;
      this.transactionAmount = '';
    }
  }

  withdrawCash() {
    let amount = parseFloat(this.transactionAmount);

    if (this.depositFormControl.invalid && (this.depositFormControl.dirty || this.depositFormControl.touched)) {
      this.errorMessage = "Please enter a valid withdrawal amount. No spaces or letters allowed."
    }
    else {
      if (amount > this._bankAccount.Balance) {
        this.errorMessage = "Not enough founds to perform this transaction.";
      }
      else {
        this._bankAccount.Balance -= amount;
        this._bankAccountService.updateAccount$(this._bankAccount.Id, this._bankAccount).subscribe({
          next(data) { console.log('Accounts successfully updated.') },
          error(err) { console.error(`ERROR: ${err}`) },
          complete() { window.location.reload() }
        });
        let reference = `Cash withdrawal -R${parseFloat(this.transactionAmount)}`;
        this.createTransaction(this._bankAccount, amount, reference, TransactionType.WITHDRAWAL);
        this.showWithdrawalForm = false;
        this.transactionAmount = '';
      }
    }
  }

  createTransaction(account: BankAccount, amount: number, reference: string, type: TransactionType) {
    let newTransaction: Transaction = new Transaction();
    newTransaction.AccountNo = account.AccountNo;
    newTransaction.Date = new Date();
    newTransaction.Reference = reference;
    if (type === TransactionType.DEPOSIT) {
      newTransaction.Deposit = amount;
      newTransaction.Withdrawal = 0;
    }
    else {
      newTransaction.Withdrawal = amount;
      newTransaction.Deposit = 0;
    }

    this._transactionService.addTransaction$(newTransaction).subscribe({
      next(data) { console.info('Transaction successful.') },
      error(err) { console.error(`ERROR: ${err}`) },
      complete() { }
    })
  }

  storeRecipient(recipient: User) {
    this._recipient = recipient;
  }
  cancelForm() {
    this.showDepositForm = false;
    this.showWithdrawalForm = false;
    this.showTransferForm = false;
    this.showTransactionsList = false;
  }

  showRelevantForm(action: string) {
    switch (action) {
      case 'Deposit':
        this.showDepositForm = !this.showDepositForm;
        this.showWithdrawalForm = false;
        this.showTransferForm = false;
        this.showTransactionsList = false;
        break;
      case 'Withdraw':
        this.showDepositForm = false
        this.showWithdrawalForm = !this.showWithdrawalForm;
        this.showTransferForm = false;
        this.showTransactionsList = false;
        break;
      case 'Transfer':
        this.showWithdrawalForm = false;
        this.showDepositForm = false
        this.showTransferForm = !this.showTransferForm;
        this.showTransactionsList = false;
        break;
      case 'Transactions':
        this.showWithdrawalForm = false;
        this.showDepositForm = false
        this.showTransferForm = false;
        this.showTransactionsList = !this.showTransactionsList;
        break;
      default:
        break;
    }
  }
  private performTransactions(fromAccount: BankAccount, toAccount: BankAccount) {
    let fromReference = `Payment: -R${this.transactionAmount} transfer money to ${this._recipient.FirstName} ${this._recipient.LastName}`;
    let toReference = `Deposit: +R${this.transactionAmount} received money from ${this.currentUser.FirstName} ${this.currentUser.LastName}`;
    let amount = parseFloat(this.transactionAmount);

    toAccount.Balance += amount;
    this._bankAccountService.updateAccount$(toAccount.Id, toAccount).subscribe({
      next(data) { console.log('Accounts successfully updated.') },
      error(err) { console.error(`ERROR: ${err}`) },
      complete() { }
    });
    this.createTransaction(toAccount, amount, toReference, TransactionType.DEPOSIT);

    fromAccount.Balance -= amount;
    this._bankAccountService.updateAccount$(fromAccount.Id, fromAccount).subscribe({
      next(data) { console.log('Accounts successfully updated.') },
      error(err) { console.error(`ERROR: ${err}`) },
      complete() { }
    });

    this.createTransaction(fromAccount, amount, fromReference, TransactionType.WITHDRAWAL);
    this.transactionAmount = '';
    window.location.reload()
  }

  private _filter(value: string): User[] {
    const filterValue = value.toLowerCase();

    return this._familyMembers.filter(member => member.FirstName.toLowerCase().includes(filterValue));
  }
}

