import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '',   redirectTo: '/login', pathMatch: 'full' }, // redirect to `login-component`
  { path: 'login', component: LoginComponent, data:{title: 'Login'}},
  { path: 'sign-up', component: SignUpComponent, data:{title: 'Sign up'}},
  { path: 'home/:firstName', component: HomeComponent, data:{title: 'Home'}},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
