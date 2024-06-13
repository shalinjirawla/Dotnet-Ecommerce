import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent {
  type: string = 'password';
  public error: string | undefined;
  isText: boolean = false;
  eyeIcon: string = 'fa-eye';

  public signupForm!: FormGroup;

  public patternForEmail: RegExp =
    /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})/;

  public patternForPassword: RegExp =
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

  constructor(
    private authService: AuthenticationService,
    private router: Router,private toast:NgToastService
  ) {}

  public ngOnInit(): void {
    this.initializeForm();
  }

  public initializeForm(): void {
    this.signupForm = new FormGroup({
      name: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [
        Validators.required,
        Validators.email,
        Validators.pattern(this.patternForEmail),
      ]),
      password: new FormControl(null, [
        Validators.required,
        Validators.pattern(this.patternForPassword),
      ]),
      mobileNumber: new FormControl(null, [
        Validators.required,
        Validators.pattern(/^\d{10}$/),
      ]),

      role: new FormControl(null, [Validators.required]),
    });
  }

  public signup(): void {
    if (this.signupForm.valid) {
      this.authService.signUp(this.signupForm.value).subscribe({
        next: (response: any) => {
          console.log(response);
          this.signupForm.reset();
          this.toast.success({detail:"Success Message",summary:"SignedUp successfully!!"})
          this.router.navigateByUrl('auth/login');
        },
        error: () => {
          this.toast.error({detail:"Error Message",summary:"Some error occurs while signUp!!", duration:3000})
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
