import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoardInputComponent } from './board-input.component';

describe('BoardInputComponent', () => {
  let component: BoardInputComponent;
  let fixture: ComponentFixture<BoardInputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoardInputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoardInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
