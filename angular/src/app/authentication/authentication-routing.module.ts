import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthWrapperComponent } from './auth-wrapper/auth-wrapper.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';

const routes: Routes = [
  {
    path: '',
    component: AuthWrapperComponent,
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'signup', component: SignupComponent },
      { path: '**', redirectTo: 'login' } 
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthenticationRoutingModule { }
