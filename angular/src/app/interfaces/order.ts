import { IProduct } from "./product"
import { IUser } from "./user"

export interface IOrder {
    productRating: any
    id:number,
    userId:number,
    productId:number ,
    quantity:number,
    totalAmount:number,
    status:string,
    orderDate:string,
    product:IProduct,
    user:IUser
}
