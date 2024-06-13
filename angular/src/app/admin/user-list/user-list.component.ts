import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastModule, NgToastService } from 'ng-angular-popup';
import { IUser } from 'src/app/interfaces/user';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {
  public usersList!: IUser[];

  constructor(private userService:UserService,private toast:NgToastService,private router:Router) {}

  ngOnInit(): void {
    this.getUsers();
  }

  public getUsers(): void {
    this.userService.getUsers().subscribe({
      next: (res) => {
        console.log(res)
        this.usersList = res.data;
      },
      error: (error) => {
        this.toast.error({detail:"Error Message",summary:"Some error occur while fetching the data!!",duration:3000})
      },
    });
  }
  // public  navigateToDashboard(): void {
  //   this.router.navigate(['/admin/dashboard']);
  // }
}
