import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-family',
  templateUrl: './family.component.html',
  styleUrls: ['./family.component.css']
})
export class FamilyComponent implements OnInit {
  private _userToken: string;

  currentUser: User;
  usersList: User[] = [];
  familyMembers: User[] = [];

  panelOpenState: boolean = false;


  constructor(
    private _authService: AuthService,
    private _userService: UserService,
    private _router: Router,
  ) {
    
  }

  ngOnInit(): void {
    this._userToken = this._authService.getUserToken();
    let isLoggedIn = this._authService.isLoggedIn();

    if (isLoggedIn == false) {
      this._router.navigate(['login']);
    }
    this.getAllUsers();
    
    this._userService.getUserByPhone$(this._userToken).subscribe(data => {
      this.currentUser = User.mapResponseToUser(data);
    });
    
    this.getFamilyMembers();
  }

  private getAllUsers() {
    this._userService.getAllUsers$().subscribe(data => {
      this.usersList = User.mapResponseToUsers(data);
    })
    console.log(this.usersList);
  }

  private getFamilyMembers() {
    this.familyMembers = this.usersList.filter(user => {
      return user.FamilyId == this.currentUser.FamilyId;
    })
    console.log(this.familyMembers)
  }
}
