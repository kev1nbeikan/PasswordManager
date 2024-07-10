import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import {PasswordDisplayComponent} from "./components/password-display.component";
import {PasswordDialogComponent, PasswordFormComponent} from "./components/password-dialog.component";
import {NotificationService} from "./components/error/notificationService";
import {NotificationComponent} from "./components/error/notification.component";
import {SearchComponent} from "./components/search.component";
import {PasswordService} from "./services/passwordSaverService";

@NgModule({
  declarations: [
    AppComponent, PasswordDisplayComponent, PasswordDialogComponent, PasswordFormComponent, NotificationComponent, SearchComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule, FormsModule
  ],
  providers: [NotificationService, PasswordService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
