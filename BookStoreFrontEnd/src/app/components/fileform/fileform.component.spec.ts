import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileformComponent } from './fileform.component';

describe('FileformComponent', () => {
  let component: FileformComponent;
  let fixture: ComponentFixture<FileformComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FileformComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FileformComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
