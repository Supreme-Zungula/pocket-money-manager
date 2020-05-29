import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

/* Import angular material Modules */
import { MaterialModule } from './material-modules/material-module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { MatRadioModule } from '@angular/material/radio';
import { HomeComponent } from './components/home/home.component';
import { ErrorComponent } from './components/error/error.component';
import { FamilyComponent } from './components/family/family.component';
import { LogoutComponent } from './components/logout/logout.component';
import { TransactionsComponent } from './components/transactions/transactions.component';
import { AccountDetailsComponent } from './components/account-details/account-details.component';
import { TransactionsListComponent } from './components/transactions/transactions-list/transactions-list.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignUpComponent,
    NavbarComponent,
    HomeComponent,
    ErrorComponent,
    FamilyComponent,
    LogoutComponent,
    TransactionsComponent,
    AccountDetailsComponent,
    TransactionsListComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule, /* Angular material modules */
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
