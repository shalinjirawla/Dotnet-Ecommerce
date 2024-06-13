import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { ProductsComponent } from '../admin/products/products.component';
import { AdminModule } from '../admin/admin.module';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { CartComponent } from './cart/cart.component';
import { FormsModule } from '@angular/forms';
import { MyOrdersComponent } from './my-orders/my-orders.component';

@NgModule({
  declarations: [
    LandingPageComponent,
    LayoutComponent,
    CartComponent,
    MyOrdersComponent,
    
  ],
  imports: [
    CommonModule,
    FormsModule,
    AdminModule,
    RouterModule
  ],
  exports:[LandingPageComponent]
})
export class SharedModule { }
