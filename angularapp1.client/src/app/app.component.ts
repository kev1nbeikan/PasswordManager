import {HttpClient} from '@angular/common/http';
import {Component, OnInit} from '@angular/core';
import {SavedPassword} from './models/saved-password';
import {PasswordSaverService} from "./services/passwordSaverService";
import {NotificationService} from "./services/notificationService";


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent
  implements OnInit {
  public passwords: SavedPassword[] = [];

  constructor(private passwordSaverService: PasswordSaverService, private notificationService: NotificationService
  ) {
  }

  ngOnInit() {
    this.getPasswords();
  }


  getPasswords() {
    this.passwordSaverService.getPasswords().subscribe(
      (result) => {
        this.showPasswords(result);
      },
      (error) => {
        this.notificationService.showSuccessWithTimeout("Произошла ошибка при получении паролей", 5000)
      }
    );
  }

  showPasswords(passwords: SavedPassword[]) {
    this.passwords = passwords
  }


  title = 'angularapp1.client';
}
