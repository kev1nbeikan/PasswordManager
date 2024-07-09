import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [HttpClientTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve weather forecasts from the server', () => {
    const mockPasswords = [
      { id: '1', password: 'password1', source: 1, sourceType: 1, CreatedDate: new Date() },
      { id: '2', password: 'password2', source: 2, sourceType: 2, CreatedDate: new Date() }
    ];

    component.ngOnInit();

    const req = httpMock.expectOne('/WeatherForecast');
    expect(req.request.method).toEqual('GET');
    req.flush(mockPasswords);

    expect(component.forecasts).toEqual(mockPasswords);
  });
});
