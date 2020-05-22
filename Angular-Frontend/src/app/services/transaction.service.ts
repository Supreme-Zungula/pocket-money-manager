import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { Transaction } from '../models/transaction';


@Injectable({
	providedIn: 'root'
})
export class TransactionService {
	private _url: string = 'http://localhost:5000/api/transaction';
	private httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

	constructor(private _http: HttpClient) {

	}

	/**
	 * Retrieves all transactions from DB.
	 */
	getAllTransactions(): Observable<Transaction[]> {
		const transactionsUrl = `${this._url}/all`;

		return this._http.get<Transaction[]>(transactionsUrl).pipe(
			tap(data =>
				console.info(`Retrieved all transactions from DB, count : ${data.length}`),
				catchError(this.handleError('getAllTranactions'))
			)
		);
	}

	/**
	 * Retrieves a transaction from DB using it ID.
	 * @param tranID - ID of the transactiont to retrieve
	 */
	getTransactionById(tranID: string): Observable<Transaction> {
		const transactionRoute = `${this._url}/${tranID}`

		return this._http.get<Transaction>(transactionRoute).pipe(
			tap(data => {
				console.info(`Retrieve transaction with id = ${tranID}`),
					catchError(this.handleError('getTransaction'))
			})
		);
	}

	/**
	 * Updates a transaction in the DB.
	 * @param tranID - ID of the transction to update.
	 * @param transaction -  Transaction with new information
	 */
	updateTransaction(tranID: string, transaction: Transaction): Observable<Transaction> {
		const updateRoute = `${this._url}/${tranID}`;

		return this._http.put<Transaction>(updateRoute, transaction, this.httpOptions).pipe(
			tap(data => {
				console.info(`Update transaction with id = ${tranID}`),
					catchError(this.handleError('updateTransaction', transaction))
			})
		);
	}

	/**
	 * Delete a transaction from DB.
	 * @param transID - ID of the transactiont to be deleted.
	 */
	deleteTransaction(transID: string): Observable<Transaction> {
		const deleteRoute = `${this._url}/${transID}`;

		return this._http.delete<Transaction>(deleteRoute, this.httpOptions).pipe(
			tap(data => {
				console.info(`Deleted transaction with id = ${data.Id}`),
					catchError(this.handleError('deleteTransaction', transID))
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
