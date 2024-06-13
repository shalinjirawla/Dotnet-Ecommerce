import { IProductCategory } from "./product-category";

export interface IProduct {
    id:number,
    productName:string,
    productPhotoUrl:File | null,
    quantity:number,
    price:number,
    timesSold:number,
    productCategories:IProductCategory[]
}
