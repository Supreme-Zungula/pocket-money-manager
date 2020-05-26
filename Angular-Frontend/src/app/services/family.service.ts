import { Injectable, OnInit } from '@angular/core';

import { UserService } from './user.service';
import { User } from '../models/user';
import { BankAccountService } from './bank-account.service';
import { BankAccount } from '../models/BankAccount';

@Injectable({
  providedIn: 'root'
})
export class FamilyService {
  private _usersList: User[] = [];
  private _familyMembers: User[] = [];
  private _bankAccounts: BankAccount[] = [];

  constructor(
    private _userService: UserService,
    private _bankAccountService: BankAccountService,
  ) {

    this._userService.getAllUsers$().subscribe(data => {
      this._usersList = User.mapResponseToUsers(data);
    })

    this._bankAccountService.getAllAccounts$().subscribe(data => {
      this._bankAccounts = BankAccount.mapResponseToBankAccountList(data);
    });
  }


  getFamilyMembers(familyId: number): User[] {
    this._familyMembers = this._usersList.filter(user => {
      if (user.FamilyId === familyId) {
        return true;
      }
    })

    return this._familyMembers;
  }

  addFamilyMember(newMember: User) {
    this._familyMembers.push(newMember);
  }

  getFamilyMemberBankDetails(member: User): BankAccount {
    return this._bankAccounts.find(account => {
      return account.CustomerRef === member.Id;
    });
  }
}
