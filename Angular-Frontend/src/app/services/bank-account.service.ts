import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { BankAccount } from '../models/BankAccount';


@Injectable({
  providedIn: 'root'
})
export class BankAccountService {
  private _url: string = 'http://localhost:5000/api/bankAccounts';
  private httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

  constructor(private _http: HttpClient) {

  }

  /**
	 * Retrieves all bankAccounts from DB.
	 */
  getAllAccounts$(): Observable<BankAccount[]> {
    const accountsUrl = `${this._url}`;

    return this._http.get<BankAccount[]>(accountsUrl).pipe(
      tap(data =>
        console.info(`Retrieved all bank accounts from DB, count : ${data.length}`),
        catchError(this.handleError('getAllAccounts'))
      )
    );
  }

	/**
	 * Retrieves a bank account from DB using it ID.
	 * @param accountId - ID of the account to retrieve
	 */
  getAccountById$(accountId: string): Observable<BankAccount> {
    const transactionRoute = `${this._url}/${accountId}`

    return this._http.get<any>(transactionRoute).pipe(
      tap(data => {
        console.info(`Retrieve transaction with id = ${data.id}`),
          catchError(this.handleError('getAccount'))
      })
    );
  }

  /**
   * Adds a new bank account to the database.
   * @param accontIn - new bank account to be added to the DB.
   */
  addBankAccount$(accountIn: BankAccount): Observable<BankAccount> {
    const accountData =  {
      "balance": accountIn.Balance,
      "customerRef": accountIn.CustomerRef
    }

    return this._http.post<any>(this._url, accountData).pipe(
      tap(data => {
        console.info(`Added new bank account with id = ${data.id}`),
          catchError(this.handleError('addAccount', accountIn))
      })
    );
  }

	/**
	 * Updates a bank account in the DB.
	 * @param accontID - ID of the transction to update.
	 * @param accountIn -  Transaction with new information
	 */
  updateAccount$(accountID: string, accountIn: BankAccount): Observable<BankAccount> {
    const updateRoute = `${this._url}/${accountID}`;

    const accountData =  {
      "id": accountIn.Id,
      "accountNo": accountIn.AccountNo,
      "balance": accountIn.Balance,
      "customerRef": accountIn.CustomerRef
    }

    return this._http.put<BankAccount>(updateRoute, accountData, this.httpOptions).pipe(
      tap(data => {
        console.info(`Updated bank account with id = ${accountID}`),
          catchError(this.handleError('updateAccount', accountIn))
      })
    );
  }

	/**
	 * Delete a bank account from DB.
	 * @param accountId - ID of the bank account to be deleted.
	 */
  deleteAccount(accountId: string): Observable<BankAccount> {
    const deleteRoute = `${this._url}/${accountId}`;

    return this._http.delete<BankAccount>(deleteRoute, this.httpOptions).pipe(
      tap(data => {
        console.info(`Deleted bank account with id = ${data.Id}`),
          catchError(this.handleError('deleteAccount', accountId))
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
