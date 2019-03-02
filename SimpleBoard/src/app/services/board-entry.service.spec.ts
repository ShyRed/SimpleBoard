import { TestBed } from '@angular/core/testing';

import { BoardEntryService } from './board-entry.service';

describe('BoardEntryService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: BoardEntryService = TestBed.get(BoardEntryService);
    expect(service).toBeTruthy();
  });
});
