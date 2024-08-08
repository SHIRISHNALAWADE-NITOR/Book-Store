export interface Book {
    bookId: number;
    isbn?: number;
    category?: string;
    numberOfPages?: number;
    quantity?: number; 
    rating?: number;
    title?: string;
    author?: string;
    price: number;
    description?: string;
    imageUrl?: string;
    createdAt?: Date;
    reviews?: any[]; // Adjust as per your Review model
}

  