import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  type: string = 'password';
  public error: string | undefined;
  isText: boolean = false;
  eyeIcon: string = 'fa-eye-slash';
  public loginForm!: FormGroup;
  public showAddProductButton!:boolean;

  public patternForEmail: RegExp =
    /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})/;

  public patternForPassword: RegExp =
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/;

  constructor(
    private authService: AuthenticationService,
    private router: Router,private toast:NgToastService
  ) {}

  public ngOnInit(): void {
    this.initializeForm();
  }

  public initializeForm(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [
        Validators.required,
        Validators.email,
        Validators.pattern(this.patternForEmail),
      ]),
      password: new FormControl(null, [Validators.required]),
    });
  }
  public login(): void {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: (response: any) => {
          console.log(response);
          this.loginForm.reset();
          console.log(response.role);
          const data = JSON.stringify(response);
          localStorage.setItem('UserData', data);
          if (response.role === 'admin') {
            this.router.navigateByUrl('admin/products');
          } else {
            this.router.navigateByUrl('landing');
          }
          
          this.toast.success({detail:"Success Message",summary:"You are logged in successfully!!",duration:3000});
          
        },
        error: () => {
          this.toast.error({detail:"Error Message",summary:"email or password incorrect!!",duration:3000})
        },
      });
    }
  }
  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? (this.eyeIcon = 'fa-eye') : (this.eyeIcon = 'fa-eye-slash');
    this.isText ? (this.type = 'text') : (this.type = 'password');
  }
}
