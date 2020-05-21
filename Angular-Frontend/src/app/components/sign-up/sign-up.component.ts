import { Component, OnInit } from '@angular/core';

import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';
import { ThrowStmt } from '@angular/compiler';
import { UrlSegment } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})

export class SignUpComponent implements OnInit {
  public usersList: User[] = [];
  public userData: User;

  constructor(private _userService: UserService) {
    this.getAllUsers();
  }

  ngOnInit(): void {
    this._userService.getUserByPhone("0721121122")
      .subscribe((data: User) => {
        this.userData = User.mapResponseToUser(data);
      });
  }

  getAllUsers() {
    this._userService.getAllUsers()
      .subscribe((data: any) => {
        this.usersList = User.mapResponseToUsers(data);
      });
  }
}
