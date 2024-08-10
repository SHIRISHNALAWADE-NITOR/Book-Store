import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SlidingCardsComponent } from './sliding-cards.component';

describe('SlidingCardsComponent', () => {
  let component: SlidingCardsComponent;
  let fixture: ComponentFixture<SlidingCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SlidingCardsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SlidingCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
