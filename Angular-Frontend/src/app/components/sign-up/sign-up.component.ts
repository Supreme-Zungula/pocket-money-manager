import { Component, OnInit } from '@angular/core';

import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})

export class SignUpComponent implements OnInit {
  public usersList: User[] = [];
  public userData: User;
  private newUser : User; 
  
  constructor(private _userService: UserService) {
    this.newUser = new User();
    this.newUser.FirstName = "TestName1";
    this.newUser.LastName = 'TestSurname1';
    this.newUser.Password = 'Password';
    this.newUser.Phone = '0123456789';
    
  }

  ngOnInit(): void {
    this.getAllUsers();
    this._userService.addNewUser(this.newUser).subscribe((user)=> {
      this.userData = user;
    });

   /*  this._userService.getUserByPhone("0123456789")
      .subscribe((data: User) => {
        this.userData = User.mapResponseToUser(data);
      }); */
  }

  getAllUsers() {
    this._userService.getAllUsers()
      .subscribe((data: any) => {
        this.usersList = User.mapResponseToUsers(data);
      });
  }
}
