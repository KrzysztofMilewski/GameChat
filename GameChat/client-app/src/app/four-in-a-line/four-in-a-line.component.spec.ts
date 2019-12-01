import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FourInALineComponent } from './four-in-a-line.component';

describe('FourInALineComponent', () => {
  let component: FourInALineComponent;
  let fixture: ComponentFixture<FourInALineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FourInALineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FourInALineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
