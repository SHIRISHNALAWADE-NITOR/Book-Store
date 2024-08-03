import { Component } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent {
  paymentDetails = {
    name: '',
    email: '',
    phone: '',
    address: '',
    city: '',
    state: '',
    zipcode: '',
    cardNumber: '',
    expiryDate: '',
    cvv: ''
  };

  constructor(private cartService: CartService,private toastService: ToastService) { }
  totalItems: number = 0;
  totalAmount: number = 0;
  ngOnInit(): void {
    this.cartService.getTotalAmount().subscribe(amount => this.totalAmount = amount);
    this.cartService.getTotalItems().subscribe(items => this.totalItems = items);
  }
  submitPayment(): void {
    // Check if all required fields are filled
    if (this.isPaymentDetailsValid()) {
      console.log('Payment Details:', this.paymentDetails);


      // Optionally, clear the cart after payment
      // this.cartService.clearCart(); // Implement this method in CartService if needed

      // Print the bill
      this.printBill();
    } else {
      alert("Please enter all required payment details.");
    }
  }

  isPaymentDetailsValid(): boolean {
    // Check if all fields are non-empty
    return (
      this.paymentDetails.name.trim() !== '' &&
      this.paymentDetails.email.trim() !== '' &&
      this.paymentDetails.phone.trim() !== '' &&
      this.paymentDetails.address.trim() !== '' &&
      this.paymentDetails.city.trim() !== '' &&
      this.paymentDetails.state.trim() !== '' &&
      this.paymentDetails.zipcode.trim() !== '' &&
      this.paymentDetails.cardNumber.trim() !== '' &&
      this.paymentDetails.expiryDate.trim() !== '' &&
      this.paymentDetails.cvv.trim() !== '' &&
      this.totalAmount!==0
    );
  }


  printBill(): void {
    const printWindow = window.open('', '', 'height=600,width=800');
    printWindow!.document.write('<html><head><title>Invoice</title>');
    printWindow!.document.write('</head><body >');
    printWindow!.document.write(document.getElementById('printableArea')!.innerHTML);
    printWindow!.document.write('</body></html>');
    printWindow!.document.close();
    printWindow!.focus();
    printWindow!.print();
  }
}
