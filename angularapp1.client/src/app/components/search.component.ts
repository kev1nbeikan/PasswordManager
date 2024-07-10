import {Component, OnInit, Output, EventEmitter, Input} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {HttpErrorResponse} from '@angular/common/http';
import {NotificationService} from '../services/notificationService';
import {SavedPassword} from '../models/saved-password';
import {PasswordSaverService} from "../services/passwordSaverService";

@Component({
  selector: 'app-search',
  template: `
    <input type="text" placeholder="Поиск..." [(ngModel)]="searchQuery" (ngModelChange)="onSearchChange()">
    <button (click)="onSearchChange()">Поиск</button>
    <button (click)="getAllPasswords()">Показать все</button>
  `
})
export class SearchComponent implements OnInit {
  @Input() initialPasswords: SavedPassword[] = [];
  @Output() searchResults = new EventEmitter<SavedPassword[]>();


  searchQuery: string = '';
  lastSearchResults: SavedPassword[] = []; // Храним результаты последнего успешного поиска

  constructor(private notificationService: NotificationService, private passwordService: PasswordSaverService) {
  }

  ngOnInit() {
  }

  onSearchChange() {
    if (this.searchQuery.trim() === "") {
      this.getAllPasswords();
    } else {

      this.passwordService.searchPasswords(this.searchQuery).subscribe(
        (passwords: SavedPassword[]) => {
          this.lastSearchResults = passwords;
          this.searchResults.emit(passwords);
        },
        (error: HttpErrorResponse) => {
          this.handleSearchError(error);
          this.getAllPasswords();
        }
      );
    }
  }

  getAllPasswords() {
    this.passwordService.getPasswords().subscribe((passwords: SavedPassword[]) => {
      this.searchResults.emit(passwords)
    })

  }

  handleSearchError(error: HttpErrorResponse) {
    let errorMessage = 'Ошибка поиска';
    if (error.error && typeof error.error === 'string') {
      errorMessage = error.error;
    } else if (error.error && error.error.message) {
      errorMessage = error.error.message;
    }
    this.notificationService.showErrorWithTimeout(errorMessage, 3000);
  }
}
