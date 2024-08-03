import { Book } from "./book.model";

export interface CartItem {
    cartItemId?: number; // Make cartItemId optional if it’s generated server-side
    userId: number;
    bookId: number;
    quantity: number;
    book?: Book; // Optional property for the book details
  }
  