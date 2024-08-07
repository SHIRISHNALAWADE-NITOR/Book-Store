import { Component, OnInit } from '@angular/core';
import { AddressService } from 'src/app/services/address.service'; // Adjust the path as needed
import { MessageService, ConfirmationService } from 'primeng/api'; // Import MessageService and ConfirmationService
import { Address } from 'src/app/services/address.model'; // Import Address interface
import { UserService } from 'src/app/services/user.service';
import { UserDTO } from 'src/app/services/profile.dto';
import { OrderService } from 'src/app/services/order.service';
import { FileService } from 'src/app/services/file.service';
import { Observable } from 'rxjs';

export interface Order {
  orderId: number;
  userId: number;
  orderDate: string; // ISO 8601 date format
  totalAmount: number;
  shippingAddressId: number;
  orderItems: OrderItem[];
}

export interface OrderItem {
  orderItemId: number;
  bookId: number;
  quantity: number;
  price: number;
}

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [MessageService, ConfirmationService] // Add providers for MessageService and ConfirmationService
})


export class ProfileComponent implements OnInit {
  
  
  displayDialog: boolean = false;
  newAddress: Address = { street: '', city: '', state: '', postalCode: '', country: '' };
   profile: UserDTO = new UserDTO();
  addresses: Address[] = [];
  orders: Order[] = [];
  

  constructor(
    private addressService: AddressService,
    private messageService: MessageService,
    private userService: UserService,
    private confirmationService: ConfirmationService, // Inject ConfirmationService
    private orderService:OrderService,
    private fileService:FileService
  ) {}

  ngOnInit() {
    const userId = localStorage.getItem("userId");
    if (userId) {
      this.loadUserProfile(userId);
      this.loadAddresses(userId);
    } else {
      console.error('User ID not found');
      this.messageService.add({ severity: 'error', summary: 'User ID Missing', detail: 'Unable to load profile and addresses without a user ID.' });
    }
  }
  loadUserProfile(userId: string) {
    this.userService.getUserProfile(userId).subscribe(
      (response: UserDTO) => {
        this.profile = response;
        console.log('Loaded profile:', this.profile);
      },
      (error: any) => {
        console.error('Failed to load profile:', error);
        this.messageService.add({ severity: 'error', summary: 'Load Profile Failed', detail: 'There was an error loading profile.' });
      }
    );
  }
  loadAddresses(userId: string) {
    this.addressService.getAddresses(userId).subscribe(
      (response: Address[]) => {
        this.addresses = response;
        console.log('Loaded addresses:', this.addresses);
        if (this.addresses.length > 0) {
          this.profile.address = this.addresses[0]; // Example logic
        }
      },
      (error: any) => {
        console.error('Failed to load addresses:', error);
        this.messageService.add({ severity: 'error', summary: 'Load Addresses Failed', detail: 'There was an error loading addresses.' });
      }
    );
  }

  saveAddress() {
    const userId = localStorage.getItem("userId");
  
    if (!userId) {
      console.error('User ID is missing');
      this.messageService.add({ severity: 'error', summary: 'Save Failed', detail: 'User ID is missing.' });
      return;
    }
  
    const addressData: Address = {
      addressId:this.newAddress.addressId,
      street: this.newAddress.street,
      city: this.newAddress.city,
      state: this.newAddress.state,
      postalCode: this.newAddress.postalCode,
      country: this.newAddress.country,
      userId: userId
      
    };
    console.log(userId)
    if (this.newAddress.addressId) {
      console.log(this.newAddress.addressId)
      this.addressService.updateAddress(this.newAddress.addressId, addressData).subscribe(
        (response: Address) => {
          this.messageService.add({ severity: 'success', summary: 'Address Updated', detail: 'Your address has been successfully updated.' });
          this.displayDialog = false;
          this.loadAddresses(userId); // Refresh the list
        },
        (error: any) => {
          console.error('Failed to update address:', error);
          this.messageService.add({ severity: 'error', summary: 'Address Update Failed', detail: 'There was an error updating your address.' });
        }
      );
    } else {
      this.addressService.addAddress(addressData).subscribe(
        (response: Address) => {
          this.messageService.add({ severity: 'success', summary: 'Address Added', detail: 'Your address has been successfully added.' });
          this.displayDialog = false;
          this.loadAddresses(userId); // Refresh the list
        },
        (error: any) => {
          console.error('Failed to add address:', error);
          this.messageService.add({ severity: 'error', summary: 'Address Addition Failed', detail: 'There was an error adding your address.' });
        }
      );
    }
  }

  openAddDialog() {
    this.newAddress = { street: '', city: '', state: '', postalCode: '', country: '' };
    this.displayDialog = true;
  }

  openEditDialog(address: Address) {
    this.newAddress = { ...address };
    this.displayDialog = true;
  }

  confirmDeleteAddress(address: Address) {
    const testAddressId = address.addressId!; // Use a valid ID for testing
  this.addressService.deleteAddress(testAddressId).subscribe(
    () => {console.log('Delete successful');
      this.loadAddresses(address.userId!)
    },
    (error) => console.error('Delete failed', error)
  );
  }
  

  getOrders(){
    return this.orderService.getOrder().subscribe(
      (responce:Order[])=>{
        this.orders=responce
      }
    );
  }
  showOrders: boolean = false; // Flag to toggle orders visibility

  // Method to toggle orders visibility
  toggleOrders() {
    if (!this.showOrders) {
      this.getOrders();
    } else {
      this.orders = []; // Clear orders if they are currently visible
    }
    this.showOrders = !this.showOrders; // Toggle visibility flag
  }

  downloadFile(bookId: number, fileType: string) {
    let fileObservable: Observable<Blob>;
    switch (fileType) {
      case 'audio':
        fileObservable = this.fileService.getAudioFile(bookId);
        break;
      case 'video':
        fileObservable = this.fileService.getVideoFile(bookId);
        break;
      case 'pdf':
        fileObservable = this.fileService.getPdfFile(bookId);
        break;
      default:
        console.error('Invalid file type');
        return;
    }

    fileObservable.subscribe(
      (blob: Blob) => {
        const url = URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `${bookId}.${fileType}`; // Set the file name here
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        URL.revokeObjectURL(url); // Clean up the URL object
      },
      (error: any) => {
        console.error('Failed to download file:', error);
        this.messageService.add({ severity: 'error', summary: 'Download Failed', detail: 'There was an error downloading the file.' });
      }
    );
  }
  
}
