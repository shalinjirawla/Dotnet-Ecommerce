import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { IProduct } from '../interfaces/product';
import { ICartItems } from '../interfaces/cart-items';
import { IOrder } from '../interfaces/order';
import { IRating } from '../interfaces/rating';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http:HttpClient) { }
  public getProducts():Observable<any>{
    return this.http.get<any>(`${environment.baseUrl}products/Products`);
  }
  public getProductsByCategoryId(categoryId: number): Observable<any> {
    return this.http.get<any>(`${environment.baseUrl}products/categoryId?categoryId=${categoryId}`);
  }
  
  public addProduct(product:any):Observable<any>{
    return this.http.post(`${environment.baseUrl}products/product`,product);
  }

  public getCategories():Observable<any>{
    return this.http.get(`${environment.baseUrl}products/Categories`);
  }
  public getOrders():Observable<any>{
    return this.http.get<any>(`${environment.baseUrl}orders`);
  }
  public addToCart(cartItems:ICartItems):Observable<any>{
    return this.http.post(`${environment.baseUrl}cart`,cartItems);
  }
  public getCartItems(userId: number): Observable<any> {
    return this.http.get<any>(`${environment.baseUrl}cart/id?userId=${userId}`);
  }

  updateCartItem(cartItem: ICartItems): Observable<any> {
    const url = `${environment.baseUrl}cart`;
    return this.http.put<any>(url, cartItem);
  }

  public deleteCartItem(cartItemId: number): Observable<any> {
    return this.http.delete<any>(`${environment.baseUrl}cart/id`, { params: { cartItemId: cartItemId.toString() } });
  }
  emptyCart(userId: number): Observable<any> {
    return this.http.delete<any>(`${environment.baseUrl}cart/empty/${userId}`);
  }
  placeOrder(order: IOrder[]): Observable<any> {
    return this.http.post<any>(`${environment.baseUrl}orders`, order);
}
getMyOrders(userId: number): Observable<any> {
  const url = `${environment.baseUrl}orders/${userId}`;
  return this.http.get<any>(url);
}
cancelOrder(orderId: number): Observable<any> {
  const url = `${environment.baseUrl}orders/${orderId}/cancel`;
  return this.http.put(url,orderId);
}
completeOrder(orderId: number): Observable<any> {
  return this.http.put<any>(`${environment.baseUrl}orders/${orderId}/complete`, orderId);
}

getProductRatingById(id: number): Observable<any> {
  return this.http.get(`${environment.baseUrl}rating/id?id=${id}`);
}

getRatingsByProductId(productId: number): Observable<any> {
  return this.http.get(`${environment.baseUrl}rating/product/${productId}`);
}
getRatingsByUserId(userId: number): Observable<any> {
  return this.http.get(`${environment.baseUrl}rating/products/${userId}`);
}

addProductRating(ratingData: any): Observable<any> {
  return this.http.post(`${environment.baseUrl}rating`, ratingData);
}

updateProductRating(rating:any): Observable<any> {
  return this.http.put(`${environment.baseUrl}rating`, rating);
}

deleteProductRating(ratingId: number): Observable<any> {
  return this.http.delete(`${environment.baseUrl}rating/${ratingId}`);
}

getAverageProductRating(productId: number): Observable<any> {
  const url = `${environment.baseUrl}rating/average/${productId}`;
  return this.http.get<any>(url);
}
getAllProductsAverageRating(): Observable<number> {
  return this.http.get<number>(`${environment.baseUrl}rating/average/all`);
}
}
