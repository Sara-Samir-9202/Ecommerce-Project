export
interface ChechOutRes {
  status: string;
  data: Data;
}

interface Data {
  taxPrice: number;
  shippingPrice: number;
  totalOrderPrice: number;
  paymentMethodType: string;
  isPaid: boolean;
  isDelivered: boolean;
  _id: string;
  user: string;
  cartItems: CartItem[];
  shippingAddress: ShippingAddress;
  createdAt: string;
  updatedAt: string;
  id: number;
  __v: number;
}

interface ShippingAddress {
  details: string;
  phone: string;
  city: string;
}

interface CartItem {
  count: number;
  _id: string;
  product: string;
  price: number;
}