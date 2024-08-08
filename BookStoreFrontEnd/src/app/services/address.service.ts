import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Address } from 'src/app/services/address.model'; // Import Address interface

@Injectable({
  providedIn: 'root'
})
export class AddressService {
  private apiUrl = 'http://localhost:5134';
  private baseUrl = `http://localhost:5134/api/Address`;

  constructor(private http: HttpClient) {}

  // Fetch all addresses for a specific user
  getAddresses(userId: string): Observable<Address[]> {
    return this.http.get<Address[]>(`${this.baseUrl}/${userId}`);
  }

  // Fetch a specific address by its ID
  getAddressById(id: number): Observable<Address> {
    return this.http.get<Address>(`${this.baseUrl}/${id}`);
  }

  // Add a new address
  addAddress(address: Address): Observable<Address> {
    return this.http.post<Address>(this.baseUrl, address);
  }

  // Update an existing address
  updateAddress(id: number, address: Address): Observable<Address> {
    return this.http.put<Address>(`${this.baseUrl}/${id}`, address);
  }

  // Delete an address
  deleteAddress(id: number): Observable<void> {
    console.log(id+ "DELETING ADDRESS FROM UI");
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
  
}
