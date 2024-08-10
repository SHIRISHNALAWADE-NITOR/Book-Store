// toast.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  private messageSource = new BehaviorSubject<{ message: string, type: 'success' | 'error' } | null>(null);
  message$ = this.messageSource.asObservable();

  show(message: string, type: 'success' | 'error') {
    this.messageSource.next({ message, type });
  }

  clear() {
    this.messageSource.next(null);
  }
}
