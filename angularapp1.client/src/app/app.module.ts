import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {ReactiveFormsModule} from '@angular/forms';

import {PasswordDisplayComponent} from "./components/password-display.component";
import {PasswordDialogComponent, PasswordFormComponent} from "./components/password-dialog.component";

@NgModule({
  declarations: [
    AppComponent, PasswordDisplayComponent, PasswordDialogComponent, PasswordFormComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
