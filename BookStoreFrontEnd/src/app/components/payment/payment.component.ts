import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/order.service';
import { UserService } from 'src/app/services/user.service';
import { AddressService } from 'src/app/services/address.service';
import { CartService } from 'src/app/services/cart.service';
import { BookService } from 'src/app/services/book.service';
import { UserDTO } from 'src/app/services/user.dto';
import { Address } from 'src/app/services/address.model';
import { CartItem } from 'src/app/services/cart-item.model';
import { Book } from 'src/app/services/book.model';
import { EmailService } from 'src/app/services/email.service';
import { ToastService } from 'src/app/services/toast.service';
 
@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  order: any = {
    userId: localStorage.getItem("userId"),
    totalAmount: 0,
    shippingAddressId: 0,
    orderItems: []
  };
 
  paymentDetails = {
    name: '',
    email: '',
    phone: '',
    address: '',
    city: '',
    state: '',
    postalCode: '',
    cardNumber: '',
    expiryDate: '',
    cvv: ''
  };
 
  totalItems: number = 0;
  addresses: Address[] = [];
  selectedAddressId: number | null = null;
  books: Map<number, Book> = new Map(); // Store book details by ID
 
  constructor(
    private orderService: OrderService,
    private userService: UserService,
    private addressService: AddressService,
    private cartService: CartService,
    private bookService: BookService,
    private emailService: EmailService,
    private toastService:ToastService
  ) { }
 
  ngOnInit(): void {
    this.loadUserData();
    this.loadAddressData();
    this.loadCartItems();
  }
 
  loadUserData(): void {
    const userId = localStorage.getItem("userId");
    if (userId) {
      this.userService.getUserProfile(userId).subscribe((user: UserDTO) => {
        this.paymentDetails.name = user.name || '';
        this.paymentDetails.email = user.email || '';
        this.paymentDetails.phone = user.phoneNumber || '';
      });
    }
  }
 
  loadAddressData(): void {
    const userId = this.order.userId;
    if (userId) {
      this.addressService.getAddresses(userId).subscribe((addresses: Address[]) => {
        this.addresses = addresses;
        if (addresses.length > 0) {
          this.selectedAddressId = addresses[0].addressId!;
          this.updateSelectedAddress(addresses[0]);
        }
      });
    }
  }
 
  onAddressSelect(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const selectedAddressId = parseInt(target.value, 10);
    const selectedAddress = this.addresses.find(address => address.addressId === selectedAddressId);
   
    if (selectedAddress) {
      this.selectedAddressId = selectedAddressId;
      this.updateSelectedAddress(selectedAddress);
    }
  }
 
  updateSelectedAddress(address: Address): void {
    this.paymentDetails.address = address.street || '';
    this.paymentDetails.city = address.city || '';
    this.paymentDetails.state = address.state || '';
    this.paymentDetails.postalCode = address.postalCode || '';
    this.order.shippingAddressId = address.addressId;
  }
 
  loadCartItems(): void {
    this.cartService.getCart().subscribe((items: CartItem[]) => {
      const bookRequests = items.map(item => this.bookService.getBook(item.bookId).toPromise());
 
      Promise.all(bookRequests).then(books => {
        books.forEach(book => {
          if (book) {
            this.books.set(book.bookId, book);
          }
        });
 
        this.order.orderItems = items.map(item => {
          const book = this.books.get(item.bookId) || { title: 'Unknown Title', price: 0, author: 'Unknown Author' };
          return {
            bookId: item.bookId,
            quantity: item.quantity,
            price: book.price,
            title: book.title,
            author: book.author
          };
        });
 
        this.order.totalAmount = items.reduce((sum, item) => sum + (item.quantity * (this.books.get(item.bookId)?.price || 0)), 0);
        this.totalItems = items.reduce((sum, item) => sum + item.quantity, 0);
      }).catch(error => {
        console.error('Error fetching books:', error);
      });
    });
  }
 
 
  submitPayment(): void {
    if (this.isPaymentDetailsValid()) {
      const orderData = {
        orderItemId: 0,
        userId: this.order.userId,
        totalAmount: this.order.totalAmount,
        shippingAddressId: this.order.shippingAddressId,
        orderItems: this.order.orderItems
      };
 
      this.orderService.createOrder(orderData).subscribe({
        next: response => {
          console.log('Order created successfully:', response);
         
          this.toastService.show("Payment Successful",'success')
          //mail service
          //this.emailService.sendEmail(this.paymentDetails.email,this.paymentDetails.name,"Payment successful","Thank you for ordering")

          this.cartService.clearCart().subscribe({
            next: () => {
              this.printBill();
            },
            error: err => {
              console.error('Error clearing cart:', err);
              alert('Failed to clear cart.');
            }
          });
        },
        error: error => {
          console.error('Error placing order:', error);
          alert('Failed to place order.');
        }
      });
    } else {
      alert("Please enter all required payment details.");
    }
  }
 
  isPaymentDetailsValid(): boolean {
    return (
      this.paymentDetails.cardNumber.trim() !== '' &&
      this.paymentDetails.cvv.trim() !== '' &&
      this.order.totalAmount !== 0
    );
  }
 
  printBill(): void {
    const printableContent = document.getElementById('printableArea')?.innerHTML;
   
    if (printableContent) {
      const printWindow = window.open('', '', 'height=600,width=800');
      if (printWindow) {
        printWindow.document.open();
        printWindow.document.write('<html><head><title>Invoice</title>');
        printWindow.document.write(`
          <style>
            body {
              font-family: Arial, sans-serif;
              margin: 20px;
              padding: 0;
            }
            .printable-area {
              padding: 20px;
              border: 1px solid #ddd;
              border-radius: 8px;
              background: #fff;
              box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            }
            h1, h2 {
              color: #333;
            }
            p {
              color: #666;
            }
            .btn-primary {
              display: inline-block;
              padding: 12px 20px;
              border: none;
              border-radius: 8px;
              background-color: #007bff;
              color: #fff;
              font-size: 16px;
              font-weight: bold;
              text-align: center;
              cursor: pointer;
              text-decoration: none;
              transition: background-color 0.3s, transform 0.2s;
            }
            .btn-primary:hover {
              background-color: #0056b3;
              transform: translateY(-2px);
            }
          </style>
        `);
        printWindow.document.write('</head><body>');
        printWindow.document.write('<div class="printable-area">');
        printWindow.document.write(printableContent);
        printWindow.document.write('</div>');
        printWindow.document.write('</body></html>');
        printWindow.document.close();
       
        printWindow.onload = function() {
          printWindow.focus();
          printWindow.print();
        };
      } else {
        console.error('Failed to open print window.');
      }
    } else {
      console.error('No content found for printing.');
    }
  }
}