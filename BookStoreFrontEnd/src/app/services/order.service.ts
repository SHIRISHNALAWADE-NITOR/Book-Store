import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ToastService } from './toast.service';
 // Adjust the path as needed
 export interface OrderItem {
    orderItemId?: number; // Optional, used when receiving an existing OrderItem// Foreign key reference to the Order
    bookId: number; // Foreign key reference to the Book
    quantity: number;
    price: number;
  }
  
@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = 'http://localhost:5134/api/Order'; // Update with your actual API URL
  private orderItemApiUrl = 'http://localhost:5134/api/OrderItem'; // Update with your actual API URL
  private userId=localStorage.getItem("userId");
  constructor(private http: HttpClient,private toastService:ToastService) {}

  // Create a new order
  createOrder(orderData: any): Observable<any> {
    this.toastService.show("Order item added",'success')
    return this.http.post<any>(this.apiUrl, orderData);

  }

  // Fetch a specific order by its ID
  
 getOrder(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/user/${this.userId}`);
  }
 

  // Update an existing order
  updateOrder(orderId: number, orderData: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${orderId}`, orderData);
  }

  // Delete a specific order
  deleteOrder(orderId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${orderId}`);
  }

  // Add order items to an order
  addOrderItem(orderItem: OrderItem): Observable<OrderItem> {
    return this.http.post<OrderItem>(`${this.orderItemApiUrl}`, orderItem);
  }
  // Update a specific order item
  updateOrderItem(orderItemId: number, orderItem: OrderItem): Observable<OrderItem> {
    return this.http.put<OrderItem>(`${this.orderItemApiUrl}/${orderItemId}`, orderItem);
  }

  // Delete a specific order item
  deleteOrderItem(orderItemId: number): Observable<void> {
    return this.http.delete<void>(`${this.orderItemApiUrl}/${orderItemId}`);
  }
}
