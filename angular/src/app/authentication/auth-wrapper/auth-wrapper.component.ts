import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-wrapper',
  templateUrl: './auth-wrapper.component.html',
  styleUrls: ['./auth-wrapper.component.scss']
})
export class AuthWrapperComponent {
  constructor(private router:Router){}
 
  
  public navigateToLandingPage(): void {
    this.router.navigate(['/landing']);
  }
}
