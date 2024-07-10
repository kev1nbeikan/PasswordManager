import {HttpClient} from '@angular/common/http';
import {Component, OnInit} from '@angular/core';
import {SavedPassword} from './models/saved-password';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent
  implements OnInit {
  public passwords: SavedPassword[] = [];

  constructor(private http: HttpClient
  ) {
  }

  ngOnInit() {
    this.getPasswords();
  }


  getPasswords() {
    this.http.get<SavedPassword[]>('password').subscribe(
      (result) => {
        this.showPasswords(result);
      },
      (error) => {
        console.error(error);
      }
    );
  }

  showPasswords(passwords: SavedPassword[]) {
    this.passwords = passwords
  }


  title = 'angularapp1.client';
}
