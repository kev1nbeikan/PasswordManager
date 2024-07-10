import {Component, Input, Output, EventEmitter, OnInit, OnDestroy} from '@angular/core';
import {Subscription} from "rxjs";
import {NotificationService} from "../services/notificationService";

@Component({
  selector: 'app-notification',
  template: `
    <div class="notification" *ngIf="showNotification"
         [ngClass]="{'error': type === 'error', 'success': type === 'success'}">
      <div class="message">{{ message }}</div>
      <button (click)="closeNotification()">Закрыть</button>
    </div>
  `,
  styles: [`
    .notification {
      position: fixed;
      top: 20px;
      right: 20px;
      background-color: white;
      border-radius: 5px;
      padding: 10px;
      box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
      z-index: 1000;
    }

    .notification.error {
      background-color: #f8d7da;
      color: #721c24;
    }

    .notification.success {
      background-color: #d4edda;
      color: #155724;
    }

    .message {
      margin-bottom: 5px;
    }
  `]
})
export class NotificationComponent implements OnInit, OnDestroy {
  showNotification = false;
  message: string | null = null;
  type: 'error' | 'success' | null = null;
  private subscription: Subscription = new Subscription();

  constructor(private notificationService: NotificationService) {
  }

  ngOnInit(): void {
    this.subscription.add(
      this.notificationService.notification$.subscribe(notification => {
        this.showNotification = notification.showNotification;
        this.message = notification.message;
        this.type = notification.type;
      })
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  closeNotification(): void {
    this.notificationService.closeNotification();
  }
}
