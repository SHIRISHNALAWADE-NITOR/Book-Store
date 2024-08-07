export interface Address {
    street: string;
    city: string;
    state: string;
    postalCode: string;
    country: string;
    addressId?: number; // Optional if your address has an ID
    userId?: string; // Optional if you associate addresses with users
  }
  