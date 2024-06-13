import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { IOrder } from 'src/app/interfaces/order';
import { IRating } from 'src/app/interfaces/rating';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.scss']
})
export class MyOrdersComponent {
  orders: IOrder[] = [];
  userId!:number;
  ratings: IRating[] = [];
  stars:number[] = [1,2,3,4,5];
  constructor(private productService: ProductService,private toast:NgToastService,private router:Router) { }
  ngOnInit():void{
    this.getMyOrders();
    this.getRatings(this.userId);
  }
  getMyOrders(): void {
    const userDataString = localStorage.getItem('UserData');
      if (userDataString) {
        const userData = JSON.parse(userDataString);
        const userId = userData.id; 
        this.userId = userId;
    this.productService.getMyOrders(userId).subscribe({
      next: (res) => {
        console.log(res)
        this.orders = res.data;
        },
      error: (error) => {
        this.toast.error({detail:"Error Message",summary:"Some error occur while fetching your orders!!",duration:3000})
      },
    });
  }
  }
  public cancelOrder(orderId: number): void {
    this.productService.cancelOrder(orderId).subscribe({
      next: (response: any) => {
        console.log('Order cancelled successfully');
        this.toast.success({detail:"Success Message",summary:"Your order cancelled successfully!!",duration:3000})
        this.getMyOrders();
      },
      error: (error: any) => {
        console.error('Error cancelling order:', error);
      }
    });
  }

  public  navigateLanding(): void {
    this.router.navigate(['/landing']);
  }

  rateProduct(productId: number, rating: number): void {
    const orderData = this.orders.findIndex(order => order.productId === productId);
    if (orderData !== -1) {
        this.orders[orderData].productRating = rating;
    }
    const userDataString = localStorage.getItem('UserData');
    if (userDataString) {
        const userData = JSON.parse(userDataString);
        const userId = userData.id;

        const existingRating = this.ratings.find(r => r.productId === productId);
        if (existingRating) {
            const ratingData: IRating = {
              id: existingRating.id, 
              productId,
              userId: +userId,
              rating
          };
            this.productService.updateProductRating(ratingData).subscribe(
                response => {
                    console.log('Rating updated successfully:', response);
                    this.getRatings(userId);
                },
                error => {
                    console.error('Error updating rating:', error);
                }
            );
        } else {
            const ratingData: IRating = {
                id: 0, 
                productId,
                userId: +userId,
                rating
            };
            this.productService.addProductRating(ratingData).subscribe(
                response => {
                    console.log('Rating added successfully:', response);
                    this.getRatings(userId);
                },
                error => {
                    console.error('Error adding rating:', error);
                }
            );
        }
    }
}
 public  getRatings(userId: number): void {
    this.productService.getRatingsByUserId(userId).subscribe({
      next: (response: any) => {
        this.ratings = response.data.filter((rating: IRating) => rating.userId === this.userId);
        console.log(this.ratings);
      },
      error: (error: any) => {
        console.error('Error fetching ratings:', error);
      }
    });
  }
  getProductRating(productId: number): number {
    const rating = this.ratings.find(rating => rating.productId === productId);
    return rating ? rating.rating : 0; 
  }
  
}
