import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationRoutingModule } from './authentication-routing.module';
import { AuthWrapperComponent } from './auth-wrapper/auth-wrapper.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthenticationInterceptor } from '../interceptors/authentication.interceptor';


@NgModule({
  declarations: [
    AuthWrapperComponent,
    LoginComponent,
    SignupComponent
  ],
  imports: [
    CommonModule,
    AuthenticationRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule
  ],
  providers:[{provide:HTTP_INTERCEPTORS,useClass:AuthenticationInterceptor,multi:true}]
})
export class AuthenticationModule { }
