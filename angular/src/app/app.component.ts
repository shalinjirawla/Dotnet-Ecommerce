import { Component } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ECommerceFrontend';
  constructor(private toast:NgToastService){}
  showSuccess(){
    this.toast.success({detail:"Success message",summary:'successsummary',duration:2000});
  }
}
