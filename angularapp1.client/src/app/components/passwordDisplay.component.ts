import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-password-display',
  template: `
    <span *ngIf="!showPassword" (click)="togglePassword()" class="masked">{{ '*****' }}</span>
    <span *ngIf="showPassword" (click)="togglePassword()">{{ password }}</span>
  `,
  styles: [`
    .masked {
      color: gray;
      cursor: pointer;
    }
  `]
})
export class PasswordDisplayComponent {
  @Input() password: string = '';
  showPassword = false;

  togglePassword() {
    this.showPassword = !this.showPassword;
  }
}
