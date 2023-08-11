import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DuedateComponent } from './duedate.component';

describe('DuedateComponent', () => {
  let component: DuedateComponent;
  let fixture: ComponentFixture<DuedateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DuedateComponent]
    });
    fixture = TestBed.createComponent(DuedateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
