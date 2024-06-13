import { NO_ERRORS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { RouterModule } from '@angular/router';
import { UserListComponent } from './user-list/user-list.component';
import { ProductsComponent } from './products/products.component';
import { ProductFormComponent } from './product-form/product-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OrdersComponent } from './orders/orders.component';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [
    AdminDashboardComponent,
    UserListComponent,
    ProductsComponent,
    ProductFormComponent,
    OrdersComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    MatIconModule
  ],
  exports:[
    ProductsComponent,
    AdminDashboardComponent,
    ProductsComponent,
    OrdersComponent,
    UserListComponent
  ],
  // schemas: [
  //   NO_ERRORS_SCHEMA
  // ]
})
export class AdminModule { }
