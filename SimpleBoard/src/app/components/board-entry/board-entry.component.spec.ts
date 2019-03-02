import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoardEntryComponent } from './board-entry.component';

describe('BoardEntryComponent', () => {
  let component: BoardEntryComponent;
  let fixture: ComponentFixture<BoardEntryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoardEntryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoardEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
