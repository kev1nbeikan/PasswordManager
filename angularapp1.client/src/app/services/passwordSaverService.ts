import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {Observable, tap, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {SavedPassword} from '../models/saved-password';
import {NotificationService} from "../components/error/notificationService";

@Injectable({
  providedIn: 'root'
})
export class PasswordService {

  private apiUrl = 'password'; // Замените на ваш URL

  constructor(private http: HttpClient, private notificationService: NotificationService) {
  }

  getPasswords(): Observable<SavedPassword[]> {
    return this.http.get<SavedPassword[]>(this.apiUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  searchPasswords(query: string): Observable<SavedPassword[]> {
    return this.http.get<SavedPassword[]>(`${this.apiUrl}/search?searchString=${query}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  savePassword(password: SavedPassword): Observable<SavedPassword> {
    return this.http.post<SavedPassword>(this.apiUrl, password)
      .pipe(
        catchError(this.handleError),
        tap(savedPassword => this.notificationService.showSuccessWithTimeout('Пароль добавлен', 3000))
      );
  }


  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Ошибка на сервере';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Ошибка: ${error.error.message}`;
    } else {
      if (error.status === 404) {
        errorMessage = 'Запрашиваемый ресурс не найден';
      } else if (error.status === 400) {
        errorMessage = error.error;
      }
    }

    return throwError(errorMessage);

  }
}
