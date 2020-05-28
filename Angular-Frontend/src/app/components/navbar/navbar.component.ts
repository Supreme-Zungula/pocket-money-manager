import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  private _userToken: string;
  currentUser: User = new User();

  constructor(
    private _authService: AuthService,
    private _userService: UserService,
  ) {

  }

  async ngOnInit() {
    this._userToken = this._authService.getUserToken();
    this._userService.getUserByPhone$(this._userToken).subscribe(data => {
      this.currentUser = User.mapResponseToUser(data);
    })
  }
}
