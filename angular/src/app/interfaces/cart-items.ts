import { IProduct } from "./product";
import { IUser } from "./user";

export interface ICartItems {
    id:number,
    userId:number,
    productId:number,
    quantity:number
    product: IProduct;
    user:IUser
}
