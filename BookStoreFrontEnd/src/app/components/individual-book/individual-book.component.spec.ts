import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndividualBookComponent } from './individual-book.component';

describe('IndividualBookComponent', () => {
  let component: IndividualBookComponent;
  let fixture: ComponentFixture<IndividualBookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndividualBookComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndividualBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
