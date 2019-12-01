import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GamingCentreComponent } from './gaming-centre.component';

describe('GamingCentreComponent', () => {
  let component: GamingCentreComponent;
  let fixture: ComponentFixture<GamingCentreComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GamingCentreComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GamingCentreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
