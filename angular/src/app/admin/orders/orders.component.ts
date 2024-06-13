import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { IOrder } from 'src/app/interfaces/order';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent {
  public orderList!:IOrder[];
  constructor(private router:Router,private productService:ProductService,private toast:NgToastService) {}
  ngOnInit(): void {
    this.getOrders();
  }
  public getOrders():void{
    this.productService.getOrders().subscribe({
      next: (res) => {
        console.log(res)
        this.orderList = res.data;
      },
      error: (error) => {
        this.toast.error({detail:"Error Message",summary:"Some error occur while fetching the data!!",duration:3000})
      },
    });
  }
  public updateOrderStatus(order:IOrder){
    if (order.status === 'InProgress') {
      this.productService.completeOrder(order.id).subscribe({
        next: () => {
          this.toast.success({detail:"Success Message",summary:"Order Status changed!!",duration:3000});
          this.getOrders();
        },
        error: (error) => {
          this.toast.error({detail:"Error message",summary:"Could not update status!!",duration:3000});
        }
      });
    }
  }
}
