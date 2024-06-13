import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { ICartItems } from 'src/app/interfaces/cart-items';
import { IOrder } from 'src/app/interfaces/order';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent {
  
  userId!:number;
  cartItems:ICartItems[] = [];
  constructor(private productService:ProductService,private toast:NgToastService,private authService:AuthenticationService,private router:Router){}

  ngOnInit():void{
    this.getCartItems();
  }
  public getCartItems():void{
    if (!this.authService.isAuthenticated()) {
      this.toast.warning({detail:"Warning Message",summary:"Please do login first to add product into cart!!",duration:4000})
      this.router.navigate(['/auth/login']);
    }
    else{
      const userDataString = localStorage.getItem('UserData');
      if (userDataString) {
        const userData = JSON.parse(userDataString);
        const userId = userData.id; 
        this.userId = userId;
  
        this.productService.getCartItems(userId).subscribe({
          next: (res) => {
            console.log(res)
            this.cartItems = res.data;
          },
          error: (error) => {
            this.toast.error({detail:"Error Message",summary:"Some error occur while fetching the products!!",duration:3000})
          },
        });
      }
    }
  }
  public updateCartItem(cartItem: ICartItems): void {
    this.productService.updateCartItem(cartItem).subscribe({
      next: (response) => {
        console.log('Cart item updated successfully', response);
      },
      error: (error) => {
        console.error('Error updating cart item', error);
      }
    });
  }
  deleteCartItem(cartItemId: number): void {
    this.productService.deleteCartItem(cartItemId).subscribe({
      next: (response) => {
       this.toast.success({detail:"Success Messsage",summary:"Cart Item deleted successfully",duration:3000})
        this.getCartItems();
      },
      error: (error) => {
        console.error('Error deleting cart item', error);
      }
    });
  }
  public emptyCart(userId: number): void {
    this.productService.emptyCart(userId).subscribe({
      next: () => {
        console.log('Cart emptied successfully.');
        this.toast.success({detail:"Success Messsage",summary:"Your cart is empty",duration:3000})
        this.getCartItems();
      },
      error: (error) => {
        console.error('Error emptying cart:', error);
      }
    });
  }
  public getTotalCartPrice(): number {
    let total = 0;
    for (const cartItem of this.cartItems) {
        total += cartItem.product.price * cartItem.quantity;
    }
    return total;
}

public placeOrder() {
  const order: IOrder[] = this.cartItems.map(cart=>{
  return  {
      id: 0, 
      userId: this.userId, 
      orderDate: '',
      productId: cart.productId,
      quantity:cart.quantity,
      totalAmount: this.getTotalCartPrice(), 
      status: '',
      product:cart.product,
      user:cart.user,
      productRating:0
  };
  })

  this.productService.placeOrder(order).subscribe({
      next: (response) => {
          console.log('Order placed successfully:', response);
          this.toast.success({detail:"Success Message",summary:"Your order placed successfully!!.."});
          this.emptyCart(this.userId);
      },
      error: (error) => {
          console.error('Error placing order:', error);
          this.toast.error({detail:"Error Message",summary:"Your order is not placed!!.."});
      }
  });
}

  public  navigateLanding(): void {
    this.router.navigate(['/landing']);
  }
}
