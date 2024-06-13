import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { IProduct } from 'src/app/interfaces/product';
import { ProductService } from 'src/app/services/product.service';
import { ProductFormComponent } from '../product-form/product-form.component';
import { MatDialog } from '@angular/material/dialog';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ICartItems } from 'src/app/interfaces/cart-items';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent {
  public productList!:IProduct[];
  public showAddButton:Boolean = false;
  public productRatings: any;
  constructor(private productService:ProductService,private toast:NgToastService,private router:Router,public dialog: MatDialog,private authService:AuthenticationService,private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.checkUserRole();
    this.loadProductRatings();
    this.route.params.subscribe(params => {
      const categoryId = +params['category'];
      if (!isNaN(categoryId)) {
        this.getProductsByCategoryId(categoryId);
      } else {
        this.getProducts(); 
      }
    });
  }
  isCategoryPage(): boolean {
    return this.route.snapshot.params.hasOwnProperty('category');
  }
  navigateToAllProducts(): void {
    this.router.navigate(['/landing']);
  }

  ngAfterViewInit(): void {
    this.subscribeToRouteParams();
  }

  private subscribeToRouteParams(): void {
    this.route.params.subscribe(params => {
      const categoryId = +params['category'];
      if (!isNaN(categoryId)) {
        this.getProductsByCategoryId(categoryId);
      } else {
        console.log('Category parameter is not defined or is invalid.');
      }
    });
  }
  
  public getProducts():void{
    this.productService.getProducts().subscribe({
      next: (res) => {
        console.log(res)
        this.productList = res.data.sort((a:IProduct, b:IProduct) => b.timesSold - a.timesSold);
      },
      error: (error) => {
        this.toast.error({detail:"Error Message",summary:"Some error occur while fetching the products!!",duration:3000})
      },
    });
  }
  
  checkUserRole() {
    if (this.authService.isAuthenticated()) {
      const userRole = this.authService.getUserRole();
      if (userRole === 'admin') {
        this.showAddButton = true;
      }
    }
  }
  public addToCart(productId:number){
    if (!this.authService.isAuthenticated()) {
      this.toast.warning({detail:"Warning Message",summary:"Please do login first to add product into cart!!",duration:4000})
      this.router.navigate(['/auth/login']);
    } else {
      const userDataString = localStorage.getItem('UserData');
    if (userDataString) {
      const userData = JSON.parse(userDataString);
      const userId = userData.id; 
      
      const cartItem: ICartItems = {
        id: 0,
        userId: userId,
        productId: productId,
        quantity: 1,
        product:{
          id:productId,
          productName:'',
          productPhotoUrl: null,
          price:0,
          quantity:0,
          timesSold:0,
          productCategories:[],

        },
        user:{
          id: userId,
          name: '',
          email: '',
          password: '',
          mobileNumber: '',
          role: ''
        }
      };

      this.productService.addToCart(cartItem).subscribe(
        () => {
          this.toast.success({ detail: "Success Messege", summary: "Product added to the cart successfully!!!", duration: 3000 });
          console.log('Product added to cart');
        },
        error => {
          console.error('Error adding product to cart:', error);
          this.toast.error({ detail: "Error Message", summary:"Failed to add the product to the cart." , duration: 3000 });
        }
      );
    }
    }

  }
  public  navigateToDashboard(): void {
    this.router.navigate(['/admin/dashboard']);
  }
public addProduct(): void {
    let product:IProduct = {
      id:0,
      productName:'',
      productPhotoUrl: null,
      price:0,
      quantity:0,
      timesSold:0,
      productCategories:[],

    };
    const dialogRef = this.dialog.open(ProductFormComponent, {
      data: { product: product, mode: 'add' },
      width: '50%',
    });
  
    dialogRef.afterClosed().subscribe({
      
      next: (res) => {
        console.log("Add Data result:", res);
        this.getProducts();
      }
    });
  }
  loadProductRatings() {
    this.productService.getAllProductsAverageRating().subscribe(
      (ratings) => {
        this.productRatings = ratings;
      },
      (error) => {
        console.error('Error fetching product ratings:', error);
      }
    );
  }
  getProductsByCategoryId(categoryId: number): void {
    this.productService.getProductsByCategoryId(categoryId).subscribe(
      {
        next: (res) => {
          console.log(res)
          this.productList = res.data;
        },
        error: (error) => {
          this.toast.error({detail:"Error Message",summary:"Some error occur while fetching the products!!",duration:3000})
        },
      }
    );
  }
  onCategoryClick(categoryId: number): void {
    this.getProductsByCategoryId(categoryId);
  }
}
 