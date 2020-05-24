import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  private _url: string = 'http://localhost:5000/api/user';

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, ) {


  }

  /**
   * Retrieve all users from database.
   */
  getAllUsers() {
    let usersUrl: string = `${this._url}/allUsers`;

    return this.http.get<User>(usersUrl, this.httpOptions);
  }

  /**
   * Retrieving a user from DB using phone number.
   * @param phone - user's unique for number.
   */
  getUserByPhone(phone: string): Observable<User> {
    let userUrl = `${this._url}/getByPhone/${phone}`;

    return this.http.get<User>(userUrl);
  }

  /**
  * Add new user the database with Http POST request method.
  * @param newUser - new user to add to the database
  */
  addNewUser(newUser: User): Observable<User> {

    let requestData = {
      "familyId": newUser.FamilyId,
      "firstName": newUser.FirstName,
      "lastName": newUser.LastName,
      "role": newUser.Role,
      "phone": newUser.Phone,
      "password": newUser.Password
    }

    return this.http.post<any>(this._url, requestData, this.httpOptions).pipe(
      tap((data) =>
        console.info(`added user w/ id=${data.Id}`)),
      catchError(this.handleError('addUser', newUser)
      )
    );
  }

  /**
   * update an existing user from DB, uses http PUT method.
   * @param userUpdates - new user details to update with.
   */
  updateUser(userUpdates: User): Observable<User> {
    let requestData = {
      "id": userUpdates.Id,
      "familyId": userUpdates.FamilyId,
      "firstName": userUpdates.FirstName,
      "lastName": userUpdates.LastName,
      "role": userUpdates.Role,
      "phone": userUpdates.Phone,
      "password": userUpdates.Password
    }

    return this.http.put<User>(this._url, requestData, this.httpOptions).pipe(
      tap(data =>
        console.info(`Updated user with w/ phone = ${userUpdates.Phone}`),
        catchError(this.handleError('updateUser', this.updateUser))
      )
    );
  }

  /**
   * Deletes a user from DB using their phone number as a key.
   * Uses DELETE http methods.
   * @param phone - phone number of the use to be deleted.
   */

  deleteUser(phone: string): Observable<User> {
    const deleteRoute = `${this._url}/${phone}`;

    return this.http.delete<User>(deleteRoute, this.httpOptions).pipe(
      tap(data =>
        console.info(`Deleted user with w/ phone= ${phone}`),
        catchError(this.handleError('deleteUser', phone))
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
