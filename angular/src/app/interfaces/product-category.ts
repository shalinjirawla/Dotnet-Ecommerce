export interface IProductCategory {
    id:number,
    productId:number,
    categoryId:number,
    category:{
        id:number,
        categoryName:string
    }
}
