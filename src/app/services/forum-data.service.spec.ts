import { TestBed } from '@angular/core/testing';

import { ForumDataService } from './forum-data.service';

describe('ForumDataService', () => {
  let service: ForumDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ForumDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
