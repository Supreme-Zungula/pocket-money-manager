import { Injectable, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, throwError} from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService implements OnInit, OnDestroy{
  private _userSet$ = new Subject();
  private _ngUnsubscribe =  new Subject();
  private _user : User;
  private _url : string = 'http://localhost:5000/api/user';

  constructor(private http: HttpClient) { 

  }

  ngOnInit() {

  }

  ngOnDestroy() {
    this._ngUnsubscribe.next();
    this._ngUnsubscribe.complete();
  }

  getAllUsers() {
    let usersUrl: string = `${this._url}/allUsers`;
    const options : object = {
      responseType: 'json'
    }

    return this.http.get<User>(usersUrl, options);
  }
}
