import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {Observable, tap, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {SavedPassword} from '../models/saved-password';
import {NotificationService} from "./notificationService";
import {NewSavedPasswordRequest} from "../models/new-saved-password-request";

@Injectable({
  providedIn: 'root'
})
export class PasswordSaverService {

  private apiUrl = 'password';

  constructor(private http: HttpClient, private notificationService: NotificationService) {
  }

  getPasswords(): Observable<SavedPassword[]> {
    return this.http.get<SavedPassword[]>(this.apiUrl)
  }

  searchPasswords(query: string): Observable<SavedPassword[]> {
    return this.http.get<SavedPassword[]>(`${this.apiUrl}/search?searchString=${query}`)
  }

  savePassword(password: NewSavedPasswordRequest): Observable<SavedPassword> {
    return this.http.post<SavedPassword>(this.apiUrl, password)

  }

}
