import { Component } from '@angular/core';
import { ToastService } from './services/toast.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'PracticeApp';
  message: { message: string, type: 'success' | 'error' } | null = null;

  constructor() { }

  // ngOnInit() {
  //   this.toastService.message$.subscribe(message => {
  //     this.message = message;
  //     if (message) {
  //       setTimeout(() => this.message = null, 3000); // Auto-hide after 3 seconds
  //     }
  //   });
  // }
}
