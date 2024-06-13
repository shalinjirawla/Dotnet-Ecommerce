import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { LandingPageComponent } from './shared/landing-page/landing-page.component';
import { CartComponent } from './shared/cart/cart.component';
import { MyOrdersComponent } from './shared/my-orders/my-orders.component';

const routes: Routes = [
  {
    path:'',
    redirectTo:'landing',
    pathMatch:'full'
  },
  { path: 'landing', component: LandingPageComponent },
  { path: 'landing/cart', component: CartComponent },
  {path:'landing/my-orders',component:MyOrdersComponent},
  { path: 'landing/men/:category', component: LandingPageComponent },
  { path: 'landing/women/:category', component: LandingPageComponent },
  { path: 'landing/kids/:category', component: LandingPageComponent },
  {
    path: 'auth',
    loadChildren: () =>
      import('./authentication/authentication.module').then(
        (m) => m.AuthenticationModule
      ),
  }
  ,
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}