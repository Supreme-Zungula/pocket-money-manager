import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

import { UserService } from './user.service';
import { User } from '../models/user';
import { BankAccountService } from './bank-account.service';
import { BankAccount } from '../models/BankAccount';
import { FamilyMember } from '../models/FamilyMember';

@Injectable({
  providedIn: 'root'
})
export class FamilyService {
  private _usersList: User[] = [];
  private _familyMembers: FamilyMember[] = [];
  private _bankAccounts: BankAccount[] = [];
  private _url: string = ' http://localhost:5000/api/familyMember';
  private httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

  constructor(
    private _userService: UserService,
    private _bankAccountService: BankAccountService,
    private _http: HttpClient,
  ) {

    this._userService.getAllUsers$().subscribe(data => {
      this._usersList = User.mapResponseToUsers(data);
    })

    this._bankAccountService.getAllAccounts$().subscribe(data => {
      this._bankAccounts = BankAccount.mapResponseToBankAccountList(data);
    });

  }



  getFamilyMemberBankDetails(member: User): BankAccount {
    return this._bankAccounts.find(account => {
      return account.CustomerRef === member.Id;
    });
  } 

  getAllFamilyMember$(familyId: number): Observable<FamilyMember[]> {
    const membersUrl = `${this._url}/getmembers/${familyId}`;

    return this._http.get<FamilyMember[]>(membersUrl).pipe(
      tap(data =>
        console.info(`Retrieved family members from DB, count : ${data.length}`),
        catchError(this.handleError('getAllMembers'))
      )
    );
  }

  getMemberByPhone$(phone: string): Observable<FamilyMember> {
    const apiUrl = `${this._url}/getbyphone/${phone}`

    return this._http.get<any>(apiUrl).pipe(
      tap(data => {
        console.info(`Retrieved member with id = ${data.id}`),
          catchError(this.handleError('getMember'))
      })
    );
  }

  addFamilyMember$(newMember: FamilyMember): Observable<FamilyMember> {
    const accountData = {
      "familyId": newMember.FamilyId,
      "firstName": newMember.FirstName,
      "lastName": newMember.LastName,
      "relationship": newMember.Relationship,
      "phone": newMember.Phone
    }

    return this._http.post<any>(this._url, accountData).pipe(
      tap(data => {
        console.info(`Added new member with id = ${data.id}`),
          catchError(this.handleError('addMember', newMember))
      })
    );
  }


  updateFamilyMember$(memberUpdates: FamilyMember): Observable<FamilyMember> {
    const accountData = {
      "id": memberUpdates.Id,
      "familyId": memberUpdates.FamilyId,
      "firstName": memberUpdates.FirstName,
      "lastName": memberUpdates.LastName,
      "relationship": memberUpdates.Relationship,
      "phone": memberUpdates.Phone
    }

    return this._http.put<FamilyMember>(this._url, accountData, this.httpOptions).pipe(
      tap(data => {
        console.info(`Updated family member.`),
          catchError(this.handleError('updateMember', memberUpdates))
      })
    );
  }

  deleteFamilyMember$(phone: string): Observable<FamilyMember> {
    const deleteRoute = `${this._url}/${phone}`;

    return this._http.delete<FamilyMember>(deleteRoute, this.httpOptions).pipe(
      tap(data => {
        console.info(`Deleted bank account`),
          catchError(this.handleError('deleteMember'))
      })
    );
  }


  /**
	 * Handle Http operation that failed.
	 * Let the app continue.
	 * @param operation - name of the operation that failed
	 * @param result - optional value to return as the observable result
	 */
  private handleError<User>(operation = 'operation', result?: User) {
    return (error: any): Observable<User> => {

      console.error(error); // log to console instead
      console.error(`${operation} failed: ${error.message}`);

      return of(result as User);
    };
  }
}
