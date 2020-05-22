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
