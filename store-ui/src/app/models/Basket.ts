export interface Basket {
    id: number;
    buyerId: string;
    basketItems: BasketItem[];
}

export interface BasketItem {
    productId: number;
    name: string;
    price: number;
    pictureUrl: string;
    type: string;
    brand: string;
    quantity: number;
}