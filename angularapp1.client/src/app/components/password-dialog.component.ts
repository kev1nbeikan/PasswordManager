import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, Validators} from '@angular/forms';
import {HttpClient} from "@angular/common/http";


enum SourceTypes {
  Email = 0,
  Site = 1
}


@Component({
  selector: 'app-password-form',
  template: `
    <button (click)="openDialog()">Добавить пароль</button>
    <app-password-dialog *ngIf="showDialog" (closeDialogEvent)="closeDialog()"></app-password-dialog>
  `,
  styles: [],
})
export class PasswordFormComponent implements OnInit {
  showDialog = false;


  constructor() {
  }

  ngOnInit(): void {
  }

  openDialog(): void {
    this.showDialog = true;
  }

  closeDialog(): void {
    this.showDialog = false;
  }
}

@Component({
  selector: 'app-password-dialog',
  template: `
    <div class="dialog">
      <div class="dialog-header">
        <h2>Сохранить новый пароль</h2>
        <button (click)="closeDialog()">Закрыть</button>
      </div>
      <form [formGroup]="form" (ngSubmit)="savePassword()">
        <div class="dialog-content">
          <div class="form-field">
            <label for="name">Ресурс:</label>
            <input type="text" id="name" formControlName="name" required>
            <span *ngIf="form.get('name')?.hasError('required')">
              Поле обязательно для заполнения
            </span>
          </div>
          <div class="form-field">
            <label for="password">Пароль:</label>
            <input type="password" id="password" formControlName="password" required>
            <span *ngIf="form.get('password')?.hasError('required')">
              Поле обязательно для заполнения
            </span>
            <span *ngIf="form.get('password')?.hasError('minlength')">
              Пароль должен быть не менее 8 символов
            </span>
          </div>
          <div class="form-field">
            <label>Тип записи:</label>
            <input type="radio" id="siteSourceType" value="site" formControlName="sourceType"
                   (click)="onSourceTypeChange('site')" checked>
            <label for="site">Сайт</label>
            <input type="radio" id="emailSourceType" value="email" formControlName="sourceType"
                   (click)="onSourceTypeChange('email')">
            <label for="email">Электронная почта</label>
            <span *ngIf="form.get('sourceType')?.hasError('required')">
              Поле обязательно для заполнения
            </span>
            <span *ngIf="form.get('sourceType')?.value === 'email' && form.get('name')?.hasError('invalidEmail')">
              Неверный адрес электронной почты
            </span>
          </div>
        </div>
        <div class="dialog-actions">
          <button type="button" (click)="closeDialog()">Отмена</button>
          <button type="submit" [disabled]="!form.valid">Сохранить</button>
        </div>
      </form>
    </div>
  `,
  styles: [`
    .dialog {
      position: fixed;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      background-color: white;
      padding: 20px;
      border-radius: 5px;
      box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
    }

    .dialog-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 20px;
    }

    .dialog-content {
      margin-bottom: 20px;
    }

    .form-field {
      margin-bottom: 10px;
    }

    .dialog-actions {
      display: flex;
      justify-content: flex-end;
    }

    .form-field label {
      display: block;
      margin-bottom: 5px;
    }

    .form-field input {
      width: 100%;
      padding: 8px;
      border: 1px solid #ccc;
      border-radius: 3px;
    }
  `]
})
export class PasswordDialogComponent implements OnInit {
  form: FormGroup;
  sourceType = 'site'; // По умолчанию сайт

  constructor(private http: HttpClient) {
    this.form = new FormGroup({
      name: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      sourceType: new FormControl(this.sourceType,),
    });
  }

  ngOnInit(): void {

  }

  savePassword(): void {
    if (this.form.valid) {
      console.log('Saved password:', this.form.value);

      let passwordRequest = {
        Source: this.form.get('name')?.value,
        SourceType: this.form.value.sourceType,
        Password: this.form.get('password')?.value
      };

      this.http.post('password', passwordRequest).subscribe(
        (result) => {
          console.log(result);
        },
        (error) => {
          console.error(error);
        }
      );


    }
  }

  @Output() closeDialogEvent = new EventEmitter<void>();

  closeDialog(): void {
    this.closeDialogEvent.emit();
  }

  validateEmail(control: AbstractControl): { [key: string]: any } | null {
    const email = control.value;
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email) ? null : {invalidEmail: true};
  }

  onSourceTypeChange(type: string) {
    this.sourceType = type
    if (type === 'email') {
      this.form.get('name')?.setValidators([this.validateEmail,]);
    } else if (type === 'site') {
      this.form.get('name')?.removeValidators([this.validateEmail,]);
    }

    this.form.get('name')?.updateValueAndValidity();

    console.log(this.sourceType);
  }
}
