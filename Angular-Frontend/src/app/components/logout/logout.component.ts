import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(
    private _authService: AuthService,
    private _router: Router,
  ) {

  }

  ngOnInit(): void {
    this._authService.clearSession();
    this._router.navigate(['login'])
  }

}
