import {HttpClient} from '@angular/common/http';
import {Component, OnInit} from '@angular/core';

interface SavedPassword {
  id: string;
  password: string;
  source: string;
  sourceType: number;
  createdDate: string;
}


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent
  implements OnInit {
  public forecasts: SavedPassword[] = [];

  constructor(private http: HttpClient
  ) {
  }

  ngOnInit() {
    this.getForecasts();
  }


  getForecasts() {
    this.http.get<SavedPassword[]>('password').subscribe(
      (result) => {
        this.forecasts = result
      },
      (error) => {
        console.error(error);
      }
    );
  }




  title = 'angularapp1.client';
}
