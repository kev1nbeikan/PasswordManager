import {Injectable} from '@angular/core';
import {Subject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notificationSubject = new Subject<{
    showNotification: boolean,
    message: string | null,
    type: 'error' | 'success' | null
  }>();
  notification$ = this.notificationSubject.asObservable();

  showSuccess(message: string): void {
    this.notificationSubject.next({
      showNotification: true,
      message,
      type: 'success'
    });
  }

  showError(message: string): void {
    this.notificationSubject.next({
      showNotification: true,
      message,
      type: 'error'
    });
  }


  showSuccessWithTimeout(message: string, ms: number): void {
    this.showSuccess(message);

    if (ms) {
      setTimeout(() => {
        this.closeNotification();
      }, ms);
    }
  }

  showErrorWithTimeout(message: string, ms: number): void {
    this.showError(message)
    if (ms) {
      setTimeout(() => {
        this.closeNotification();
      }, ms);
    }
  }

  closeNotification(): void {
    this.notificationSubject.next({
      showNotification: false,
      message: null,
      type: null
    });
  }
}
