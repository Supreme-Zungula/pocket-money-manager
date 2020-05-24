import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,  FormControl, Validators } from '@angular/forms';

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
  public newUser: User;
  // public transactionsList: Transaction[];
  // public accountsList: BankAccount[] = [];
  public signupFormGroup: FormGroup;

  constructor(
    private _userService: UserService,
    private _transactionService: TransactionService,
    private _bankAccountService: BankAccountService,
  ) { 
    this.newUser = new User();
    this.newUser.FirstName = "TestName"
  }

  async ngOnInit() {
    this.signupFormGroup = new FormGroup({
      'firstname': new FormControl(this.newUser.FirstName, [ Validators.required ]),
      'lastname': new FormControl(this.newUser.LastName, [ Validators.required]),
      'phoneNumber': new FormControl(this.newUser.Password, [ Validators.required,]),
      'password': new FormControl(this.newUser.Password,  [ Validators.required, Validators.minLength(6)])
            
    });


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
