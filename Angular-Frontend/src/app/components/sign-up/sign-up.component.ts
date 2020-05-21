import { Component, OnInit } from '@angular/core';

import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  private _users : User[]  = [];

  constructor(private _userService: UserService) { 

  }

  ngOnInit(): void {

  }

  getAllUsers() : User[] {
    this._userService.getAllUsers()
      .subscribe((data : User) => {
        this._users.push(data);
      })
    return this._users;
  }
}
