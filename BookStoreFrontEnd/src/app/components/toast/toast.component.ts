import { Component, OnInit } from '@angular/core';
import { ToastService } from 'src/app/services/toast.service'; // Adjust the import path

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css']
})
export class ToastComponent implements OnInit {
  message: string = '';
  type: 'success' | 'error' | null = null;

  constructor(private toastService: ToastService) {}

  ngOnInit(): void {
    this.toastService.message$.subscribe(messageData => {
      console.log('ToastComponent received:', messageData); // Debugging
      if (messageData) {
        this.message = messageData.message;
        this.type = messageData.type;
        console.log('Message:', this.message, 'Type:', this.type); // Debugging
        setTimeout(() => {
          this.message='';
          this.type = null;
        }, 1000); // Adjust timing as needed
      }
    });
  }
  
}
